using Application.Events;
using Application.Exceptions;
using Application.Repositories;
using Application.Services;
using Pigsty.CQRS.Command;
using Pigsty.MessagesBrokers;

namespace Application.Commands;

public record class RecoverPasswordCommand(string Email, Guid RecoveryId, string Password) : ICommand { }
internal record class RecoverPasswordCommandHandler(
    IUserRepository _userRepository,
    IMessageBroker _messageBroker,
    IHashService _hashService) : ICommandHandler<RecoverPasswordCommand>
{
    public async Task HandleAsync(RecoverPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(command.Email);
        if (user is null)
        {
            throw new UserDoesNotExistException(command.Email);
        }

        if (!user.Password.WasRecoveryRequested || user.Password.RecoveryId != command.RecoveryId)
        {
            throw new PasswordCanNotBeRecoveredException(command.Email);
        }

        var salt = _hashService.GenerateSalt();
        var hashedPassword = _hashService.GetHash(command.Password, salt);

        user.ChangePassword(hashedPassword, salt);
        //TODO: Send Email about changed password

        await _userRepository.UpdateAsync(user);
        await _messageBroker.Publish(new UserUpdatedEvent(user.Id, user.TenantId, user.Email.Value, user.ModificationDate));
    }
}