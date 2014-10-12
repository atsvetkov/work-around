using System;
using System.IO;
using System.Linq;
using System.Net;
using ServiceStack.Text;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
	public sealed class IndeedProvider : BaseSearchProvider
	{
		private readonly string _publisherId;
		private const string BaseApiUrl = "http://api.indeed.com/ads/apisearch";

		public IndeedProvider(string publisherId)
		{
			_publisherId = publisherId;
		}

		public override string Name
		{
			get { return "Indeed"; }
		}

		protected override Uri BuildUri(SearchOptions options)
		{
			var uriBuilder = new UriBuilder(BaseApiUrl)
			{
				Query = string.Format("q={0}&l={1}&publisher={2}&v=2&format=json&limit=5", options.Keywords, options.Location, _publisherId)
			};

			return uriBuilder.Uri;
		}

		protected override SearchResult DeserializeSearchResult(Stream stream)
		{
			var searchResult = JsonSerializer.DeserializeFromStream<IndeedSearchResult>(stream);
			var items =
				searchResult.results.Select(
					r => new SearchResultItem(
						r.jobtitle,
						r.company,
						WebUtility.HtmlDecode(r.snippet),
						string.Empty,
						r.city + ", " + r.country,
						r.url,
						Name));

			return new SearchResult(items);
		}
	}
	
	public class IndeedSearchResult
	{
		public int version { get; set; }
		public string query { get; set; }
		public string location { get; set; }
		public bool dupefilter { get; set; }
		public bool highlight { get; set; }
		public int radius { get; set; }
		public int start { get; set; }
		public int end { get; set; }
		public int totalResults { get; set; }
		public int pageNumber { get; set; }
		public IndeedSearchResultItem[] results { get; set; }
	}

	public class IndeedSearchResultItem
	{
		public string jobtitle { get; set; }
		public string company { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string country { get; set; }
		public string formattedLocation { get; set; }
		public string source { get; set; }
		public string date { get; set; }
		public string snippet { get; set; }
		public string url { get; set; }
		public string onmousedown { get; set; }
		public string jobkey { get; set; }
		public bool sponsored { get; set; }
		public bool expired { get; set; }
		public bool indeedApply { get; set; }
		public string formattedLocationFull { get; set; }
		public string formattedRelativeTime { get; set; }
	}
}