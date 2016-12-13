using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class TransactionView
	{
        public int Id;
        public int NestingLevel = 3;

        public string Time;
        public string Counterparty;        // Date, Counterparty, InvoiceLine
		public List<TransactionView> Nodes;
		public string Amount;
		public string Comment;
		public string Acount;
		public string Balance;
		public string Currency;

		public bool HasChildren => Nodes != null && Nodes.Count != 0;
	}
}
