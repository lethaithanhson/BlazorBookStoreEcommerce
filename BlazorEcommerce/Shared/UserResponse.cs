using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Address? Address { get; set; }
        public string Role { get; set; } = "Customer";
    }
}
