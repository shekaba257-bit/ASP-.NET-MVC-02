using DemoAsp.Net8_Session01_.FluentConfigration;
using DemoAsp.Net8_Session01_.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAsp.Net8_Session01_.Contexts
{
    public class GymDbContext:DbContext
    {
       
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //Appsetting .JSON
                optionsBuilder.UseSqlServer("Server=.;Database=GymDb;Trusted_Connection=true;TrustServerCertificate=true");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration<Plan>(new PlanConfigration()); //Call Configration in DbContext
            }
            public DbSet<Plan> Plans { get; set; }


        
    }
}
