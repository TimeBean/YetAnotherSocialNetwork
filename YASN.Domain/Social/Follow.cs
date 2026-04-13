namespace YASN.Domain.Social;

public class Follow
{
    public Guid FollowerId { get; private set; }  
    public Guid FolloweeId { get; private set; } 
    public DateTime Created { get; private set; }

    public Follow(Guid followerId, Guid followeeId)
    {
        if (followerId == followeeId)
        {
            throw new InvalidOperationException("Cannot follow self");
        }

        FollowerId = followerId;
        FolloweeId = followeeId;
        Created = DateTime.UtcNow;
    }
}