using Pigsty.Domain.Events;

namespace Domain.Tenants;
public record TenantCreated(Tenant Tenant) : IDomainEvent { }