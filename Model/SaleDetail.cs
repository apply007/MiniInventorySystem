using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class SaleDetail
    {
        [Key]
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
