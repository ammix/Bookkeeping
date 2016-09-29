using Newtonsoft.Json;

namespace BookkeepingServer.Views
{
	public class InvoiceLine
	{
		public string Article;
		public string Price;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Note;
	}
}