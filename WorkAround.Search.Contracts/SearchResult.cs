using System.Collections.Generic;
using System.Linq;

namespace WorkAround.Search.Contracts
{
	public sealed class SearchResult
	{
		public SearchResult(IEnumerable<SearchResultItem> items)
		{
			Items = items.ToArray();
		}

		public SearchResultItem[] Items { get; private set; }
	}
}