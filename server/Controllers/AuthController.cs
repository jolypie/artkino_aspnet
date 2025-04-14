using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Services;
using server.Shared.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;
    private readonly AppDbContext _db;

    public AuthController(AuthService auth, AppDbContext db)
    {
        _auth = auth;
        _db = db;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterDto registerDto)
    {
        if(_db.Users.Any(u => u.Username == registerDto.Username))
            return BadRequest("User already exists");
        
        _auth.CreatePasswordHash(registerDto.Password, out var hash, out var salt);
        
        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        return Ok("Registered");
    }
    
    
    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null) return Unauthorized("User not found");

        if (!_auth.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            return Unauthorized("Wrong password");

        var token = _auth.CreateToken(user);
        return Ok(new { token });
    }
}