using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WorkAround.Data;
using WorkAround.Web.Models;

namespace WorkAround.Web.Controllers
{
	[AllowAnonymous]
    public class AuthController : Controller
    {
		private readonly UserManager<AppUser> _userManager;

		public AuthController()
			: this(Startup.UserManagerFactory.Invoke())
		{
		}

		public AuthController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

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
		public async Task<ActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var user = await _userManager.FindAsync(model.Email, model.Password);
			if (user != null)
			{
				var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
				Request.GetOwinContext().Authentication.SignIn(identity);

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

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var user = new AppUser
			{
				UserName = model.Email,
				Email = model.Email,
				Country = model.Country,
				WillingToRelocate = model.WillingToRelocate
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
				Request.GetOwinContext().Authentication.SignIn(identity);
				return RedirectToAction("index", "search");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}

			return View();
		}

		private string GetRedirectUrl(string url)
		{
			if (string.IsNullOrEmpty(url) || !Url.IsLocalUrl(url))
			{
				return Url.Action("Index", "Search");
			}

			return url;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _userManager != null)
			{
				_userManager.Dispose();
			}

			base.Dispose(disposing);
		}
    }
}
