using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
	public class TransactionModel : ITreeListViewModel
	{
		public string Counterparty;

		public override string Tree => Counterparty;

		//static int id = 0;
		//static int NewId() => id++;


		//public ITreeListViewModel(DateTime dateTime)
		//{
		//	NestingLevel = NestingLevel.FinDay;

		//	this.dateTime = dateTime;
		//	Children = new List<ITreeListViewModel>();
		//}

		//public TransactionModel(List<ITreeListViewModel> transactions, DateTime date)
		//{
		//	NestingLevel = NestingLevel.FinDay;
		//	Children = transactions;

		//	//this.dateTime = date;
		//	SetDate(date);

		//	foreach (var tr in transactions)
		//	{
		//		tr.Parent = this;
		//	}
		//}

		public TransactionModel(List<ITreeListViewModel> articles,
			string counterparty,
			string amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null)
		{
			Children = articles;
			Id = id;

			Counterparty = counterparty;
			Amount = amount;
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;

			if (articles != null)
				foreach (var ar in articles)
					ar.Parent = this;
		}
	}
}
