using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel
	{
		private string column2;

		public TransactionModel() { }

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
			Amount = amount;
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
		}

		public string Counterparty { get; }
		public decimal Amount
		{
			get { return Value; }
			set
			{
				Value = value;
				column2 = value.ToString("N");
			}
		}

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Counterparty; }
		}

		public override string Column2
		{
			get { return column2; }
		}
		#endregion
	}
}
