using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel
	{
		public TransactionModel() { }

		public TransactionModel(
			string counterparty,
			string amount,
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
			Amount = amount;
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
		}

		public string Counterparty { get; }
		//public string Amount { get; }

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Counterparty; }
		}
		#endregion
	}
}
