using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly SocialMediaDbContext _context;

    public UsersController(SocialMediaDbContext context)
    {
        _context = context;
    }

    // Get request for all users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // Post request to add a new user to all users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
    }

    // Get request to return a user by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user != null ? user : NotFound();
    }
    
    // Get request to return a user by Username
    [HttpGet("{userName}")]
    public async Task<ActionResult<User>> GetUserByUserName(string userName)
    {
        var user = await _context.Users.FindAsync(userName);
        return user != null ? user : NotFound();
    }
}