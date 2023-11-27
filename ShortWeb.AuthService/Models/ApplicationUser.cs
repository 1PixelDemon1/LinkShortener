using Microsoft.AspNetCore.Identity;

namespace ShortWeb.AuthService.Models
{
    // After extending IdentityUser EF.Core will provide
    // bd with additional information from this class.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
