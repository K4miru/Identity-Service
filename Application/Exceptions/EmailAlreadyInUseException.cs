namespace Application.Exceptions;

public class EmailAlreadyInUseException : ApplicationException
{
    public override string Code { get; } = "email_already_in_use";
    public EmailAlreadyInUseException(string email) : base($"Email is already in use {email}")
    {
    }
}
