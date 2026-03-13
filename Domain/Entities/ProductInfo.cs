using Product.Domain.Enums;

namespace Product.Domain.Entities
{
    public class ProductInfo
    {
        public uint Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public ProductCategory Category { get; private set; }

        public List<ProductPrice> Price { get; private set; } = null!;
        public ProductStock Stock { get; private set; } = null!;

        private ProductInfo() { }
        public ProductInfo(string name, ProductCategory category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("name cannot be empty.");
            Name = name;
            Category = category;
        }
        public ProductPrice CurrentPrice => Price
            .OrderByDescending(x => x.ValidAfter)
            .FirstOrDefault()!;
        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdateStockCount(int newCount)
        {
            Stock.ManageStock(newCount);
        }

        public void AddPrice(ProductPrice price)
        {
            if (Price == null)
            {
                Price = new List<ProductPrice>();
            }

            Price.Add(price);
        }
        public void SetStockInfo(ProductStock stock) => Stock = stock; 
    }

}
