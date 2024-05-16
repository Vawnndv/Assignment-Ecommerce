using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class UpdateProductTypeVmDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30, ErrorMessage = "Type of product cannot be over 30 over characters")]
        public string Type { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Description of product type cannot be over 100 over characters")]
        public string Description { get; set; }

        [Required]
        public virtual List<UpdateProductImageVmDto> ProductImages { get; set; }
    }
}
