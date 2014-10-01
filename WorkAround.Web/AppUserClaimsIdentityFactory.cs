using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WorkAround.Data;

namespace WorkAround.Web
{
	public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<AppUser>
	{
		public override async Task<ClaimsIdentity> CreateAsync(UserManager<AppUser, string> manager, AppUser user, string authenticationType)
		{
			var identity = await base.CreateAsync(manager, user, authenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));
			identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
			identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
			identity.AddClaim(new Claim("Relocate", user.WillingToRelocate.ToString()));

			return identity;
		}
	}
}