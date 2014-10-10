using System.Collections.Generic;

namespace WorkAround.Search.Contracts
{
	public interface ISearchService
	{
		IEnumerable<SearchResultItem> Search(SearchOptions options);
	}
}