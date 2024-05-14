using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
