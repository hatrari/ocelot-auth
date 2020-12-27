using Microsoft.EntityFrameworkCore;

namespace Getway.Entities
{
  public class AuthContext : DbContext
  {
    public AuthContext(DbContextOptions options)
      :base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}