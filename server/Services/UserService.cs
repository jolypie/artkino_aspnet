using server.Models;
using server.Repositories.IRepositories;
using server.Shared.DTOs;

namespace server.Services;

public class UserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepo.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepo.GetByIdAsync(id);
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepo.GetByIdAsync(id)
                   ?? throw new Exception("User not found");

        user.Username = dto.Username;

        await _userRepo.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id)
                   ?? throw new Exception("User not found");

        await _userRepo.DeleteAsync(user);
    }
}