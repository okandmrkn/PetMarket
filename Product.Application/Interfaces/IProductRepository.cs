using Product.Application.DTOs;
using Product.Domain.Entities;
using Product.Domain.Enums;

namespace Product.Application.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(ProductInfo product);
        Task UpdateAsync(ProductInfo product);
        Task DeleteAsync(uint id);
        Task<IEnumerable<ProductInfo>> GetAllAsync();
        Task<ProductInfo?> GetByIdAsync (uint id);
        Task<IEnumerable<ProductInfo>> GetByCategoryAsync (ProductCategory category);
 

    }
}
