namespace WorkAround.Search.Contracts
{
    public interface ISearchProvider
    {
		string Name { get; }
	    SearchResult Search(SearchOptions options);
    }
}
