using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITCareerProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Cool-events-Exam;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}