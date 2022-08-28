using Domain.Users;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.Users;

internal static class UserMappings
{
    internal static User AsAggregate(this UserDocument userDocument)
        => new User(
            Guid.Parse(userDocument.Id),
            Guid.Parse(userDocument.TenantId),
            new Email(userDocument.Email, userDocument.IsConfirmed),
            new Password(
                userDocument.Password,
                userDocument.Salt,
                userDocument.RecoveryId == null ? null : Guid.Parse(userDocument.RecoveryId)
                ),
            userDocument.CreationDate,
            userDocument.ModificationDate,
            userDocument.Version);

    internal static UserDocument AsDocument(this User user)
        => new UserDocument(
            user.Id.ToString(),
            user.TenantId.ToString(),
            user.Email.Value,
            user.Email.IsConfirmed,
            user.Password.Value,
            user.Password.Salt,
            user.Password.RecoveryId?.ToString(),
            user.CreationDate,
            user.ModificationDate,
            user.Version
        );
}