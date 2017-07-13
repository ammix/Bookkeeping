//using System;
//using System.Collections.Generic;

//namespace DesktopBookkeepingClient
//{
//	public abstract class TreeListViewModel : ITreeListViewModel
//	{
//		public virtual string Tree { get; set; }
//		public virtual string Amount { get; set; } // Value, Sum
//		public virtual string Comment { get; set; } // Remark, Note
//		public virtual string Account { get; set; }
//		public virtual string Balance { get; set; }
//		public virtual string Time { get; set; }

//		public List<ITreeListViewModel> Children { get; set; }
//		public bool CanExpand => Children != null && Children.Count != 0;

//		#region TODO: move this in abstract class and this class do an interface
//		public virtual int? Id { get; set; }
//		public TreeListViewModel Parent { get; set; }
//		public virtual DateTime GetDate()
//		{
//			return Parent.GetDate();
//		}
//		public virtual void Add(TreeListViewModel model)
//		{
//			model.Parent = this;
//			if (Children == null)
//				Children = new List<ITreeListViewModel>();
//			Children.Add(model);
//		}

//		public virtual void Insert(int index, TreeListViewModel model)
//		{
//			model.Parent = this;
//			if (Children == null)
//				Children = new List<ITreeListViewModel>();
//			Children.Insert(index, model);
//		}
//		public virtual void Remove()
//		{
//			Parent.Children.Remove(this);
//			//CurrentItem.Parent.Children.Remove(CurrentItem);
//		}
//		#endregion
//	}
//}