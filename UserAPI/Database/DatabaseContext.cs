using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
