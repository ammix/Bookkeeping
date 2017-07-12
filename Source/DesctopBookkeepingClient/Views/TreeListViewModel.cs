using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	//public class TreeListViewModel
	//{
	//	public string Tree;
	//	public string Amount;
	//	public string Comment;
	//	public string Account;
	//	public string Balance;
	//	public string Time;
	//}

	public class Line
	{
		public string Tree;
		public string Amount;
		public string Comment;
		public string Account;
		public string Balance;
		public string Time;
	}

	// Model for TreeListView control
	public class TreeListViewModel //2: TreeListViewModel
	{
		//public int? Id;
		//public int UserId = 1;
		//public int TransacId;
		//public int CounterId;
		//public int ArticleId;
		//public int AccountId;
		public string _date;
		public DateTime dateTime;
		public TreeListViewModel _parent;

		public DateTime GetDate()
		{
			return dateTime != DateTime.MinValue? dateTime: _parent.GetDate();
		}

		public void SetDate(DateTime date)
		{
			dateTime = date;
			var culture = CultureInfo.GetCultureInfo("uk-UA");
			_date = dateTime.ToString("d MMMM yyyy (dddd)", culture);
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

		//public TreeListViewModel(DateTime dateTime)
		//{
		//	NestingLevel = NestingLevel.FinDay;

		//	this.dateTime = dateTime;
		//	Nodes = new List<TreeListViewModel>();
		//}

		public TreeListViewModel(List<TreeListViewModel> transactions, DateTime date)
		{
			NestingLevel = NestingLevel.FinDay;
			Nodes = transactions;

			//this.dateTime = date;
			SetDate(date);

			foreach (var tr in transactions)
			{
				tr._parent = this;
			}
		}

		public TreeListViewModel(List<TreeListViewModel> articles,
			string counterparty,
			string amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null)
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

		public TreeListViewModel(string article, string price, string note = null, int? id = null)
		{
			NestingLevel = NestingLevel.InvoiceLine;

			Article = article;
			Amount = price;
			Comment = note;

			Id = id;
		}

		public void Add(TreeListViewModel model)
		{
			model._parent = this;
			if (Nodes == null)
				Nodes = new List<TreeListViewModel>();
			Nodes.Add(model);
		}

		public void Insert(int index, TreeListViewModel model)
		{
			model._parent = this;
			if (Nodes == null)
				Nodes = new List<TreeListViewModel>();
			Nodes.Insert(index, model);
		}

		public void Remove()
		{
			_parent.Nodes.Remove(this);
			//CurrentItem._parent.Nodes.Remove(CurrentItem);
		}
	}

	public enum NestingLevel
	{
		FinDay,
		Transaction,
		InvoiceLine
	}
}
