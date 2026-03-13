using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.DTOs.Interfaces
{
    public interface IProductBase
    {
        string Name { get; init; }
        decimal Price { get; init; }
        int StockCount { get; init; }

    }
}
