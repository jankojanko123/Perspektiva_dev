using Microsoft.AspNetCore.Identity;

namespace Perspektiva.Models
{
    public class ApplicationUser: IdentityUser
    {
        
        
        
        public string? FirstName { get; set; } // TODO: make editable
        public string? LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[]? ProfilePicture { get; set; }
    }
}
