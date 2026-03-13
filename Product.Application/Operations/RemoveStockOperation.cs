using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Application.Operations;
using Product.Domain.Entities;

public class RemoveStockOperation : StockOperationTemplate
{
    public RemoveStockOperation(IProductRepository repository, int amount) : base(repository, amount) { }

    protected override int RetryCount => 5; 

    protected override BaseResponse<bool> ProcessStockLogic(ProductInfo product)
    {
        if (product.Stock.StockCount < _amount)
            return BaseResponse<bool>.Failure("Insufficient Stock Amount!");

        product.Stock.ManageStock(-_amount);
        return BaseResponse<bool>.Success(true);
    }
}