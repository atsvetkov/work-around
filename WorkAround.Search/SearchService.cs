using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkAround.Search.Contracts;
using WorkAround.Search.Providers;

namespace WorkAround.Search
{
	public sealed class SearchService : ISearchService
	{
		private readonly IEnumerable<ISearchProvider> _searchProviders;

		public SearchService(string reedApiKey, string indeedPublisherId, string careerBuilderDeveloperKey)
		{
			_searchProviders = new List<ISearchProvider>
			{
				new ReedProvider(reedApiKey),
				new IndeedProvider(indeedPublisherId),
				new CareerBuilderProvider(careerBuilderDeveloperKey)
			};
		}
		
		public IEnumerable<SearchResultItem> Search(SearchOptions options)
		{
			var tasks = _searchProviders.Select(p => Task.Run(() => p.Search(options)));
			var searchResults = Task.WhenAll(tasks).Result;

			return searchResults.SelectMany(r => r.Items);
		}
	}
}
