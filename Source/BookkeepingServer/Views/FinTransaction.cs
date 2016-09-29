using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookkeepingServer.Views
{
	public class FinTransaction
	{
		public string Id;
		public string Counterparty;
		public string Account;
		public string Amount;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Note;
		public string Balance;
		public string Currency;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<InvoiceLine> InvoiceLines;
	}
}