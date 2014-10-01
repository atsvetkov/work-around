using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkAround.Data
{
    public class AppUser : IdentityUser
    {
		public string Country { get; set; }
		public bool WillingToRelocate { get; set; }
    }
}
