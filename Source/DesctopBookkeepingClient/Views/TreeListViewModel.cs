using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
    public class TreeListViewModel
    {
		public int Id;
		//public int UserId = 1;
		//public int TransacId;
		//public int CounterId;
		//public int ArticleId;
		//public int AccountId;
	    public string _date;
	    public TreeListViewModel _parent;

	    public string Date
	    {
			get
			{
				string q = !string.IsNullOrEmpty(_date) ? _date : _parent != null ? _parent.Date : null;
				if (q == null)
					return q;
				if (Nodes != null)
				{
					DateTime d = DateTime.Parse(q);
					return d.AddHours(Nodes.Count).ToString();
				}
				return q;
			}
			set { _date = value; }
	    }

	    public string Counterparty;
		public string Article;

		public NestingLevel NestingLevel = NestingLevel.InvoiceLine;
        public List<TreeListViewModel> Nodes;

		public string Tree => _date + Counterparty + Article;
        public string Amount;
        public string Comment;
        public string Account;
        public string Balance;
        public string Time;

        //static int id = 0;
        //static int NewId() => id++;

        public bool HasChildren => Nodes != null && Nodes.Count != 0;

        public TreeListViewModel(List<TreeListViewModel> transactions, string date)
        {
			NestingLevel = NestingLevel.FinDay;
            Nodes = transactions;
            Date = date;

			foreach (var tr in transactions)
				tr._parent = this;
        }

        public TreeListViewModel(List<TreeListViewModel> articles,
			int id,
            string counterparty,
            string amount,
            string account,
            string balance,
            string comment = null,
            string time = null)
        {
            NestingLevel = NestingLevel.Transaction;
            Nodes = articles;
			Id = id;

            Counterparty = counterparty;
            Amount = amount;
            Comment = comment;
            Account = account;
            Balance = balance;
            Time = time;
			
			if (articles != null)
				foreach (var ar in articles)
					ar._parent = this;
		}

        public TreeListViewModel(string article, string price, string note = null)
        {
            NestingLevel = NestingLevel.InvoiceLine;

            Article = article;
            Amount = price;
            Comment = note;
        }

		public void Add(TreeListViewModel model)
		{
			model._parent = this;
			Nodes.Add(model);
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
