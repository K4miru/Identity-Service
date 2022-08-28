using Pigsty.Domain;

namespace Domain.Exceptions;

public class InvalidEmailDomainException : DomainException
{
    public override string Code => "invalid_email";
    public InvalidEmailDomainException(string email) : base($"Invalid email: {email}") { }
}
