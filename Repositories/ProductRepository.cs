using Microsoft.EntityFrameworkCore;
using MiniInventorySystem.Data;
using MiniInventorySystem.Interfaces;
using MiniInventorySystem.Model;

namespace MiniInventorySystem.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
          _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && !p.IsDeleted);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null || existing.IsDeleted)
                return null;

            existing.Name = product.Name;
            existing.Barcode = product.Barcode;
            existing.Price = product.Price;
            existing.StockQty = product.StockQty;
            existing.Category = product.Category;
            existing.Status = product.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null || existing.IsDeleted)
                return false;

            existing.IsDeleted = true; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
