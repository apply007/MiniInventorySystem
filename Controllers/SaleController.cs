using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInventorySystem.Data;
using MiniInventorySystem.DTO;
using MiniInventorySystem.Interfaces;
using static MiniInventorySystem.DTO.SaleRequestDTO;

namespace MiniInventorySystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;
        private readonly ApplicationDbContext _context;

        public SaleController(ISaleService service, ApplicationDbContext context)
        {
            _service = service;
           _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] SaleRequestDto saleDto)
        {
            try
            {
               var saleStatus= await _service.CreateSaleAsync(saleDto);
                if (saleStatus)
                {                
                return Ok(new { message = "Sale completed successfully" });
                }
                return BadRequest();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(429, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("report")]
        public async Task<ActionResult<SalesReportDTO>> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            var sales = await _context.Sales
                .Include(s => s.SaleDetails)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToListAsync();

            var totalSalesQty = sales.SelectMany(s => s.SaleDetails).Sum(d => d.Quantity);
            var totalAmount = sales.Sum(s => s.TotalAmount);
            var totalDue = sales.Sum(s=>s.DueAmount);
            var numberOfTransactions = sales.Count;

            var report = new SalesReportDTO
            {
                TotalSales = (int)totalSalesQty,
                TotalRevenue = totalAmount-totalDue,
                NumberOfTransactions = numberOfTransactions
            };

            return Ok(report);
        }
    }
}
