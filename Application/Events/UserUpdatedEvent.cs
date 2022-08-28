using Pigsty.Events;

namespace Application.Events;

public record class UserUpdatedEvent(
    Guid Id,
    Guid TenantId,
    string Email,
    DateTimeOffset ModificationDate) : IEvent
{ }
