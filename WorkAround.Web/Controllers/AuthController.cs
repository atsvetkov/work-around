using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WorkAround.Web.Models;

namespace WorkAround.Web.Controllers
{
	[AllowAnonymous]
    public class AuthController : Controller
    {
		[HttpGet]
		public ActionResult Login(string returnUrl)
		{
			var model = new LoginModel
			{
				ReturnUrl = returnUrl
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (model.Email == "hello@world" && model.Password == "qwerty")
			{
				var identity = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Email, model.Email), 
					new Claim(ClaimTypes.Name, "John Smith")
				}, "ApplicationCookie");

				var context = Request.GetOwinContext();
				var authManager = context.Authentication;
				authManager.SignIn(identity);

				return Redirect(GetRedirectUrl(model.ReturnUrl));
			}

			ModelState.AddModelError("", "Invalid email or password");
			return View();
		}

		[HttpGet]
		public ActionResult Logoff()
		{
			var context = Request.GetOwinContext();
			var authManager = context.Authentication;
			authManager.SignOut("ApplicationCookie");

			return RedirectToAction("Login");
		}

		private string GetRedirectUrl(string url)
		{
			if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
			{
				return Url.Action("Index", "Search");
			}

			return url;
		}
    }
}
