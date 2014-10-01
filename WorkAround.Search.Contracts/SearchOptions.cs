namespace WorkAround.Search.Contracts
{
	public sealed class SearchOptions
	{
		public SearchOptions(string keywords, string location)
		{
			Keywords = keywords;
			Location = location;
		}

		public string Keywords { get; private set; }
		public string Location { get; private set; }
	}
}