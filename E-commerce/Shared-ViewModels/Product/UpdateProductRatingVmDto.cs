using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class UpdateProductRatingVmDto
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(100, ErrorMessage = "Description of product cannot be over 100 over characters")]
        public string Review { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}
