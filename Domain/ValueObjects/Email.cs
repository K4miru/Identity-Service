using Domain.Exceptions;
using Pigsty.Domain;

namespace Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }
    public bool IsConfirmed { get; private set; }

    public Email(string email)
        : this(email, false) { }

    public Email(string email, bool isConfirmed)
    {
        if (!IsValid(email))
        {
            throw new InvalidEmailDomainException(email);
        }
        Value = email;
        IsConfirmed = isConfirmed;
    }

    private bool IsValid(string email) => !string.IsNullOrWhiteSpace(email) && email.Contains("@");
}