namespace DesktopBookkeepingClient
{
	public class InvoiceLineModel : TreeListViewModel
	{
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

		public string Article { get; private set; }
		public decimal Price
		{
			get { return Value; }
			private set
			{
				Value = value;
			}
		}
		public string Note { get; private set; }

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Article; }
			set { Article = value; }
		}

		public override string Column3
		{
			get { return Note; }
			set { Note = value; }
		}
		#endregion

		public int ParentTransactionId => Parent.Id.Value;
	}
}