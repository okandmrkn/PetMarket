using Product.Application.DTOs;
using Product.Application.DTOs.Interfaces;
using Product.Application.Exceptions;
using Product.Application.Interfaces;
using Product.Application.Operations;
using Product.Domain.Entities;
using Product.Domain.Enums;
using Product.Domain.Services;
using System.Formats.Asn1;


namespace Product.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IPriceHistoryManager _priceHistoryManager;

    public ProductService(IProductRepository productRepository, IPriceHistoryManager priceManager)
    {
        _productRepository = productRepository;
        _priceHistoryManager = priceManager;
    }

    private BaseResponse<bool> ValidateProductRequest(IProductBase request)
    {
        //return request.Validate();
        if (string.IsNullOrWhiteSpace(request.Name))
            return BaseResponse<bool>.Failure("Product Name Can't be Empty");

        if (request.Price <= 0)
            return BaseResponse<bool>.Failure("Price Can't be Zero Or Negative");

        if (request.StockCount < 0)
            return BaseResponse<bool>.Failure("Stock Amount Can't be Negative");

        return BaseResponse<bool>.Success(true);
    }
    private ProductResponse ProductResponseMapper(ProductInfo product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.CurrentPrice.Price,
            StockCount = product.Stock.StockCount
        };
    }

    public async Task<BaseResponse<bool>> CreateProduct(CreateProductRequest product)
    {

        var validationResult = ValidateProductRequest(product);
        if (!validationResult.IsSuccess) return validationResult;

        try
        {
            var newProduct = new ProductInfo(product.Name, product.Category);
            var price = new ProductPrice(newProduct, product.Price);
            var stock = new ProductStock(newProduct, product.StockCount);

            newProduct.SetStockInfo(stock);
            newProduct.AddPrice(price);
            await _productRepository.AddAsync(newProduct);
            return BaseResponse<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.Failure(ex.Message);
        }
    }

    public async Task<BaseResponse<bool>> UpdateProduct(UpdateProductRequest request)
    {

        var validationResult = ValidateProductRequest(request);
        if (!validationResult.IsSuccess) return validationResult;

        var existProduct = await _productRepository.GetByIdAsync(request.Id);
        if (existProduct == null)
        {
            return BaseResponse<bool>.Failure("Product Not Found");
        }

        try
        {
            existProduct.UpdateName(request.Name);
            existProduct.UpdateStockCount(request.StockCount);

            await _priceHistoryManager.UpdateProductPrice(existProduct, request.Price);
            await _productRepository.UpdateAsync(existProduct);

            return BaseResponse<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.Failure(ex.Message);
        }
    }

    public async Task<BaseResponse<ProductResponse>> GetById(uint id)
    {
        try
        {
            var existProduct = await _productRepository.GetByIdAsync(id);
            if (existProduct == null)
            {
                return BaseResponse<ProductResponse>.Failure("Product Not Found");
            }
            var response = ProductResponseMapper(existProduct);
            return BaseResponse<ProductResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return BaseResponse<ProductResponse>.Failure(ex.Message);
        }
    }
    public async Task<BaseResponse<IEnumerable<ProductResponse>>> GetAll()
    {
        try
        {
            var allProducts = await _productRepository.GetAllAsync();
            var responseList = allProducts.Select(c => ProductResponseMapper(c)).ToList();
            return BaseResponse<IEnumerable<ProductResponse>>.Success(responseList);
        }
        catch (Exception ex)
        {
            return BaseResponse<IEnumerable<ProductResponse>>.Failure(ex.Message);
        }
    }
    public async Task<BaseResponse<bool>> DeleteById(uint id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return BaseResponse<bool>.Failure("Product Not Found");

            await _productRepository.DeleteAsync(id);

            return BaseResponse<bool>.Success(true, "Product Deleted Successfully");
        }
        catch (Exception ex)
        {
            return BaseResponse<bool>.Failure($"An error occurred while deleting: {ex.Message}");
        }
    }
    public async Task<BaseResponse<IEnumerable<ProductResponse>>> GetByCategory(ProductCategory category)
    {
        try
        {
            var allProductsInCategory = await _productRepository.GetByCategoryAsync(category);
            var responseList = allProductsInCategory.Select(c => ProductResponseMapper(c)).ToList();
            return BaseResponse<IEnumerable<ProductResponse>>.Success(responseList);
        }
        catch (Exception ex)
        {
            return BaseResponse<IEnumerable<ProductResponse>>.Failure(ex.Message);
        }
    }

    public async Task<BaseResponse<bool>> UpdateStock(uint productId, int amount)
    {
        StockOperationTemplate operation = amount > 0
            ? new AddStockOperation(_productRepository, amount)
            : new RemoveStockOperation(_productRepository, Math.Abs(amount));

        return await operation.ExecuteAsync(productId);

    }
}
