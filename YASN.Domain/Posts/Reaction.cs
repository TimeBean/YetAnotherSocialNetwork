namespace YASN.Domain.Posts;

public class Reaction
{
    public Guid UserId { get; private set; }
    public Guid TargetId { get; private set; }
    public bool IsLike { get; private set; }
    public DateTime Created { get; private set; }

    public Reaction(Guid userId, Guid targetId, bool isLike)
    {
        UserId = userId;
        TargetId = targetId;
        IsLike = isLike;
        Created = DateTime.UtcNow;
    }
}