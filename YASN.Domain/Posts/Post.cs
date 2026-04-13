namespace YASN.Domain.Posts;

public class Post
{
    public Guid Id { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Content { get; private set; }
    public DateTime Created { get; private set; }

    public Post(Guid id, Guid authorId, string content)
    {
        Id = id;
        AuthorId = authorId;
        Content = content;
        Created = DateTime.UtcNow;
    }
}