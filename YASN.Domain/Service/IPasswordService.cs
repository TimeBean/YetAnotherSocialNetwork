namespace YASN.Domain.Service;

public interface IPasswordService
{
    string ComputeHash(string password, string salt);
    bool VerifyHash(string stored_hash, string password, string salt);
}