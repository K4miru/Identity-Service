using Pigsty.Domain;

namespace Domain.Exceptions;

public class InvalidPasswordDomainException : DomainException
{
    public override string Code => "invalid_password";
    public InvalidPasswordDomainException(string password) : base($"Invalid password: {password}") { }
}
