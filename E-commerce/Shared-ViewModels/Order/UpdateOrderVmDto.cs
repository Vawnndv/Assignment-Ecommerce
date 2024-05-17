using Shared_ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Order
{
    public class UpdateOrderVmDto
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public virtual ICollection<UpdateOrderItemVmDto> OrderItems { get; set; }

        [Required]
        public virtual UpdatePaymentVmDto Payment { get; set; }
    }
}
