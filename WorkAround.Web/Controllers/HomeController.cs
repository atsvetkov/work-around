using System.Web.Mvc;
using WorkAround.Web.Models;

namespace WorkAround.Web.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		//
		// GET: /Home/
		public ActionResult Index(string k = null, string l = null)
		{
			return View(new SearchOptionsModel(k, l));
		}
	}
}