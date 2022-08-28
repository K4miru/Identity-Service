using Pigsty.Events;

namespace Application.Events;

public record class UserCreatedEvent(
    Guid Id,
    Guid TenantId,
    string Email,
    DateTimeOffset CreationDate) : IEvent
{ }
