using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Payment
{
    public class CreatePaymentRequestVmDto
    {
        [Required]
        public string PaymentMethod { get; set; } // COD, Card
    }
}
