namespace YASN.Domain.Users;

public class UserCredential
{
    public Guid UserId { get; private set; }
    public string Hash { get; private set; }
    public string Salt { get; private set; }

    public UserCredential(Guid userId, string hash, string salt)
    {
        UserId = userId;
        Hash = hash;
        Salt = salt;
    }
}