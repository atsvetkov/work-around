using System;
using System.Security.Claims;

namespace WorkAround.Data
{
	public class AppUserPrincipal : ClaimsPrincipal
	{
		public AppUserPrincipal(ClaimsPrincipal principal)
			: base(principal)
		{
			
		}

		public string Email
		{
			get { return this.FindFirst(ClaimTypes.Email).Value; }
		}

		public string Country
		{
			get { return this.FindFirst(ClaimTypes.Country).Value; }
		}

		public bool WillingToRelocate
		{
			get { return Convert.ToBoolean(this.FindFirst("Relocate").Value); }
		}
	}
}