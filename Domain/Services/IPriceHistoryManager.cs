using Product.Domain.Entities;

namespace Product.Domain.Services
{
    public interface IPriceHistoryManager
    {
        Task UpdateProductPrice(ProductInfo product, decimal newPrice);

    }
}
