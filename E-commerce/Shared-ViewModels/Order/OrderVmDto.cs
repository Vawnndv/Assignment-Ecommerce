using Shared_ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Order
{
    public class OrderVmDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string AppUserId { get; set; }

        public virtual ICollection<OrderItemVmDto> OrderItems { get; set; }

        public virtual PaymentVmDto Payment { get; set; }
    }
}
