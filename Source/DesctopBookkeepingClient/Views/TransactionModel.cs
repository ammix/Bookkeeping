using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel
	{
		private decimal amount;
		private string column2;

		#region Constructors
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
		#endregion

		public string Counterparty { get; private set; }

		public decimal Amount
		{
			get { return amount; }
			set
			{
				amount = value;
				column2 = value.ToString("N");
			}
		}

		public string Comment { get; protected set; }

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Counterparty; }
			set { Counterparty = value; }
		}

		public override string Column2
		{
			get { return column2; }
			set { Amount = decimal.Parse(value); }
		}

		public override string Column3
		{
			get { return Comment; }
			set { Comment = value; }
		}
		#endregion
	}
}
