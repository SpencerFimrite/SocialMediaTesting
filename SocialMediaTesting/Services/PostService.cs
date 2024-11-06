using Blazored.LocalStorage;
using SocialMediaTesting.Models;

namespace SocialMediaTesting.Services
{
    public class PostService
    {
        private readonly UserService _userService;
        private readonly ILocalStorageService _localStorage;

        public PostService(UserService userService, ILocalStorageService localStorage)
        {
            _userService = userService;
            _localStorage = localStorage;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _localStorage.GetItemAsync<List<Post>>("userPosts") ?? new List<Post>();
            return posts;
        } 
        public async Task AddPost(Post newPost)
        {
            var posts = await GetAllPosts();
            newPost.PostId = posts.Count + 1;
            posts.Add(newPost);
            await _localStorage.SetItemAsync("userPosts", posts);
        }
        public async Task LikePost(Post post)
        {
            var userId = await _userService.GetCurrentUserId();
            var likingUser = await _userService.GetUserById(userId);
            var posts = await GetAllPosts();
            var postToUpdate = posts.FirstOrDefault(p => p.PostId == post.PostId);
    
            if (postToUpdate != null)
            {
                if (!postToUpdate.LikedByUserIds.Contains(userId))
                {
                    postToUpdate.LikeCount++; 
                    postToUpdate.LikedByUserIds.Add(userId);
            
                    if (likingUser.LikedPosts.All(p => p.PostId != postToUpdate.PostId))
                    {
                        likingUser.LikedPosts.Add(postToUpdate);
                    }
                }
                // Update the posts and users in local storage
                await _localStorage.SetItemAsync("userPosts", posts);
                await _userService.UpdateUserInLocalStorage(likingUser); 
                await _localStorage.SetItemAsync("users", await _userService.GetAllUsers());
            }
        }
        public async Task UnlikePost(Post post)
        {
            var userId = await _userService.GetCurrentUserId();
            var likingUser = await _userService.GetUserById(userId);
            var posts = await GetAllPosts();
            var postToUpdate = posts.FirstOrDefault(p => p.PostId == post.PostId);
    
            if (postToUpdate != null)
            {
                if (postToUpdate.LikedByUserIds.Contains(userId))
                {
                    postToUpdate.LikeCount--; 
                    postToUpdate.LikedByUserIds.Remove(userId);
                    var likedPost = likingUser.LikedPosts.FirstOrDefault(p => p.PostId == postToUpdate.PostId);
                    if (likedPost != null)
                    {
                        likingUser.LikedPosts.Remove(likedPost);
                    }
                }
                
                await _localStorage.SetItemAsync("userPosts", posts);
                await _userService.UpdateUserInLocalStorage(likingUser); 
                await _localStorage.SetItemAsync("users", await _userService.GetAllUsers());
            }
        }
        public async Task<bool> IsPostLikedByCurrentUser(Post post)
        {
            var currentUserId = await _userService.GetCurrentUserId();
            return post.LikedByUserIds.Contains(currentUserId);
        }
        public async Task<Post> GetPostById(int postId)
        {
            var posts = await GetAllPosts();
            return posts.FirstOrDefault(p => p.PostId == postId);
        }
    }
}