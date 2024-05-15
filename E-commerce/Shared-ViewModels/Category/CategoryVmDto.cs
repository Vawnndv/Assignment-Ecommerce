using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Category
{
    public class CategoryVmDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public virtual CategoryVmDto ParentCategory { get; set; }

        public virtual ICollection<CategoryVmDto> SubCategories { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}
