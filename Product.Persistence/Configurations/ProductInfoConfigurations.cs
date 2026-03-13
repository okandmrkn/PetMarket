using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Persistence.Configurations
{
    public class ProductInfoConfigurations : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Category);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Category).IsRequired().HasConversion<string>();

            builder.HasMany(x => x.Price)
                   .WithOne(x => x.Product)
                   .HasForeignKey("ProductId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Stock)
                   .WithOne(x => x.Product)
                   .HasForeignKey<ProductStock>(x => x.Id);
        }
    }
}
