namespace DesktopBookkeepingClient
{
	public class InvoiceLineModel : TreeListViewModel, ITreeListViewModel
	{
		private decimal price;
		private string column2;

		#region Constructors
		public InvoiceLineModel()
		{
		}

		public InvoiceLineModel(string article, decimal price, string note = null, int? id = null)
		{
			Id = id;
			Article = article;
			Price = price;
			Note = note;
		}
		#endregion

		public string Article { get; private set; }
		public decimal Price
		{
			get { return price; }
			set
			{
				price = value;
				column2 = value.ToString("N");
			}
		}
		public string Note { get; private set; }

		#region Implementation ITreeListViewModel
		string ITreeListViewModel.Column1
		{
			get { return Article; }
			set { Article = value; }
		}

		string ITreeListViewModel.Column2
		{
			get { return column2; }
			set { Price = decimal.Parse(value); }
		}

		string ITreeListViewModel.Column3
		{
			get { return Note; }
			set { Note = value; }
		}
		#endregion

		public int ParentTransactionId => Parent.Id.Value;
	}
}