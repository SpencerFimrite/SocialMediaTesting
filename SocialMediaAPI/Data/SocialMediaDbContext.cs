using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Data;

public class SocialMediaDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options)
    {
        
    }
}