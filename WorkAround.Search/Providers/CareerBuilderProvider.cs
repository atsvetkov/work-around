using System;
using System.Linq;
using System.Net;
using System.Xml;
using WorkAround.Search.Contracts;

namespace WorkAround.Search.Providers
{
	public sealed class CareerBuilderProvider : BaseSearchProvider
	{
		private readonly string _developerKey;
		private const string BaseApiUrl = "http://api.careerbuilder.com/v2/jobsearch";

		public CareerBuilderProvider(string developerKey)
		{
			_developerKey = developerKey;
		}

		public override string Name
		{
			get { return "CareerBuilder"; }
		}

		protected override Uri BuildUri(SearchOptions options)
		{
			var uriBuilder = new UriBuilder(BaseApiUrl)
			{
				Query = string.Format("keywords={0}&location={1}&developerkey={2}&v=2&format=json&perpage=5&pagenumber=", options.Keywords, options.Location, _developerKey)
			};

			return uriBuilder.Uri;
		}

		public override SearchResult Search(SearchOptions options)
		{
			var uri = BuildUri(options);
			var webRequest = (HttpWebRequest)WebRequest.Create(uri);
			using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
			using (var stream = webResponse.GetResponseStream())
			using (var xmlReader = XmlReader.Create(stream))
			{
				var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ResponseJobSearch));
				var searchResult = (ResponseJobSearch)xmlSerializer.Deserialize(xmlReader);
				var items =
					searchResult.Results.Select(
						r => new SearchResultItem(
							r.JobTitle,
							r.Company,
							r.DescriptionTeaser,
							r.Pay,
							r.Location,
							r.JobDetailsURL,
							Name));

				return new SearchResult(items);
			}
		}
	}

	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public class ResponseJobSearch
	{
		public object Errors { get; set; }
		public string TimeResponseSent { get; set; }
		public decimal TimeElapsed { get; set; }
		public ushort TotalPages { get; set; }
		public uint TotalCount { get; set; }
		public byte FirstItemIndex { get; set; }
		public byte LastItemIndex { get; set; }
		public ResponseJobSearchSearchMetaData SearchMetaData { get; set; }

		[System.Xml.Serialization.XmlArrayItemAttribute("JobSearchResult", IsNullable = false)]
		public ResponseJobSearchJobSearchResult[] Results { get; set; }
	}
	
	public class ResponseJobSearchJobSearchResult
	{
		public string Company { get; set; }
		public string CompanyDID { get; set; }
		public string CompanyDetailsURL { get; set; }
		public string DID { get; set; }
		public string OnetCode { get; set; }
		public string ONetFriendlyTitle { get; set; }
		public string DescriptionTeaser { get; set; }
		public object Distance { get; set; }
		public string EmploymentType { get; set; }
		public string EducationRequired { get; set; }
		public string ExperienceRequired { get; set; }
		public string JobDetailsURL { get; set; }
		public string JobServiceURL { get; set; }
		public string Location { get; set; }
		public string DisplayCity { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public decimal LocationLatitude { get; set; }
		public decimal LocationLongitude { get; set; }
		public string PostedDate { get; set; }
		public string PostedTime { get; set; }
		public string Pay { get; set; }
		public string SimilarJobsURL { get; set; }
		public string JobTitle { get; set; }
		public string CompanyImageURL { get; set; }
		public string JobBrandingIcons { get; set; }
		public ResponseJobSearchJobSearchResultApplyRequirements ApplyRequirements { get; set; }
		public ResponseJobSearchJobSearchResultSkills Skills { get; set; }
	}
	
	public class ResponseJobSearchJobSearchResultApplyRequirements
	{
		public string ApplyRequirement { get; set; }
	}
	
	public class ResponseJobSearchJobSearchResultSkills
	{
		public string Skill { get; set; }
	}
	
	public class ResponseJobSearchSearchMetaData
	{
		public bool ResultsAlteredByUsersSearchPreferences { get; set; }
		public ResponseJobSearchSearchMetaDataSearchLocations SearchLocations { get; set; }
	}
	
	public class ResponseJobSearchSearchMetaDataSearchLocations
	{
		public ResponseJobSearchSearchMetaDataSearchLocationsSearchLocation SearchLocation { get; set; }
	}
	
	public class ResponseJobSearchSearchMetaDataSearchLocationsSearchLocation
	{
		public object City { get; set; }
		public object StateCode { get; set; }
		public string CountryCode { get; set; }
		public object PostalCode { get; set; }
		public string Valid { get; set; }
	}
}