using Domain.Tenants;

namespace Application.Repositories;

public interface ITenantRepository
{
    Task<Tenant?> GetByEmail(string email);
    Task AddAsync(Tenant tenant);
}
