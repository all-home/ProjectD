using Microsoft.EntityFrameworkCore;

namespace LOP.SystemProfelis.Models
{
    public class ProfileContext : DbContext
    {
        public DbSet<ProfileModel> Files { get; set; }
        public ProfileContext(DbContextOptions<ProfileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

