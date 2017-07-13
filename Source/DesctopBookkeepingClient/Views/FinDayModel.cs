using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class FinDayModel : TreeListViewModel
	{
		string date;
		DateTime dateTime;
		//public DateTime Date;
		//public List<FinTransaction> FinTransactions;

		public FinDayModel(List<ITreeListViewModel> transactions, DateTime date)
		{
			//NestingLevel = NestingLevel.FinDay;
			Children = transactions;

			//this.dateTime = date;
			SetDate(date);

			foreach (var tr in transactions)
			{
				//Add(tr);
				(tr as TreeListViewModel).Parent = this;
			}
		}

		#region Implementation ITreeListViewModel
		public override string Tree
		{
			get { return date; }
		}
		#endregion

		public override DateTime GetDate()
		{
			//return dateTime != DateTime.MinValue ? dateTime : Parent.GetDate();
			return dateTime;
		}

		public void SetDate(DateTime date)
		{
			dateTime = date;
			var culture = CultureInfo.GetCultureInfo("uk-UA");
			this.date = dateTime.ToString("d MMMM yyyy (dddd)", culture);
		}
	}
}