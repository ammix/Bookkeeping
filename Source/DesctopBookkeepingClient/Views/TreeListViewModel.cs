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
	    string _date;
	    TreeListViewModel Parent;

	    public string Date
	    {
		    get { return !string.IsNullOrEmpty(_date) ? _date : Parent.Date; }
		    set { _date = value; }
	    }

	    public string Counterparty;
		public string Article;

		public NestingLevel NestingLevel = NestingLevel.InvoiceLine;
        public List<TreeListViewModel> Nodes;

		public string Tree => Date + Counterparty + Article;
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

            Date = date;
        }

        public TreeListViewModel(List<TreeListViewModel> articles,
			TreeListViewModel parent,
            string counterparty,
            string amount,
            string account,
            string balance,
            string comment = null,
            string time = null)
        {
            NestingLevel = NestingLevel.Transaction;
            Nodes = articles;

            Counterparty = counterparty;
            Amount = amount;
            Comment = comment;
            Account = account;
            Balance = balance;
            Time = time;
        }

        public TreeListViewModel(string article, string price, string note = null)
        {
            NestingLevel = NestingLevel.InvoiceLine;

            Article = article;
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
