using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class ProductRatingVmDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public int ProductId { get; set; }
    }
}
