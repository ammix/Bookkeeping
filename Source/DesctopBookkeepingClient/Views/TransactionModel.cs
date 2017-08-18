﻿using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class TransactionModel : TreeListViewModel
	{
		public string Counterparty { get; private set; }

		public TransactionModel()
		{
			Amount = ""; // "0"
		}

		//public string Amount { get; private set; } // decimal

		public TransactionModel(
			string counterparty,
			decimal amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null)
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

			Counterparty = counterparty;
			sum = amount;
			Amount = amount.ToString("N");
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
		}

		#region Implementation ITreeListViewModel
		public override string Tree
		{
			get { return Counterparty; }
		}
		#endregion
	}
}
