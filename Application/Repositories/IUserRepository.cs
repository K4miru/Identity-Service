using Domain.Users;

namespace Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}