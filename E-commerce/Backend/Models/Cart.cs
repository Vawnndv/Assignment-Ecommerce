using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public decimal CalculateTotalAmount()
        {
            return CartItems.Sum(item => item.Quantity * item.UnitPrice);
        }
    }
}
