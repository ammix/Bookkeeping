using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
    public class TreeListViewModel
    {
		//public int Id;
		//public int UserId = 1;
		//public int TransacId;
		//public int CounterId;
		//public int ArticleId;
		//public int AccountId;

		public DateTime Date;
		string _counterparty;
		string _article;

		public NestingLevel NestingLevel = NestingLevel.InvoiceLine;
        public List<TreeListViewModel> Nodes;

        public string Tree;   // Date, Counterparty, Article
        public string Amount;
        public string Comment;
        public string Account;
        public string Balance;
        public string Time;

        static int id = 0;
        static int NewId() => id++;

        public bool HasChildren => Nodes != null && Nodes.Count != 0;

        public TreeListViewModel(List<TreeListViewModel> transactions, string date)
        {
			NestingLevel = NestingLevel.FinDay;
            Nodes = transactions;

            Tree = date;

			DateTime.TryParse(date, out Date);
        }

        public TreeListViewModel(List<TreeListViewModel> articles,
            string counterparty,
            string amount,
            string account,
            string balance,
            string comment = null,
            string time = null)
        {
            NestingLevel = NestingLevel.Transaction;
            Nodes = articles;

            Tree = counterparty;
            Amount = amount;
            Comment = comment;
            Account = account;
            Balance = balance;
            Time = time;
        }

        public TreeListViewModel(string article, string price, string note = null)
        {
            NestingLevel = NestingLevel.InvoiceLine;

            Tree = article;
            Amount = price;
            Comment = note;
        }
    }

    public enum NestingLevel
    {
        FinDay,
        Transaction,
        InvoiceLine
    }

	public class AccountModel
	{
		public int Id;
		public string Account;
	}
}
