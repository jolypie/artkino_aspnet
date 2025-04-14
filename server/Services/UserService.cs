using server.Data;
using server.Models;

namespace server.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }
    
    public List<User> GetAllUsers() => _db.Users.ToList();
}