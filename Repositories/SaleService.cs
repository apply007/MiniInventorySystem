using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MiniInventorySystem.Data;
using MiniInventorySystem.Interfaces;
using MiniInventorySystem.Model;
using System;
using static MiniInventorySystem.DTO.SaleRequestDTO;

namespace MiniInventorySystem.Repositories
{
    public class SaleService:ISaleService
    {
        private readonly ApplicationDbContext _context;
        private static SemaphoreSlim _saleLock = new SemaphoreSlim(3);

        public SaleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateSaleAsync(SaleRequestDto saleDto)
        {
            if (!await _saleLock.WaitAsync(0))
            {
                throw new InvalidOperationException("Too many concurrent sales. Try again later.");
            }

            try
            {
                await Task.Delay(3000); // Simulate delay

                //  Check if Customer exists
                var customerExists = await _context.Customers
                    .AnyAsync(c => c.CustomerId == saleDto.CustomerId && !c.IsDeleted);

                if (!customerExists)
                {
                    throw new InvalidOperationException($"Customer with ID {saleDto.CustomerId} does not exist.");
                }

                var sale = new Sale
                {
                    CustomerId = saleDto.CustomerId,
                    PaidAmount = saleDto.PaidAmount,
                    TotalAmount = saleDto.Items.Sum(i => i.Price * i.Quantity),
                };
                sale.DueAmount = sale.TotalAmount - sale.PaidAmount;

                foreach (var item in saleDto.Items)
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(p => p.ProductId == item.ProductId && !p.IsDeleted);

                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                    }

                    if (product.StockQty < item.Quantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}");
                    }

                    product.StockQty -= item.Quantity;

                    sale.SaleDetails.Add(new SaleDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {  Console.WriteLine(ex); return false; }
            finally
            {
                _saleLock.Release();
            }
        }




    }
}
