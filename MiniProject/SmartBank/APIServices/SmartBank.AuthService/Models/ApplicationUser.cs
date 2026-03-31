using Microsoft.AspNetCore.Identity;

namespace SmartBank.AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
