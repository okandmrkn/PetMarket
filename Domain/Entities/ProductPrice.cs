using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Entities
{
    public class ProductPrice
    {
        public uint Id { get; private set; }
        public decimal Price { get; private set; }
        public DateTimeOffset ValidAfter { get; private set; }
        public DateTimeOffset ValidUntil { get; private set; }
        public ProductInfo Product { get; private set; } = null!;

        private ProductPrice() { }
        public ProductPrice(ProductInfo Product, decimal price) {
            if (price <= 0) throw new Exception("Price cannot be zero or negative.");
            ArgumentNullException.ThrowIfNull(Product);
            Price = price;
            ValidAfter = DateTimeOffset.UtcNow;
            ValidUntil = DateTimeOffset.MaxValue;
        }

        public void ExpirePriceDate(DateTimeOffset expireDate) {
            ValidUntil = expireDate;
        }
    }
}
