using InTown.Core.Models;
using InTown.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

//TODO: check the flow and read more about it.

namespace InTown.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<User?> GetByEmail(string email)
    {
        //checks if this email exist. returns null if nobody has it
        return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(); //User is saved with user.id.
        return user;
    }
}