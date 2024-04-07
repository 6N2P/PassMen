using Microsoft.EntityFrameworkCore;
using PassMen.Beakend.Model;

namespace PassMen.Beakend.Data
{
    public class PassMenDbContext : DbContext
    {
        public PassMenDbContext(DbContextOptions<PassMenDbContext> options) 
            : base(options)
        {
            
        }
       

        public DbSet<UserPass> UserPasses { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Application> Applications { get; set; }
    }

    
}
