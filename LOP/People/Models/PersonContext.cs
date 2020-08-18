using Microsoft.EntityFrameworkCore;
using LOP.People.Models;

namespace LOP.People.ModelsModels
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
