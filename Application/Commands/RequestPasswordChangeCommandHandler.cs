using Application.Events;
using Application.Exceptions;
using Application.Repositories;
using Pigsty.CQRS.Command;
using Pigsty.MessagesBrokers;

namespace Application.Commands;

public record class RequestPasswordChangeCommand(string Email) : ICommand { }
internal record class RequestPasswordChangeCommandHandler(
    IUserRepository _userRepository,
    IMessageBroker _messageBroker) : ICommandHandler<RequestPasswordChangeCommand>
{
    public async Task HandleAsync(RequestPasswordChangeCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(command.Email);
        if (user is null)
        {
            throw new UserDoesNotExistException(command.Email);
        }

        user.RequestPasswordChange();
        //TODO: Send Email with link to password recovery page

        await _userRepository.UpdateAsync(user);
        await _messageBroker.Publish(new UserUpdatedEvent(user.Id, user.TenantId, user.Email.Value, user.ModificationDate));
    }
}