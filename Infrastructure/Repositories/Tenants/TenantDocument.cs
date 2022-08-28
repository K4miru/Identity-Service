using Pigsty.Databases;

namespace Infrastructure.Repositories.Tenants;

public record TenantDocument(
    string Id,
    string Email,
    bool IsConfirmed,
    DateTimeOffset CreationDate,
    DateTimeOffset ModificationDate,
    int Version) : IIdentity<string>
{ }
