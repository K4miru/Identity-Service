using Pigsty.Events;

namespace Application.Events;

public record class TenantCreatedEvent(
    Guid Id,
    DateTimeOffset CreationDate) : IEvent
{ }
