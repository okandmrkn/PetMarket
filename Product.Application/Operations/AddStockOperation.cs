using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Domain.Entities;

namespace Product.Application.Operations;

public class AddStockOperation : StockOperationTemplate
{
    public AddStockOperation(IProductRepository repository, int amount) : base(repository, amount) { }

    protected override BaseResponse<bool> ProcessStockLogic(ProductInfo product)
    {
        product.Stock.ManageStock(_amount);
        return BaseResponse<bool>.Success(true);
    }
}