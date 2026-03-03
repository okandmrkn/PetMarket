using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CustomerInfo> Customers { get; set; }
        public DbSet<CustomerLogin> CustomerLogins { get; set; }
        public DbSet<CustomerBalance> CustomerBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}