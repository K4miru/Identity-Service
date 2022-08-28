using Pigsty.Domain.Events;

namespace Domain.Users;
internal record UserPasswordChanged(User user) : IDomainEvent { }