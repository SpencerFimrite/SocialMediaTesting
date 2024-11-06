namespace SocialMediaAPI.Models;

// Class for posts to hold post data
public class Post
{
    public int Id { get; set; } 
    public int UserId { get; set; } 
    public string Content { get; set; }
    public List<int> LikedByUserIds { get; set; } = [];
    public int LikeCount { get; set; }
    public DateTime Timestamp { get; set; }
}