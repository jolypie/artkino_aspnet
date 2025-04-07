using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Get() => Ok(_context.Users.ToList());

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }

}
