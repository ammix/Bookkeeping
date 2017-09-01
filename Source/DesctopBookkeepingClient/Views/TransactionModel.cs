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
			: base(id, articles)
		{
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
		public string Account { get; protected set; }
		public string Balance { get; private set; }
		public string Time { get; private set; }

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

		public override string Column4
		{
			get { return Balance; }
			set { Balance = value; }
		}

		public override string Column5
		{
			get { return Account; }
			set { Account = value; }
		}

		public override string Column6
		{
			get { return Time; }
			set { Time = value; }
		}
		#endregion
	}
}
