using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using ServiceStack.Text;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
	public sealed class ReedProvider : ISearchProvider
	{
		private readonly string _apiKey;
		private const string BaseApiUrl = "http://www.reed.co.uk/api/1.0/search";

		public ReedProvider(string apiKey)
		{
			_apiKey = apiKey;
		}

		public string Name
		{
			get { return "Reed.co.uk"; }
		}

		public SearchResult Search(SearchOptions options)
		{
			string s = null;
			var uri = BuildUri(options);
			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			webRequest.Credentials = new NetworkCredential(_apiKey, string.Empty);
			using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
			using (var stream = webResponse.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				var content = reader.ReadToEnd();
				var searchResult = JsonSerializer.DeserializeFromString<ReedSearchResult>(content);
				var items =
					searchResult.results.Select(
						r => new SearchResultItem(
							r.jobTitle,
							r.employerName,
							WebUtility.HtmlDecode(r.jobDescription),
							r.minimumSalary + " - " + r.maximumSalary,
							r.locationName,
							Name));

				return new SearchResult(items);
			}
		}

		private Uri BuildUri(SearchOptions options)
		{
			var uriBuilder = new UriBuilder(BaseApiUrl)
			{
				Query = string.Format("keywords={0}&location={1}", options.Keywords, options.Location)
			};

			return uriBuilder.Uri;
		}
	}

	public class ReedSearchResult
	{
		public ReedSearchResultItem[] results { get; set; }
		public int totalResults { get; set; }
	}

	public class ReedSearchResultItem
	{
		public int jobId { get; set; }
		public int employerId { get; set; }
		public string employerName { get; set; }
		public int employerProfileId { get; set; }
		public object employerProfileName { get; set; }
		public string jobTitle { get; set; }
		public string locationName { get; set; }
		public float minimumSalary { get; set; }
		public float maximumSalary { get; set; }
		public string currency { get; set; }
		public string expirationDate { get; set; }
		public string date { get; set; }
		public string jobDescription { get; set; }
		public int applications { get; set; }
		public string jobUrl { get; set; }
	}
}