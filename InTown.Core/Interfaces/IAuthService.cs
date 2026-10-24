using InTown.Core.Models;

namespace InTown.Core.Interfaces;

public interface IAuthService
{
    Task<User?> Register(string name, string email, string password);
    Task<User?> Login(string email, string password);
    
}