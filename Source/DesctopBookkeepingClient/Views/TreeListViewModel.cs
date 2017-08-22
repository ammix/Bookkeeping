using System;
using System.Collections.Generic;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
	public abstract class TreeListViewModel: ITreeListViewModel
	{
		#region Constructors
		protected TreeListViewModel() { }

		protected TreeListViewModel(int? id, List<ITreeListViewModel> children)
		{
			Id = id;
			AddChildren(children);
		}
		#endregion

		public int? Id { get; set; }
		public virtual DateTime Date
		{
			get { return Parent.Date; }
			protected set { }
		}

		public virtual string Column1 { get; set; }
		public virtual string Column2 { get; set; }
		public virtual string Column3 { get; set; }
		public virtual string Column4 { get; set; }
		public virtual string Column5 { get; set; }
		public virtual string Column6 { get; set; }

		public bool CanExpand
		{
			get { return Children != null && Children.Count != 0; }
		}
		public ITreeListViewModel Parent { get; set; }
		public List<ITreeListViewModel> Children { get; set; } // private set

		public void AddChild(ITreeListViewModel model)
		{
			((TreeListViewModel)model).Parent = this;
			if (Children == null)
				Children = new List<ITreeListViewModel>();
			Children.Add(model);
		}

		public void InsertChild(int index, ITreeListViewModel model)
		{
			((TreeListViewModel)model).Parent = this;
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