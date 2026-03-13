using Product.Domain.Entities;
namespace Product.Domain.Services
{
    public class PriceHistoryManager : IPriceHistoryManager
    {
        public Task UpdateProductPrice(ProductInfo product, decimal newPrice)
        {
            if (newPrice <= 0) throw new Exception("Price cannot be zero or negative.");
            var currentPrice = product.CurrentPrice;
            if (currentPrice != null)
            {
                currentPrice.ExpirePriceDate(DateTimeOffset.UtcNow);
            }
            ProductPrice price = new ProductPrice(product, newPrice);
            product.AddPrice(price);
            return Task.CompletedTask;
        }
    }
}
