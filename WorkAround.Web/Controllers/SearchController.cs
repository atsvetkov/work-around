using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkAround.Search;
using WorkAround.Search.Contracts;

namespace WorkAround.Web.Controllers
{
	[AllowAnonymous]
    public class SearchController : Controller
    {
		private ISearchProvider[] _searchProviders = { new FakeSearchProvider() };

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}