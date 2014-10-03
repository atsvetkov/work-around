using System.Web.Http;
using WorkAround.Search;
using WorkAround.Search.Contracts;
using WorkAround.Search.Providers;

namespace WorkAround.Web.Controllers
{
	public class SearchController : ApiController
	{
		private ISearchProvider[] _searchProviders = { new FakeSearchProvider() };
		
		public IHttpActionResult Get(string k = null, string l = null)
		{
			//return Ok(new FakeSearchProvider().Search(new SearchOptions(k, l)));
			return Ok(new ReedProvider("78a252ed-1863-4ccd-a910-536588ab0d7a").Search(new SearchOptions(k, l)));
		}
	}
}