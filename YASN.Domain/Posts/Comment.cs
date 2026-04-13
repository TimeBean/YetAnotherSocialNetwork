namespace YASN.Domain.Posts;

public class Comment
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public string Content { get; private set; }
    public DateTime Created { get; private set; }

    public Comment(Guid userId, Guid postId, string content)
    {
        UserId = userId;
        PostId = postId;
        Content = content;
        Created = DateTime.Now;
    }
}