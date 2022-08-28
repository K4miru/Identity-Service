using Pigsty.Domain.Events;

namespace Domain.Users;
public record UserCreated(User User) : IDomainEvent { }