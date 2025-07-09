using Microsoft.EntityFrameworkCore;
using MiniInventorySystem.Data;
using MiniInventorySystem.Interfaces;
using MiniInventorySystem.Model;
using System;

namespace MiniInventorySystem.Repositories
{
    public class CustomerRepository:ICustomerRepository 
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id && !c.IsDeleted);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(int id, Customer customer)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null || existing.IsDeleted)
                return null;

            existing.FullName = customer.FullName;
            existing.Phone = customer.Phone;
            existing.Email = customer.Email;
            existing.LoyaltyPoints = customer.LoyaltyPoints;

            await _context.SaveChangesAsync();
            return existing;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null || existing.IsDeleted)
                return false;

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
