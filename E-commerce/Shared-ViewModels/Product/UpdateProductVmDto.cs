using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class UpdateProductVmDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name of product cannot be over 30 over characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Description of product cannot be over 100 over characters")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;

        [Required]
        public virtual List<UpdateProductTypeVmDto> ProductTypes { get; set; }
    }
}
