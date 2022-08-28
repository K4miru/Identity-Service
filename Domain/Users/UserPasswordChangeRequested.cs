using Pigsty.Domain.Events;

namespace Domain.Users;
internal record UserPasswordChangeRequested(User user) : IDomainEvent { }