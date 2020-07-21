using Microsoft.EntityFrameworkCore;

namespace LOP.Models
{
    public class PersonContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Statistics> Stat { get; set; }

        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
