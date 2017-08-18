using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
	public abstract class TreeListViewModel : ITreeListViewModel
	{
		protected decimal sum;

		#region Constructors
		protected TreeListViewModel() { }

		protected TreeListViewModel(List<ITreeListViewModel> children)
		{
			AddChildren(children);
		}
		#endregion

		public virtual int? Id { get; set; }
		public virtual DateTime Date
		{
			get { return Parent.Date; }
			protected set { }
		}
		public decimal Sum // Value
		{
			get { return sum; }
			set
			{
				sum = value; //TODO invoice price do not update automatically
				Amount = sum.ToString("N"); //TODO
			}
		}

		public virtual string Column1 { get; set; }
		public virtual string Amount { get; protected set; } // Value, Sum -> Column2
		public virtual string Comment { get; set; } // Remark, Note
		public virtual string Account { get; set; }
		public virtual string Balance { get; set; }
		public virtual string Time { get; set; }

		public bool CanExpand
		{
			get { return Children != null && Children.Count != 0; }
		}
		public ITreeListViewModel Parent { get; set; }
		public List<ITreeListViewModel> Children { get; set; } // private set

		public void AddChild(ITreeListViewModel model)
		{
			(model as TreeListViewModel).Parent = this;
			if (Children == null)
				Children = new List<ITreeListViewModel>();
			Children.Add(model);
		}

		public void InsertChild(int index, ITreeListViewModel model)
		{
			(model as TreeListViewModel).Parent = this;
			if (Children == null)
				Children = new List<ITreeListViewModel>();
			Children.Insert(index, model);
		}

		public void AddChildren(List<ITreeListViewModel> children) // IList
		{
			if (children == null) return; //TODO

			foreach (var child in children)
			{
				AddChild(child);
			}
		}

		public void RemoveChild()
		{
			Parent.Children.Remove(this);
			//Parent = null;
		}
	}
}