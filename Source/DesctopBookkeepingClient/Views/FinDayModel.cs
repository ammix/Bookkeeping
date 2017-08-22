using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class FinDayModel : TreeListViewModel
	{
		private string date;
		private DateTime dateTime;

		public FinDayModel(DateTime date, List<ITreeListViewModel> transactions=null) :
			base(null, transactions)
		{
			SetDate(date);
		}

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return date; }
			set { }
		}

		public override DateTime Date
		{
			get
			{
				return dateTime;
			}
			protected set
			{
				SetDate(value);
			}
		}
		#endregion

		private void SetDate(DateTime value)
		{
			dateTime = value;
			date = dateTime.ToString("d MMMM yyyy (dddd)", CultureInfo.GetCultureInfo("uk-UA"));
		}
	}
}