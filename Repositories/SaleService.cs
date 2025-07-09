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

                var sale = new Sale
                {
                    CustomerId = saleDto.CustomerId,
                    PaidAmount = saleDto.PaidAmount,
                    TotalAmount = saleDto.Items.Sum(i => i.Price * i.Quantity),
                };
                sale.DueAmount = sale.TotalAmount - sale.PaidAmount;

                foreach (var item in saleDto.Items)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == item.ProductId && !p.IsDeleted);
                    if (product == null || product.StockQty < item.Quantity)
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
            finally
            {
                _saleLock.Release();
            }
        }



    }
}
