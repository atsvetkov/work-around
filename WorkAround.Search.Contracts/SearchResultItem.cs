namespace WorkAround.Search.Contracts
{
	public sealed class SearchResultItem
	{
		public SearchResultItem(string position, string company, string description, string salary, string location)
		{
			Position = position;
			Company = company;
			Description = description;
			Salary = salary;
			Location = location;
		}

		public string Position { get; private set; }
		public string Company { get; private set; }
		public string Location { get; private set; }
		public string Description { get; private set; }
		public string Salary { get; private set; }
	}
}