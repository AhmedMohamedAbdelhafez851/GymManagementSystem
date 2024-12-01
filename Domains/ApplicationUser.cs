using Microsoft.AspNetCore.Identity;

namespace Domains
{
    public class ApplicationUser : IdentityUser
    {
        // Add any custom properties you want here, such as FullName, etc.
        public string FullName { get; set; } = ""; 
    }
}
