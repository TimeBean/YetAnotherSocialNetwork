namespace YASN.Domain.Users;

public class Profile
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string? Description { get; private set; }
    public DateTime Created { get; private set; }

    public Profile(Guid id, string username, string? description = null)
    {
        Id = id;
        Username = username;
        Description = description;
        Created = DateTime.UtcNow;
    }
}