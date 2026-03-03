using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistence.Configurations
{
    public class CustomerBalanceConfiguration : IEntityTypeConfiguration<CustomerBalance>
    {
        public void Configure(EntityTypeBuilder<CustomerBalance> builder)
        {
            builder.ToTable("CustomerBalances");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Balance)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}