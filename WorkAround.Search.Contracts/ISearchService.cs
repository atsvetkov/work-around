using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkAround.Search.Contracts
{
	public interface ISearchService
	{
		IEnumerable<SearchResultItem> Search(SearchOptions options);
		Task<IEnumerable<SearchResultItem>> SearchAsync(SearchOptions options);
	}
}