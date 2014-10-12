using System.Threading.Tasks;

namespace WorkAround.Search.Contracts
{
    public interface ISearchProvider
    {
		string Name { get; }
	    SearchResult Search(SearchOptions options);
	    Task<SearchResult> SearchAsync(SearchOptions options);
    }
}
