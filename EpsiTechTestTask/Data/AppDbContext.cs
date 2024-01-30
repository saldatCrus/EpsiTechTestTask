using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace EpsiTechTestTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public DbSet<Models.Enitity.Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
