using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	// Model for TreeListView control
	public abstract class TreeListViewModel : ITreeListViewModel
	{
		private DateTime? _dateTime;
		private decimal _value;

		#region Constructors
		protected TreeListViewModel() { }

		protected TreeListViewModel(List<ITreeListViewModel> children)
		{
			AddChildren(children);
		}
		#endregion

		public virtual int? Id { get; set; }
		public DateTime Date
		{
			get
			{
				return _dateTime ?? Parent.Date;
			}
			protected set
			{
				_dateTime = value;
				Column1 = _dateTime.Value.ToString("d MMMM yyyy (dddd)", CultureInfo.GetCultureInfo("uk-UA"));
			}
		}
		public decimal Value
		{
			get { return _value; }
			set
			{
				_value = value;
				Column2 = _value.ToString("N");
			}
		}

		public virtual string Column1 { get; set; }
		public virtual string Column2 { get; private set; } // Value, Sum -> Column2
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