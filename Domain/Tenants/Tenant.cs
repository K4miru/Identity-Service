using Domain.ValueObjects;
using Pigsty.Domain;

namespace Domain.Tenants;

public sealed class Tenant : AggregateRoot
{
    public AggregateId Id { get; private set; }
    public Email Email { get; private set; }
    public DateTimeOffset CreationDate { get; private set; }
    public DateTimeOffset ModificationDate { get; private set; }

    public Tenant(AggregateId id, Email email, DateTimeOffset creationDate, DateTimeOffset modificationDate, int version = 1)
    {
        Id = id;
        Email = email;
        CreationDate = creationDate;
        ModificationDate = modificationDate;
        Version = version;
    }

    public static Tenant CreateTenant(AggregateId id, string email)
    {
        var date = DateTimeOffset.UtcNow;
        var tenant = new Tenant(id, new Email(email), date, date);
        tenant.AddEvent(new TenantCreated(tenant));
        return tenant;
    }
}