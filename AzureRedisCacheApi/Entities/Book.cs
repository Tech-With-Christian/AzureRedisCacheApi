namespace AzureRedisCacheApi.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public string Publisher { get; set; }
		public string Language { get; set; }
		public string Edition { get; set; }
		public DateTime Published { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
		public double Rating { get; set; }
    }
}
