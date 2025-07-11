using System.ComponentModel.DataAnnotations;

namespace MiniInventorySystem.Model
{
    public class Sale
    {
        public Sale()
        {
            //Customer    = new Customer();
            SaleDetails = new List<SaleDetail>();
        }



        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public int? CustomerId { get; set; }
        //public  Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
