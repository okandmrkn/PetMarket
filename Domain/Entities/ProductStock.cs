using Product.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Entities
{
    public class ProductStock
    {
        public uint Id { get; private set; }
        public int StockCount { get; private set; }
        public ProductStatus Status { get; private set; }
        public byte[] RowVersion { get; private set; } = null!;
        public ProductInfo Product { get; private set; } = null!;

        private ProductStock() { }
        private void UpdateStatus() => Status = StockCount == 0 ? ProductStatus.OutOfStock : ProductStatus.InStock;
        public ProductStock(ProductInfo Product, int initialStock = 0) {

            ArgumentNullException.ThrowIfNull(Product);

            if (initialStock < 0) throw new Exception("Initial stock cannot be negative.");

            StockCount = initialStock;
            UpdateStatus();

        }

        public void ManageStock(int amount) {
            if (amount < 0)
            {
                if (StockCount < Math.Abs(amount)) throw new Exception("Not enough products in stock.");
                StockCount -= Math.Abs(amount);
            }
            else { 
                StockCount += Math.Abs(amount);
            }

            UpdateStatus();
        }
    }
}
