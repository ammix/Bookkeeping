﻿using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class FinTransaction
	{
		public string Counterparty;
		public string Account;
		public string Amount;
		public string Note;
		public string Balance;
		public string Currency;
		public List<InvoiceLine> InvoiceLines;
	}
}