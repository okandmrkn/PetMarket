using Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Product.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<ProductPrice> Prices { get; set; }
        public DbSet<ProductStock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}