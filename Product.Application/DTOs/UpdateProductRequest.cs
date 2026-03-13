using Product.Application.DTOs.Interfaces;
using Product.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.DTOs
{
    public class UpdateProductRequest : IProductBase
    {
        public uint Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public ProductCategory Category { get; init; }
        public decimal Price { get; init; }
        public int StockCount { get; init; }
    }
}
