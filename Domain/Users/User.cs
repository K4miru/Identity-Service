using Domain.ValueObjects;
using Pigsty.Domain;

namespace Domain.Users;

public sealed class User : AggregateRoot
{
    public AggregateId Id { get; private set; }
    public AggregateId TenantId { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTimeOffset CreationDate { get; private set; }
    public DateTimeOffset ModificationDate { get; private set; }

    public User(
        AggregateId id,
        AggregateId tenantId,
        Email email,
        Password password,
        DateTimeOffset creationDate,
        DateTimeOffset modificationDate,
        int version = 1)
    {
        Id = id;
        TenantId = tenantId;
        Email = email;
        Password = password;
        CreationDate = creationDate;
        ModificationDate = modificationDate;
        Version = version;
    }

    public void RequestPasswordChange()
    {
        Password.RequestPasswordChange();
        var date = DateTimeOffset.UtcNow;
        ModificationDate = date;
        AddEvent(new UserPasswordChangeRequested(this));
    }

    public void ChangePassword(string password, string salt)
    {
        var changedPassword = new Password(password, salt);
        var date = DateTimeOffset.UtcNow;
        Password = changedPassword;
        ModificationDate = date;
        AddEvent(new UserPasswordChanged(this));
    }

    public static User CreateUser(AggregateId id, AggregateId tenantId, string email, string password, string salt)
    {
        var date = DateTimeOffset.UtcNow;
        var user = new User(id, tenantId, new Email(email), new Password(password, salt), date, date);
        user.AddEvent(new UserCreated(user));
        return user;
    }
}