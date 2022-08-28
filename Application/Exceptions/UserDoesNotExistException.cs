namespace Application.Exceptions;

public class UserDoesNotExistException : ApplicationException
{
    public override string Code { get; } = "user_does_not_exist_exception";
    public UserDoesNotExistException(string email) : base($"User with email {email} does not exist")
    {
    }
}
