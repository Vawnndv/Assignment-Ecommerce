using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Cart
{
    public class UpdateCartVmDto
    {
        public virtual ICollection<UpdateCartItemVmDto> CartItems { get; set; }
    }
}
