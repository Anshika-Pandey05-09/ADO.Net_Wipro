using Microsoft.AspNetCore.Identity;

namespace Day38_SecureShoppingAssignment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = "Customer";
    }
}