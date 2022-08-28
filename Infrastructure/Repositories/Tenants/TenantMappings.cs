using Domain.Tenants;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.Tenants;

internal static class TenantMappings
{
    internal static Tenant AsAggregate(this TenantDocument tenantDocument)
        => new Tenant(
            Guid.Parse(tenantDocument.Id),
            new Email(tenantDocument.Email, tenantDocument.IsConfirmed),
            tenantDocument.CreationDate,
            tenantDocument.ModificationDate,
            tenantDocument.Version);

    internal static TenantDocument AsDocument(this Tenant tenant)
        => new TenantDocument(
            tenant.Id.ToString(),
            tenant.Email.Value,
            tenant.Email.IsConfirmed,
            tenant.CreationDate,
            tenant.ModificationDate,
            tenant.Version
        );
}