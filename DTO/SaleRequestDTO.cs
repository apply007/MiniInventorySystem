namespace MiniInventorySystem.DTO
{
    public class SaleRequestDTO
    {
        public class SaleRequestDto
        {
            public int? CustomerId { get; set; }
            public decimal PaidAmount { get; set; }

            public List<SaleItemDto> Items { get; set; } = new();
        }

        public class SaleItemDto
        {
            public int ProductId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}
