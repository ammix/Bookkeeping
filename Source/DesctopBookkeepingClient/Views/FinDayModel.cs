using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public class FinDayModel : ITreeListViewModel
	{
		public DateTime Date;
		//public List<FinTransaction> FinTransactions;
	}
}


//public TreeListViewModelConcrete(List<ITreeListViewModel> transactions, DateTime date)
//{
//	NestingLevel = NestingLevel.FinDay;
//	Children = transactions;

//	//this.dateTime = date;
//	SetDate(date);

//	foreach (var tr in transactions)
//	{
//		tr.Parent = this;
//	}
//}

//#region This have to be deleted after refactoring
//public string _date;
//public DateTime dateTime;

//public DateTime GetDate()
//{
//	return dateTime != DateTime.MinValue ? dateTime : Parent.GetDate();
//}
//public NestingLevel NestingLevel = NestingLevel.InvoiceLine;
//#endregion