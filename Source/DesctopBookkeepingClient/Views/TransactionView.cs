using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class TransactionView
	{
		public string Counterparty;        // Date, Counterparty, InvoiceLine
        public string Time;
		public List<TransactionView> Nodes;
		public string Amount;
		public string Comment;
		public string Acount;
		public string Balance;
		public string Currency;

		public bool HasChildren => Nodes != null && Nodes.Count != 0;
	}
}
