using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WorkAround.Search.Contracts;

namespace WorkAround.Search
{
    public class FakeSearchProvider : ISearchProvider
    {
	    readonly IEnumerable<SearchResultItem> _hardcodedPositions = new []
		    {
			    new SearchResultItem("Senior Software Developer", "Super Mega Corporation", "A senior software developer is desperately needed", "100000 USD", "New-York"),
			    new SearchResultItem("Software Engineer", "Typical Company Inc.", "We are hiring! Come join us!", "70000 USD", "Seattle"),
			    new SearchResultItem(".NET Application Developer", "Dream Team Gmbh.", "Das ist fantastisch", "60000 EUR", "Berlin"),
			    new SearchResultItem("Web Developer", "British Bank", "Looking for a bright graduate level web developer to work on our boring corporate web site", "30000 GBP", "London")
		    };

	    public string Name
	    {
		    get { return "Fake Provider"; }
	    }

	    public SearchResult Search(SearchOptions options)
	    {
			Thread.Sleep(500);

		    var items = string.IsNullOrWhiteSpace(options.Keywords) ? _hardcodedPositions : _hardcodedPositions.Where(p => p.Position.Contains(options.Keywords));

			return new SearchResult(items);
	    }
    }
}
