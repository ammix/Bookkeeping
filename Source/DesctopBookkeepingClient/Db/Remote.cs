using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class RemoteDb
	{
		public static List<TransactionView> GetTransactions()
		{
			var client = new RestClient("http://money.somee.com/");
			var request = new RestRequest("api/transactions/6");

			IRestResponse response = client.Get(request);
			var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

			var transactions = new List<TransactionView>();
			foreach (var finDay in view)
			{
				transactions.Add(new TransactionView { Counterparty = finDay.Date, Nodes = ToView(finDay.FinTransactions) });
			}
			return transactions;
		}

		static List<TransactionView> ToView(List<FinTransaction> trs)
		{
			var transaction = new List<TransactionView>();
			foreach (var t in trs)
			{
				transaction.Add(new TransactionView { Counterparty = t.Counterparty, Amount = t.Amount, Comment = t.Note, Acount = t.Account, Balance = t.Balance, Currency = t.Currency, Nodes = ToView(t.InvoiceLines) });
			}
			return transaction;
		}

		static List<TransactionView> ToView(List<InvoiceLine> ils)
		{
			if (ils == null) return null;
			var transaction = new List<TransactionView>();
			foreach (var i in ils)
			{
				transaction.Add(new TransactionView { Counterparty = i.Article, Amount = i.Price, Comment = i.Note });
			}
			return transaction;
		}
	}
}
