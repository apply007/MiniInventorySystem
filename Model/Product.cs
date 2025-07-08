
using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class Product
    {
        [Required]
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
        public string Barcode { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal StockQty { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; } // Active/Inactive
        public bool IsDeleted { get; set; } = false; // Soft delete
    }
}
