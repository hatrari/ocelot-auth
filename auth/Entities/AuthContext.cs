using Microsoft.EntityFrameworkCore;

namespace auth.Entities
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