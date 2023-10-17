using Microsoft.EntityFrameworkCore;

namespace Vision_Metering.Data
{
    public class VisionDbContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VisionDb;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
