using MiniInventorySystem.Model;

namespace MiniInventorySystem.Interfaces
{
    public interface IProductRepository
    {
        Task<(List<Product> Items, int TotalCount)> GetFilteredProductsAsync(string? search, string? category, int page, int pageSize);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<bool> DeleteAsync(int id);
    }
}
