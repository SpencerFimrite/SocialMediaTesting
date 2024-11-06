namespace SocialMediaTesting.Models;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Id { get; set; }
    public List<int> FollowingIds { get; set; } = new List<int>();
    public List<int> FollowersIds { get; set; } = new List<int>();
    public List<Post> LikedPosts { get; set; } = new List<Post>();
    public string ProfilePicPath { get; set; } = string.Empty;
}