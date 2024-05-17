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
    public class CreateOrderRequestVmDto
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public virtual ICollection<CreateOrderItemRequestVmDto> OrderItems { get; set; }

        [Required]
        public virtual CreatePaymentRequestVmDto Payment { get; set; }
    }
}
