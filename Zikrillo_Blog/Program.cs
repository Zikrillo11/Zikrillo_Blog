using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.BLL.Services;
using Zikrillo_Blog.DAL.Data;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 🔥 DATABASE
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // 🔥 SERVICES (DI)
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPostLikeService, PostLikeService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // 🔥 CONTROLLERS
        builder.Services.AddControllersWithViews();

        // 🔐 JWT AUTH CONFIG (ENG MUHIM 🔥)
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                )
            };
        });

        var app = builder.Build();

        //Validation qoshish
        builder.Services.AddControllers()
    .AddFluentValidation(x =>
        x.RegisterValidatorsFromAssemblyContaining<PostForCreateDtoValidator>());

        // 🔧 PIPELINE
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // 🔐 MUHIM JOYLASHUV (TARTIB MUHIM ❗)
        app.UseAuthentication();   // 👈 SHU YO‘Q EDI
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}