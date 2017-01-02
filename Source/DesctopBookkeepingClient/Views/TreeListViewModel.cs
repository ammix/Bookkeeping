using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
    public class TreeListViewModel
    {
        public int Id;
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

        public TreeListViewModel()
        {
            //Id = NewId();
        }

        public TreeListViewModel(List<TreeListViewModel> transactions, string date, int id)
        {
			//Id = NewId();
			Id = id;
			NestingLevel = NestingLevel.FinDay;
            Nodes = transactions;

            Tree = date;
        }

        public TreeListViewModel(List<TreeListViewModel> articles,
            string counterparty,
            string amount,
            string account,
            string balance,
			int id,
            string comment = null,
            string time = null)
        {
            //Id = NewId();
	        //Id = id;
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
            //Id = NewId();
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
}
