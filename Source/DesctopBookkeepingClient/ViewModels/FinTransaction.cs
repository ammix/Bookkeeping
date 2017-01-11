using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class FinTransaction
	{
		public int Id;
		public string Time;
		public string Counterparty;
		public string Account;
		public string Amount;
		public string Note;
		public string Balance;
		public string Currency;
		public List<InvoiceLine> InvoiceLines;
	}
}