using System.Web.Http;
using WorkAround.Search;
using WorkAround.Search.Contracts;

namespace WorkAround.Web.Controllers
{
	public class SearchController : ApiController
	{
		private ISearchProvider[] _searchProviders = { new FakeSearchProvider() };
		
		public IHttpActionResult Get(string k = null, string l = null)
		{
			return Ok(new FakeSearchProvider().Search(new SearchOptions(k, l)));
		}
	}
}