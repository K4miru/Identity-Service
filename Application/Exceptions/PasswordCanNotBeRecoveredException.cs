namespace Application.Exceptions;

public class PasswordCanNotBeRecoveredException: ApplicationException
{
    public override string Code { get; } = "password_can_not_be_recovered";
    public PasswordCanNotBeRecoveredException(string email) : base($"User with email {email} can not recover password")
    {
    }
}