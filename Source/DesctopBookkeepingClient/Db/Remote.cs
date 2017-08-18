using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	class RemoteDb
	{
		public static List<ITreeListViewModel> GetTransactions()
		{
			var client = new RestClient("http://localhost"); //http://money.somee.com/
			var request = new RestRequest("api/transactions/1");

			IRestResponse response = client.Get(request);
			var view = JsonConvert.DeserializeObject<List<FinDay>>(response.Content);

			var transactions = new List<ITreeListViewModel>();
			foreach (var finDay in view)
			{
				transactions.Add(new FinDayModel(date: finDay.Date, transactions: ToView(finDay.FinTransactions)));
			}
			return transactions;
		}

		static List<ITreeListViewModel> ToView(List<FinTransaction> trs)
		{
			var transaction = new List<ITreeListViewModel>();
			foreach (var t in trs)
			{
				var transactionModel = new TransactionModel(
					id: t.Id,
					counterparty: t.Counterparty,
					amount: decimal.Parse(t.Amount), //TODO
					comment: t.Note,
					account: t.Account,
					balance: t.Balance /*Currency = t.Currency,*/
					);
				transactionModel.AddChildren(ToView(t.InvoiceLines));
				transaction.Add(transactionModel);
			}
			return transaction;
		}

		static List<ITreeListViewModel> ToView(List<InvoiceLine> ils)
		{
			if (ils == null) return null;
			var transaction = new List<ITreeListViewModel>();
			foreach (var i in ils)
			{
				transaction.Add(new InvoiceLineModel(article: i.Article, price: decimal.Parse(i.Price) /*TODO*/, note: i.Note));
			}
			return transaction;
		}
	}
}
