using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
	public abstract class BaseSearchProvider : ISearchProvider
	{
		public abstract string Name { get; }
		
		protected abstract Uri BuildUri(SearchOptions options);

		protected virtual HttpWebRequest CreateWebRequest(Uri uri)
		{
			return (HttpWebRequest)WebRequest.Create(uri);
		}

		protected abstract SearchResult DeserializeSearchResult(Stream stream);

		public virtual SearchResult Search(SearchOptions options)
		{
			var uri = BuildUri(options);
			var webRequest = CreateWebRequest(uri);

			using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
			using (var stream = webResponse.GetResponseStream())
			{
				return DeserializeSearchResult(stream);
			}
		}

		public virtual async Task<SearchResult> SearchAsync(SearchOptions options)
		{
			var uri = BuildUri(options);
			var webRequest = CreateWebRequest(uri);

			using (var webResponse = (HttpWebResponse) await webRequest.GetResponseAsync())
			using (var stream = webResponse.GetResponseStream())
			{
				return DeserializeSearchResult(stream);
			}
		}
	}
}