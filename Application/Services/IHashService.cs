namespace Application.Services;

public interface IHashService
{
    public string GenerateSalt();
    public string GetHash(string text, string salt);
}