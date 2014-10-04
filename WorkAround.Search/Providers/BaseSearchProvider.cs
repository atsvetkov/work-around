using System;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
	public abstract class BaseSearchProvider : ISearchProvider
	{
		public abstract string Name { get; }
		
		protected abstract Uri BuildUri(SearchOptions options);

		public abstract SearchResult Search(SearchOptions options);
	}
}