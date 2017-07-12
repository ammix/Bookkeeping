using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
	public class TreeListViewModelConcrete: ITreeListViewModel
	{
		public void SetDate(DateTime date)
		{
			dateTime = date;
			var culture = CultureInfo.GetCultureInfo("uk-UA");
			_date = dateTime.ToString("d MMMM yyyy (dddd)", culture);
		}

		public string Counterparty;
		public string Article;


		public override string Tree => _date + Counterparty + Article;

		//static int id = 0;
		//static int NewId() => id++;


		//public ITreeListViewModel(DateTime dateTime)
		//{
		//	NestingLevel = NestingLevel.FinDay;

		//	this.dateTime = dateTime;
		//	Children = new List<ITreeListViewModel>();
		//}

		public TreeListViewModelConcrete(List<ITreeListViewModel> transactions, DateTime date)
		{
			NestingLevel = NestingLevel.FinDay;
			Children = transactions;

			//this.dateTime = date;
			SetDate(date);

			foreach (var tr in transactions)
			{
				tr.Parent = this;
			}
		}

		public TreeListViewModelConcrete(List<ITreeListViewModel> articles,
			string counterparty,
			string amount,
			string account,
			string balance,
			string comment = null,
			string time = null,
			int? id = null)
		{
			NestingLevel = NestingLevel.Transaction;
			Children = articles;
			Id = id;

			Counterparty = counterparty;
			Amount = amount;
			Comment = comment;
			Account = account;
			Balance = balance;
			Time = time;
			
			if (articles != null)
				foreach (var ar in articles)
					ar.Parent = this;
		}

		public void Add(ITreeListViewModel model)
		{
			model.Parent = this;
			if (Children == null)
				Children = new List<ITreeListViewModel>();
			Children.Add(model);
		}

		public void Insert(int index, ITreeListViewModel model)
		{
			model.Parent = this;
			if (Children == null)
				Children = new List<ITreeListViewModel>();
			Children.Insert(index, model);
		}

		public void Remove()
		{
			Parent.Children.Remove(this);
			//CurrentItem.Parent.Children.Remove(CurrentItem);
		}

	}

	public enum NestingLevel
	{
		FinDay,
		Transaction,
		InvoiceLine
	}
}
