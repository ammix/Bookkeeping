using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public class FinDayModel : TreeListViewModel
	{
		//string date;
		//DateTime dateTime;

		public FinDayModel(DateTime date, List<ITreeListViewModel> transactions=null) :
			base(transactions)
		{
			Date = date;
			//SetDate(date);
		}

		#region Implementation ITreeListViewModel
		//public override string Column1
		//{
		//	get { return date; }
		//}

		//public override DateTime Date
		//{
		//	get
		//	{
		//		return dateTime;
		//	}
		//	protected set
		//	{
		//		SetDate(value);
		//	}
		//}
		#endregion

		//void SetDate(DateTime value)
		//{
		//	dateTime = value;
		//	date = dateTime.ToString("d MMMM yyyy (dddd)", CultureInfo.GetCultureInfo("uk-UA"));
		//}
	}
}