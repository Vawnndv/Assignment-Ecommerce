using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Cart
{
    public class CreateCartRequestVmDto
    {
        public virtual ICollection<CreateCartItemRequestVmDto> CartItems { get; set; }
    }
}
