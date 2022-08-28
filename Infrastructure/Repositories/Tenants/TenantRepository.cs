using Application.Repositories;
using Domain.Tenants;
using Pigsty.Databases.MongoDb;

namespace Infrastructure.Repositories.Tenants;

internal sealed class TenantRepository : ITenantRepository
{
    private readonly IMongoDbRepository<TenantDocument, string> _repository;

    public TenantRepository(IMongoDbRepository<TenantDocument, string> repository)
    {
        _repository = repository;
        _repository.CollectionName = "tenants";
    }

    public Task AddAsync(Tenant tenant) => _repository.AddAsync(tenant.AsDocument());
    public async Task<Tenant?> GetByEmail(string email)
        => (await _repository.GetAllAsync(tenant => tenant.Email == email))
            .FirstOrDefault()?
            .AsAggregate();
}