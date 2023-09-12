using Microsoft.EntityFrameworkCore;
using Wishlis.Domain;

namespace Wishlis.Infrastructure;

public class WishlistContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> UserSettings{ get; set; }
    public DbSet<WishlistItem> Items { get; set; }
    
    public WishlistContext (DbContextOptions<WishlistContext> options) : base(options) { }
}