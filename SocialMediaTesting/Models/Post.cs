using System;

namespace SocialMediaTesting.Models;

public class Post
{
    public int PostId { get; set; } 
    public int UserId { get; set; } 
    public string Content { get; set; }
    public string Image { get; set; }
    public List<int> LikedByUserIds { get; set; } = new List<int>();
    public int LikeCount { get; set; }
    public DateTime Timestamp { get; set; }
}