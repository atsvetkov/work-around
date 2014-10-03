using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
    public sealed class FakeSearchProvider : ISearchProvider
    {
		readonly IEnumerable<SearchResultItem> _hardcodedPositions;

	    public FakeSearchProvider()
	    {
			_hardcodedPositions = new[]
		    {
			    new SearchResultItem("Senior Software Developer", "Super Mega Corporation", "A senior software developer is desperately needed", "100000 USD", "New-York", Name),
			    new SearchResultItem("Software Engineer", "Typical Company Inc.", "We are hiring! Come join us!", "70000 USD", "Seattle", Name),
			    new SearchResultItem(".NET Application Developer", "Dream Team Gmbh.", "Das ist fantastisch", "60000 EUR", "Berlin", Name),
			    new SearchResultItem("Web Developer", "British Bank", "Looking for a bright graduate level web developer to work on our boring corporate web site", "30000 GBP", "London", Name)
		    };
	    }
		
	    public string Name
	    {
		    get { return "Fake Provider"; }
	    }

	    public SearchResult Search(SearchOptions options)
	    {
			Thread.Sleep(500);

		    var items = _hardcodedPositions;
		    if (!string.IsNullOrWhiteSpace(options.Keywords))
		    {
				var keywords = options.Keywords.Trim().ToLower();
			    items = items.Where(p => p.Position.ToLower().Contains(keywords));
		    }

		    if (!string.IsNullOrWhiteSpace(options.Location))
			{
				var location = options.Location.Trim().ToLower();
				items = items.Where(p => p.Location.ToLower().Contains(location));
			}

			return new SearchResult(items);
	    }
    }
}
