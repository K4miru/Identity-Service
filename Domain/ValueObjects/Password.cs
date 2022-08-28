using Domain.Exceptions;
using Pigsty.Domain;

namespace Domain.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; }
    public string Salt { get; }
    public Guid? RecoveryId { get; private set; }
    public bool WasRecoveryRequested => RecoveryId != null;

    public Password(string password, string salt, Guid? recoveryId = null)
    {
        if (!IsPasswordValid(password))
        {
            throw new InvalidPasswordDomainException(password);
        }
        Value = password;

        if (!IsSaltValid(salt))
        {
            throw new InvalidSaltDomainException(salt);
        }
        Salt = salt;

        RecoveryId = recoveryId;
    }

    public void RequestPasswordChange() 
        => RecoveryId = Guid.NewGuid();

    private bool IsSaltValid(string salt) => !string.IsNullOrWhiteSpace(Salt);
    private bool IsPasswordValid(string password) => !string.IsNullOrWhiteSpace(password);
}
