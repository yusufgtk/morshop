using Microsoft.AspNetCore.Identity;

namespace morshop.app.Identity
{
    public class AppUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}