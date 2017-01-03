using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class RemoteDb
	{
		public static List<TreeListViewModel> GetTransactions()
		{
			var client = new RestClient("http://money.somee.com/");
			var request = new RestRequest("api/transactions/6");

			IRestResponse response = client.Get(request);
			var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

			var transactions = new List<TreeListViewModel>();
			foreach (var finDay in view)
			{
				transactions.Add(new TreeListViewModel(date: finDay.Date, transactions: ToView(finDay.FinTransactions)));
			}
			return transactions;
		}

		static List<TreeListViewModel> ToView(List<FinTransaction> trs)
		{
			var transaction = new List<TreeListViewModel>();
			foreach (var t in trs)
			{
				transaction.Add(new TreeListViewModel(
					counterparty: t.Counterparty,
					amount: t.Amount,
					comment: t.Note,
					account: t.Account,
					balance: t.Balance, /*Currency = t.Currency,*/
					articles: ToView(t.InvoiceLines)));
			}
			return transaction;
		}

		static List<TreeListViewModel> ToView(List<InvoiceLine> ils)
		{
			if (ils == null) return null;
			var transaction = new List<TreeListViewModel>();
			foreach (var i in ils)
			{
				transaction.Add(new TreeListViewModel(article: i.Article, price: i.Price, note: i.Note));
			}
			return transaction;
		}
	}
}
