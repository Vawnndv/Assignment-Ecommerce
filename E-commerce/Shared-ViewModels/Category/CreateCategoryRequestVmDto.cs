using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Category
{
    public class CreateCategoryRequestVmDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Category cannot be over 30 over characters")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
