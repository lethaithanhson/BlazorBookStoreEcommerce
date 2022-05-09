using BlazorEcommerce.Shared.Enum;

namespace BlazorEcommerce.Shared
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Address? Address { get; set; }
        public string Role { get; set; }
        public AccountStatus Status { get; set; }
    }
}
