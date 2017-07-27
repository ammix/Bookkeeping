using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class FinDayModel : TreeListViewModel
	{
		string date;
		DateTime dateTime;

		public FinDayModel(List<ITreeListViewModel> transactions, DateTime date)
		{
			Children = transactions;

			Date = date;

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

		public override DateTime Date
		{
			get
			{
				return dateTime;
			}
			protected set
			{
				dateTime = value;
				date = dateTime.ToString("d MMMM yyyy (dddd)", CultureInfo.GetCultureInfo("uk-UA"));
			}
		}
		#endregion
	}
}