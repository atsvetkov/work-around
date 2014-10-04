using System.Configuration;
using System.Linq;
using System.Web.Http;
using WorkAround.Search.Contracts;
using WorkAround.Search.Providers;
using WorkAround.Web.Models;

namespace WorkAround.Web.Controllers
{
	public class SearchController : ApiController
	{
		private readonly ISearchProvider[] _searchProviders =
		{
			new ReedProvider(ConfigurationManager.AppSettings["ReedApiKey"]),
			new IndeedProvider(ConfigurationManager.AppSettings["IndeedPublisherId"]),
			new CareerBuilderProvider(ConfigurationManager.AppSettings["CareerBuilderDeveloperKey"])
		};
		
		public IHttpActionResult Get(string k = null, string l = null)
		{
			var options = new SearchOptions(k, l);
			var results = _searchProviders.Select(p => p.Search(options)).SelectMany(r => r.Items).Select(i => new SearchResultItemModel
			{
				Position = i.Position,
				Company = i.Company,
				Description = i.Description,
				Location = i.Location,
				Salary = i.Salary,
				JobUrl = i.JobUrl,
				SearchProviderName = i.SearchProviderName,
				ProviderLabelClassName = GetProviderLabelClassName(i.SearchProviderName)
			});

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