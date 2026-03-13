using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Persistence.Configurations
{
    internal class ProductStockConfigurations : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder) {
            builder.ToTable("Stocks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever(); 

            builder.Property(x => x.StockCount)
                   .IsRequired();
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            
            builder.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
