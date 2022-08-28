using Application.Services;
using System.Security.Cryptography;

namespace Infrastructure.Services;

internal sealed class HashService : IHashService
{
    private const int SaltByteSize = 24;
    private const int HashByteSize = 24;
    private const int IterationsCount = 1010;

    public string GenerateSalt() => GenerateSalt(SaltByteSize);
    private string GenerateSalt(int saltByteSize = SaltByteSize)
    {
        using (var saltGenerator = new RNGCryptoServiceProvider())
        {
            var salt = new byte[saltByteSize];
            saltGenerator.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }

    public string GetHash(string text, string salt) => GetHash(text, salt, IterationsCount, HashByteSize);
    private string GetHash(string text, string salt, int iterations, int hashByteSize)
    {
        using (var hashGenerator = new Rfc2898DeriveBytes(text, Convert.FromBase64String(salt)))
        {
            hashGenerator.IterationCount = iterations;
            return Convert.ToBase64String(hashGenerator.GetBytes(hashByteSize));
        }
    }
}
