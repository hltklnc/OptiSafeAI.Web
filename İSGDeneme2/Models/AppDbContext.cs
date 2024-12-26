using Microsoft.EntityFrameworkCore;

namespace İSGDeneme2.Models
{
    public class AppDbContext : DbContext
    {   
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Login> Login {  get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
