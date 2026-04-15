using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using YASN.Domain.Service;

namespace YASN.Infrastructure.Service;

public class PasswordService : IPasswordService
{
    public string ComputeHash(string password, string salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = Encoding.UTF8.GetBytes(salt),
            DegreeOfParallelism = 4,
            MemorySize = 65536,
            Iterations = 3
        };

        var hash = argon2.GetBytes(32);

        return Convert.ToBase64String(hash);
    }

    public bool VerifyHash(string stored_hash, string password, string salt)
    {
        var computedHash = ComputeHash(password, salt);

        var hashBytes = Convert.FromBase64String(stored_hash);
        var computedBytes = Convert.FromBase64String(computedHash);

        return CryptographicOperations.FixedTimeEquals(hashBytes, computedBytes);
    }
}