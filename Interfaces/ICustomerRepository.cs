using MiniInventorySystem.Model;

namespace MiniInventorySystem.Interfaces
{
    public interface ICustomerRepository
    {

        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer?> UpdateAsync(int id, Customer customer);
        Task<bool> DeleteAsync(int id);
    }
}
