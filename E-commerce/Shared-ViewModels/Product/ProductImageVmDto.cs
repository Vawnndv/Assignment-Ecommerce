using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class ProductImageVmDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ProductTypeId { get; set; }
    }
}
