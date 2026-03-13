using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Domain.Enums;
using Product.Persistence.Context;
using System.Data;
using Product.Application.Exceptions;
namespace Product.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductInfo?> GetByIdAsync(uint id)
        {
            return await _context.Products
                .Include(p => p.Price)
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductInfo>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Price)
                .Include(p => p.Stock)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductInfo>> GetByCategoryAsync(ProductCategory category)
        {
            return await _context.Products
                .Include(p => p.Price)
                .Include(p => p.Stock)
                .Where(p => p.Category == category)
                .ToListAsync();
        }

        public async Task AddAsync(ProductInfo product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductInfo product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                throw new ConcurrencyException("Bu ürünün verisi başka bir işlem tarafından güncellendi.");
            }
        }

        public async Task DeleteAsync(uint id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}