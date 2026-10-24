using InTown.Core.Models;
using InTown.Core.Interfaces;

namespace InTown.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository  _userRepo;

    public AuthService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }
    
    public async Task<User?> Register(string name, string email, string password)
    {
        //capital and lower case are the same
        email = email.Trim().ToLower();
        
        //Check if email is already taken 
        var existing = await _userRepo.GetByEmail(email);

        if (existing != null)
            return null;
        var user = new User(); 
        user.Name = name.Trim();
        user.Email = email.Trim();
        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        user.CreatedAt = DateTime.UtcNow;
        
        var saved = await _userRepo.Add(user);
        return saved;
    }

    public async Task<User?> Login(string email, string password)
    {
        email = email.Trim().ToLower();

        var user = await _userRepo.GetByEmail(email);
        if (user == null)
            return null;
        //Hash what they typed and compare it to the saved hash
        bool correct = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!correct)
            return null;
        
        return user;
        
    }
}