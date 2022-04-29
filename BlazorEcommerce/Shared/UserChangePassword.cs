using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class UserChangePassword
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; } = string.Empty;
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
