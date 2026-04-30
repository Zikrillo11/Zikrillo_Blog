using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zikrillo_Blog.DAL.Data;
using Zikrillo_Blog.Domain.Entites;
using Zikrillo_Blog.Shared.DTOs.Auth;


// 🔥 ALIAS (xatoni oldini oladi)
using BCryptNet = BCrypt.Net.BCrypt;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // 🔥 REGISTER
    public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
    {
        // Email tekshirish
        var exists = await _context.Users
            .AnyAsync(x => x.Email == dto.Email);

        if (exists)
            throw new Exception("Email already exists");

        // User yaratish
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCryptNet.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new AuthResultDto
        {
            Token = GenerateToken(user),
            Username = user.Username
        };
    }

    // 🔥 LOGIN
    public async Task<AuthResultDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null)
            throw new Exception("User not found");

        // Password tekshirish
        var isValid = BCryptNet.Verify(dto.Password, user.PasswordHash);

        if (!isValid)
            throw new Exception("Wrong password");

        return new AuthResultDto
        {
            Token = GenerateToken(user),
            Username = user.Username
        };
    }

    // 🔑 TOKEN GENERATE
    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_config["Jwt:ExpireMinutes"])
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}