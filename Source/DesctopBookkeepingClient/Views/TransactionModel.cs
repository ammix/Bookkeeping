using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel
	{
		public TransactionModel() { }

		//public string Amount { get; private set; } // decimal

		public TransactionModel(
			string counterparty,
			decimal amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null,
			List<ITreeListViewModel> articles = null)
			: base(articles)
		{
			Id = id;

			Counterparty = counterparty;
			sum = amount;
			Amount = amount.ToString("N");
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
		}

		public TransactionModel(List<ITreeListViewModel> articles,
			string counterparty,
			decimal amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null): base(articles)
		{
			Id = id;
		public string Counterparty { get; }
		//public string Amount { get; }

			Counterparty = counterparty;
			sum = amount;
			Amount = amount.ToString("N");
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
		}

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Counterparty; }
		}
		#endregion
	}
}
