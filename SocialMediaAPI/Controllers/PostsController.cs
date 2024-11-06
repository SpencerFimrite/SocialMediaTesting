using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Data;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly SocialMediaDbContext _context;
    
    public PostsController(SocialMediaDbContext context)
    {
        _context = context;
    }
    
    // Get request for user posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }
}