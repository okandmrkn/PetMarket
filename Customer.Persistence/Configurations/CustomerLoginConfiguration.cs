using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerLoginConfiguration : IEntityTypeConfiguration<CustomerLogin>
{
    public void Configure(EntityTypeBuilder<CustomerLogin> builder)
    {
        builder.ToTable("CustomerLogins");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Password).IsRequired();
    }
}