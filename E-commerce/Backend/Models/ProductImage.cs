using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
    }
}
