using Application.Events;
using Application.Exceptions;
using Application.Repositories;
using Application.Services;
using Domain.Users;
using Pigsty.CQRS.Command;
using Pigsty.Domain;
using Pigsty.MessagesBrokers;

namespace Application.Commands;

public record class CreateUserCommand(string Email, string Password, Guid TenantId) : ICommand { }
internal record class CreateUserCommandHandler(
    IUserRepository _userRepository,
    IMessageBroker _messageBroker,
    IHashService _hashService) : ICommandHandler<CreateUserCommand>
{
    public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(command.Email);
        if (user is { })
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        var salt = _hashService.GenerateSalt();
        var hashedPassword = _hashService.GetHash(command.Password, salt);

        user = User.CreateUser(new AggregateId(), command.TenantId, command.Email, hashedPassword, salt);

        await _userRepository.AddAsync(user);
        await _messageBroker.Publish(new UserCreatedEvent(user.Id, user.TenantId, user.Email.Value, user.CreationDate));
    }
}