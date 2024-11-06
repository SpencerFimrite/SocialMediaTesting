using SocialMediaTesting.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace SocialMediaTesting.Services
{
    public class UserService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        private User CurrentUser { get; set; }

        public UserService(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _localStorage.GetItemAsync<List<User>>("users") ?? new List<User>();
            return users;
        }
        public async Task AddUser(User newUser)
        {
            var users = await GetAllUsers();
            newUser.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            newUser.UserName = GenerateUsername(newUser.Name);
            users.Add(newUser);
            await _localStorage.SetItemAsync("users", users);
        }

        public async Task<int> GetCurrentUserId()
        {
            var currentUser = await GetCurrentUser();
            return currentUser?.Id ?? -1;
        }
        public async Task<User> GetCurrentUser()
        {
            if (CurrentUser == null)
            {
                CurrentUser = await _localStorage.GetItemAsync<User>("currentUser");
                
                if (CurrentUser != null && CurrentUser.FollowingIds == null)
                {
                    CurrentUser.FollowingIds = new List<int>();
                } 
            }

            return CurrentUser;
        }
        public async Task SetCurrentUser(User user)
        {
            CurrentUser = user;
            
            if (CurrentUser.FollowingIds == null)
            {
                CurrentUser.FollowingIds = new List<int>();
            }
            
            await _localStorage.SetItemAsync("currentUser", user);
            await _localStorage.SetItemAsync("isLoggedIn", true);
        }
        public async Task<User> Login(string email)
        {
            var users = await GetAllUsers();
            var user = users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                await SetCurrentUser(user);
            }

            return user;
        }
        public async Task Logout()
        {
            CurrentUser = null;
            await _localStorage.RemoveItemAsync("currentUser");
            await _localStorage.SetItemAsync("isLoggedIn", false);
            _navigationManager.NavigateTo("/login", true);
        }
        public async Task<bool> IsUserLoggedIn()
        {
            return await _localStorage.GetItemAsync<bool>("isLoggedIn");
            ;
        }
        public async Task<User> GetUserById(int userId)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Id == userId);
        }
        public string GenerateUsername(string name)
        {
            return name.Replace(" ", "-"); // Replace spaces with hyphens
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
        public bool IsFollowing(User user)
        {
            return CurrentUser.FollowingIds.Contains(user.Id);
        }
        public async Task FollowUser(User userToFollow)
        {
            if (CurrentUser != null && userToFollow != null && !IsFollowing(userToFollow))
            {
                // Store only user IDs in the Following and Followers lists.
                CurrentUser.FollowingIds.Add(userToFollow.Id);
                userToFollow.FollowersIds.Add(CurrentUser.Id);

                await UpdateUserInLocalStorage(CurrentUser);
                await UpdateUserInLocalStorage(userToFollow);  // Also update the user being followed
            }
        }
        public async Task UnFollowUser(User userToUnfollow)
        {
            if (CurrentUser != null && userToUnfollow != null && IsFollowing(userToUnfollow))
            {
                // Remove the IDs from the lists instead of the full objects.
                CurrentUser.FollowingIds.Remove(userToUnfollow.Id);
                userToUnfollow.FollowersIds.Remove(CurrentUser.Id);

                await UpdateUserInLocalStorage(CurrentUser);
                await UpdateUserInLocalStorage(userToUnfollow);  // Also update the user being unfollowed
            }
        }

        public async Task<List<User>> GetUsersByIds(List<int> userIds)
        {
            var users = await GetAllUsers();
            return users.Where(u => userIds.Contains(u.Id)).ToList();
        }
        public async Task UpdateUserInLocalStorage(User updatedUser)
        {
            var users = await GetAllUsers();
            var existingUser = users.FirstOrDefault(u => u.Id == updatedUser.Id);
    
            if (existingUser != null)
            {
                users.Remove(existingUser);  // Remove the old user
                users.Add(updatedUser);      // Add the updated user
                await _localStorage.SetItemAsync("users", users);  // Save changes to local storage
            }
            // Also update the current user in local storage
            await SetCurrentUser(updatedUser);
        }
    }
}