using Microsoft.EntityFrameworkCore;
using LOP.People.Models;

namespace LOP.People.ModelsModels
{
    public class WorkerContext: DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Statistics> Stat { get; set; }

        public WorkerContext(DbContextOptions<WorkerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
