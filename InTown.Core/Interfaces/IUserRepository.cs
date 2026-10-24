using InTown.Core.Models;

namespace InTown.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(string id);
    Task<User> Add(User user);
}