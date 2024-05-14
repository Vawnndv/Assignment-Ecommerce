using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ProductRating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Review { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
