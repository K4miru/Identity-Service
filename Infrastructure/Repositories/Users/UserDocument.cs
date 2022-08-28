using Pigsty.Databases;

namespace Infrastructure.Repositories.Users;

public record UserDocument(
    string Id,
    string TenantId,
    string Email,
    bool IsConfirmed,
    string Password,
    string Salt,
    string? RecoveryId,
    DateTimeOffset CreationDate,
    DateTimeOffset ModificationDate,
    int Version) : IIdentity<string>
{ }
