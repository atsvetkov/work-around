using System.Security.Claims;
using System.Web.Mvc;
using WorkAround.Data;

namespace WorkAround.Web.Controllers
{
	public class AppController : Controller
	{
		public AppUserPrincipal CurrentUser
		{
			get
			{
				return new AppUserPrincipal(this.User as ClaimsPrincipal);
			}
		}
	}
}