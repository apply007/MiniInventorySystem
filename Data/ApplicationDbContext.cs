using Microsoft.EntityFrameworkCore;
using MiniInventorySystem.Model;
namespace MiniInventorySystem.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;

    }
    
}
