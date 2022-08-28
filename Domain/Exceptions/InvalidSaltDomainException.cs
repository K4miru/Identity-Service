using Pigsty.Domain;

namespace Domain.Exceptions;

public class InvalidSaltDomainException : DomainException
{
    public override string Code => "invalid_salt";
    public InvalidSaltDomainException(string salt) : base($"Invalid salt: {salt}") { }
}
