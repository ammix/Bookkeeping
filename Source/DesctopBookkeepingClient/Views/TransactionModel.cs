using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel, ITreeListViewModel
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
		string ITreeListViewModel.Column1
		{
			get { return Counterparty; }
			set { Counterparty = value; }
		}

		string ITreeListViewModel.Column2
		{
			get { return column2; }
			set { Amount = decimal.Parse(value); }
		}

		string ITreeListViewModel.Column3
		{
			get { return Comment; }
			set { Comment = value; }
		}

		string ITreeListViewModel.Column4
		{
			get { return Account;  }
			set { Account = value; }
		}

		string ITreeListViewModel.Column5
		{
			get { return Balance; }
			set { Balance = value; }
		}

		string ITreeListViewModel.Column6
		{
			get { return Time; }
			set { Time = value; }
		}
		#endregion
	}
}
