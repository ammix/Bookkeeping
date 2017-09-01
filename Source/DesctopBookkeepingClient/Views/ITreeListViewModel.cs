using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public interface ITreeListViewModel
	{
		int? Id { get; }
		DateTime Date { get; }

		string Column1 { get; set; } // Tree: Date, Counterparty, Article
		string Column2 { get; set; } // Value: DaySum, Amount, Price
		string Column3 { get; set; } // Remark: Comment, Note
		string Column4 { get; set; } // Balance
		string Column5 { get; set; } // Account
		string Column6 { get; set; } // Time

		bool CanExpand { get; }
		ITreeListViewModel Parent { get; }
		List<ITreeListViewModel> Children { get; } //IList
		void AddChild(ITreeListViewModel child);

		//void AddChildren(List<ITreeListViewModel> children); //IList
		//void InsertChild(int index, ITreeListViewModel child);
	}
}