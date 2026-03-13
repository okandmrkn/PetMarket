using Product.Application.DTOs.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.DTOs
{
    public record CreateProductRequest : IProductBase
    {
        public string Name { get; init; } = string.Empty;
        public ProductCategory Category { get; init; }
        public decimal Price { get; init; }
        public int StockCount { get; init; }


    }

}
