using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkAround.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext()
			: base("DefaultConnection")
		{
		}
	}
}