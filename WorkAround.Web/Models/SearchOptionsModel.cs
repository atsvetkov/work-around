namespace WorkAround.Web.Models
{
	public class SearchOptionsModel
	{
		public SearchOptionsModel(string keywords, string location)
		{
			Keywords = keywords;
			Location = location;
		}

		public string Keywords { get; set; }
		public string Location { get; set; }
	}
}