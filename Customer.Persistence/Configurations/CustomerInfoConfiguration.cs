using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerInfoConfiguration : IEntityTypeConfiguration<CustomerInfo>
{
    public void Configure(EntityTypeBuilder<CustomerInfo> builder)
    {
        builder.ToTable("Customers"); 
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.Gender).IsRequired();

        builder.HasOne(x => x.LoginInfo)
               .WithOne(x => x.Customer)
               .HasForeignKey<CustomerLogin>(x => x.Id);

        builder.HasOne(x => x.BalanceInfo)
               .WithOne(x => x.Customer)
               .HasForeignKey<CustomerBalance>(x => x.Id);
    }
}
