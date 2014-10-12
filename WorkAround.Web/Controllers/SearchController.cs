using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WorkAround.Search;
using WorkAround.Search.Contracts;
using WorkAround.Web.Models;

namespace WorkAround.Web.Controllers
{
	public class SearchController : ApiController
	{
		public async Task<IHttpActionResult> Get(string k = null, string l = null)
		{
			var options = new SearchOptions(k, l);

			ISearchService searchService = new SearchService(
				ConfigurationManager.AppSettings["ReedApiKey"],
				ConfigurationManager.AppSettings["IndeedPublisherId"],
				ConfigurationManager.AppSettings["CareerBuilderDeveloperKey"]);

			var resultItems = await searchService.SearchAsync(options);
			var results = resultItems.Select(i => new SearchResultItemModel
			{
				Position = i.Position,
				Company = i.Company,
				Description = i.Description,
				Location = i.Location,
				Salary = i.Salary,
				JobUrl = i.JobUrl,
				SearchProviderName = i.SearchProviderName,
				ProviderLabelClassName = GetProviderLabelClassName(i.SearchProviderName)
			}).OrderBy(item => item.Position);

			return Ok(results);
		}

		private static string GetProviderLabelClassName(string searchProviderName)
		{
			switch (searchProviderName)
			{
				case "CareerBuilder":
					return "ink-label blue";
				case "Reed.co.uk":
					return "ink-label red";
				case "Indeed":
					return "ink-label yellow";
				default:
					return "ink-label";
			}
		}
	}
}