namespace Product.Application.Operations;

using Product.Application.Interfaces;
using Product.Application.Exceptions;
using Product.Domain.Entities;
using Product.Application.DTOs;

public abstract class StockOperationTemplate
{
    protected readonly IProductRepository _repository;
    protected readonly int _amount;

    protected StockOperationTemplate(IProductRepository repository, int amount)
    {
        _repository = repository;
        _amount = amount;
    }

    protected virtual int RetryCount => 3;

    public async Task<BaseResponse<bool>> ExecuteAsync(uint productId)
    {
        for (int i = 0; i < RetryCount; i++)
        {
            try
            {
                var product = await _repository.GetByIdAsync(productId);
                if (product == null) return BaseResponse<bool>.Failure("Product Not Found.");

                var result = ProcessStockLogic(product);
                if (!result.IsSuccess) return result;

                await _repository.UpdateAsync(product);
                return BaseResponse<bool>.Success(true);
            }
            catch (ConcurrencyException)
            {
                if (i == RetryCount - 1) throw;
                await Task.Delay(100);
            }
        }
        return BaseResponse<bool>.Failure("Failed.");
    }

    protected abstract BaseResponse<bool> ProcessStockLogic(ProductInfo product);
}