using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Persistence.Configurations
{
    internal class ProductPriceConfigurations : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder) {
            builder.ToTable("Prices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(x => x.ValidAfter);
            builder.Property(x => x.ValidUntil);



        }
    }
}
