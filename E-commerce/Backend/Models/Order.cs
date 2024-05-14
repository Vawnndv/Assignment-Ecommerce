using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual Payment Payment { get; set; }
    }
}
