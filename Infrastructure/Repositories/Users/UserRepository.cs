using Application.Repositories;
using Domain.Users;
using Pigsty.Databases.MongoDb;

namespace Infrastructure.Repositories.Users;

internal sealed class UserRepository : IUserRepository
{
    private readonly IMongoDbRepository<UserDocument, string> _repository;

    public UserRepository(IMongoDbRepository<UserDocument, string> repository)
    {
        _repository = repository;
        _repository.CollectionName = "users";
    }

    public Task AddAsync(User user) => _repository.AddAsync(user.AsDocument());
    public Task UpdateAsync(User user) => _repository.UpdateAsync(user.AsDocument());
    public async Task<User?> GetByEmail(string email)
        => (await _repository.GetAllAsync(user => user.Email == email))
            .FirstOrDefault()?
            .AsAggregate();
}