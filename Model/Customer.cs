using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [MinLength(5,ErrorMessage ="Minimum length 5 characters"),MaxLength(30,ErrorMessage ="Max Length 30 characters")]
        public string FullName { get; set; } = string.Empty;
        [RegularExpression(@"^(017|013|018|015|016|019)[0-9]{8}$", ErrorMessage = "Not Valid Phone number")]
        public string Phone { get; set; }=  string.Empty;
        [EmailAddress ]
        public string Email { get; set; } = string.Empty;
        public int LoyaltyPoints { get; set; }
        public bool IsDeleted { get; set; } = false; // Soft delete
    }
}
