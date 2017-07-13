using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public abstract class ITreeListViewModel
	{
		public virtual string Tree { get; set; }
		public virtual string Amount { get; set; } // Value, Sum
		public virtual string Comment { get; set; } // Remark, Note
		public virtual string Account { get; set; }
		public virtual string Balance { get; set; }
		public virtual string Time { get; set; }

		public List<ITreeListViewModel> Children;
		public bool CanExpand => Children != null && Children.Count != 0;

		#region TODO: move this in abstract class and this class do an interface
		public virtual int? Id { get; set; }
		public ITreeListViewModel Parent { get; set; }
		#endregion

		#region This have to be deleted after refactoring
		public string _date;
		public DateTime dateTime;

		public DateTime GetDate()
		{
			return dateTime != DateTime.MinValue ? dateTime : Parent.GetDate();
		}

		public void SetDate(DateTime date)
		{
			dateTime = date;
			var culture = CultureInfo.GetCultureInfo("uk-UA");
			_date = dateTime.ToString("d MMMM yyyy (dddd)", culture);
		}

		public NestingLevel NestingLevel = NestingLevel.Transaction;
		#endregion
	}
}