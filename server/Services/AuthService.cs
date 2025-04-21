using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Models;
using server.Shared.DTOs;

public class AuthService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _db;

    public AuthService(IConfiguration config, AppDbContext db)
    {
        _config = config;
        _db = db;
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
            throw new Exception("User already exists");

        CreatePasswordHash(dto.Password, out var hash, out var salt);

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        // Create default playlists
        var playlists = new List<Playlist>
        {
            new() { Name = "Favorites", UserId = user.Id, IsSystem = true },
            new() { Name = "Watch Later", UserId = user.Id, IsSystem = true },
            new() { Name = "Watched", UserId = user.Id, IsSystem = true }
        };

        await _db.Playlists.AddRangeAsync(playlists);
        await _db.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null) throw new Exception("User not found");

        if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Wrong password");

        return CreateToken(user);
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }

    private string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
