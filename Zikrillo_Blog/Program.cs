using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.BLL.Services;
using Zikrillo_Blog.DAL.Data;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.DAL.Repositories;
using AutoMapper; // 👈 Faqat shu qolishi kerak

var builder = WebApplication.CreateBuilder(args);

// --- 1. DATABASE ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- 2. AUTOMAPPER (AMBIGUOUS CALL XATOSINI YO'QOTISH) ---
// Agar ziddiyat bo'lsa, metodni to'liq nomi bilan chaqiramiz:
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// --- 3. SERVICES (DI) ---
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPostLikeService, PostLikeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// --- 4. CONTROLLERS & FLUENT VALIDATION ---
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    });

// --- 5. JWT AUTH CONFIG ---
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ---------------------------------------------------------
var app = builder.Build();
// ---------------------------------------------------------

// --- 6. PIPELINE (MIDDLEWARE) ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Tartib juda muhim!
app.UseAuthentication();
app.UseAuthorization();

// --- 7. ROUTING ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();