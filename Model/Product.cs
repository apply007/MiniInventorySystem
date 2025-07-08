
using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class Product
    {
        [Required]
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal StockQty { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool Status { get; set; } // Active/Inactive
        public bool IsDeleted { get; set; } = false; // Soft delete
    }
}
