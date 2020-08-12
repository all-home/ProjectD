using Microsoft.EntityFrameworkCore;

namespace LOP.SystemProfelis.Models
{
    public class ProfileContext : DbContext
    {
        public DbSet<ProfileModel> Files { get; set; }
        public FileContext(DbContextOptions<ProfileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

