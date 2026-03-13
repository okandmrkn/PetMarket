using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;
using Product.Domain.Enums;

namespace Product.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

 
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return Ok(result);
    }

 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(uint id)
    {
        var result = await _productService.GetById(id);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }


    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(ProductCategory category)
    {
        var result = await _productService.GetByCategory(category);
        return Ok(result);
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var result = await _productService.CreateProduct(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

 
    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductRequest request)
    {
        var result = await _productService.UpdateProduct(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    
    [HttpPost("{id}/stock")]
    public async Task<IActionResult> UpdateStock(uint id, int amount)
    {
  
        var result = await _productService.UpdateStock(id, amount);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

  
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var result = await _productService.DeleteById(id);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}