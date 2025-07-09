using static MiniInventorySystem.DTO.SaleRequestDTO;

namespace MiniInventorySystem.Interfaces
{
    public interface ISaleService
    {
        Task<bool> CreateSaleAsync(SaleRequestDto saleDto);
    }
}
