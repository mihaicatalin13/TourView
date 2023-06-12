using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TourView.Models
{
    public class ApplicationUser : IdentityUser
    {
        public static implicit operator ApplicationUser(ClaimsPrincipal v)
        {
            throw new NotImplementedException();
        }
    }
}
