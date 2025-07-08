using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; }=  string.Empty;
        public string Email { get; set; } = string.Empty;
        public int LoyaltyPoints { get; set; }
        public bool IsDeleted { get; set; } = false; // Soft delete
    }
}
