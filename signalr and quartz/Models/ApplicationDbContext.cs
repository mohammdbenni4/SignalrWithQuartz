using Microsoft.EntityFrameworkCore;

namespace signalr_and_quartz.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
        
        public DbSet<Order> Orders { get; set; }
    }
}
