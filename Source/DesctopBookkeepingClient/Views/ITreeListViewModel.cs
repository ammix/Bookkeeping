using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	public interface ITreeListViewModel
	{
		int? Id { get; }
		DateTime Date { get; }

		string Column1 { get; }
		string Column2 { get; } // Value, Sum
		string Comment { get; } // Remark, Note
		string Account { get; }
		string Balance { get; }
		string Time { get; }

		bool CanExpand { get; }
		ITreeListViewModel Parent { get; }
		List<ITreeListViewModel> Children { get; } //IList
		void AddChild(ITreeListViewModel child);

		//void AddChildren(List<ITreeListViewModel> children); //IList
		//void InsertChild(int index, ITreeListViewModel child);
	}
}