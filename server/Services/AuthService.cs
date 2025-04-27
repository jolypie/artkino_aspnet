using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Services;
using server.Shared.DTOs;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokenService;

    public AuthService(AppDbContext db, TokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
            throw new Exception("User already exists");

        CreatePasswordHash(dto.Password, out var hash, out var salt);

        var user = new User
        {
            Username     = dto.Username,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = dto.Role
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync(); 

        var playlists = new List<Playlist>
        {
            new() { Name = "Favorites",   UserId = user.Id, IsSystem = true },
            new() { Name = "Watch Later", UserId = user.Id, IsSystem = true },
            new() { Name = "Watched",     UserId = user.Id, IsSystem = true }
        };
        await _db.Playlists.AddRangeAsync(playlists);
        await _db.SaveChangesAsync(); 
    }

    public async Task<(AuthResponseDto auth, string refreshToken)> LoginAsync(UserLoginDto dto)
    {
        var user = await _db.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null) throw new Exception("User not found");

        if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Wrong password");

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var hashedRefresh = _tokenService.ComputeHash(refreshToken);

        user.RefreshTokens.Add(new RefreshToken
        {
            TokenHash = hashedRefresh,
            Expires = DateTime.UtcNow.AddDays(7),
            User = user
        });
        await _db.SaveChangesAsync();

        var authDto = new AuthResponseDto(accessToken, new UserDetailsResponseDto(user.Username, user.Role));
        return (authDto, refreshToken);
    }

    public async Task<string> RefreshAsync(string refreshToken)
    {
        var hash = _tokenService.ComputeHash(refreshToken);
        var tokenEntity = await _db.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.TokenHash == hash && !t.Revoked);
        if (tokenEntity == null || tokenEntity.Expires < DateTime.UtcNow)
            throw new Exception("Invalid refresh token");

        var newAccess = _tokenService.GenerateAccessToken(tokenEntity.User);
        return newAccess;
    }

    public async Task LogoutAsync(int userId, string refreshToken)
    {
        var hash = _tokenService.ComputeHash(refreshToken);
        var token = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == hash && t.UserId == userId && !t.Revoked);
        if (token == null) return;
        token.Revoked = true;
        await _db.SaveChangesAsync();
    }

    private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }
}
