using Microsoft.EntityFrameworkCore;

namespace Manage.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
    }
}
