namespace DesktopBookkeepingClient
{
	public class InvoiceLineModel : TreeListViewModel
	{
		public InvoiceLineModel()
		{
		}

		public InvoiceLineModel(string article, string price, string note = null, int? id = null)
		{
			this.Id = id;
			this.Article = article;
			this.Price = price;
			this.Note = note;
		}

		public string Article { get; private set; }
		public string Price { get; private set; }
		public string Note { get; private set; }

		#region Implementation ITreeListViewModel
		public override string Column1
		{
			get { return Article; }
			set { Article = value; }
		}

		public override string Amount
		{
			get { return Price; }
			set { Price = value; }
		}

		public override string Comment
		{
			get { return Note; }
			set { Note = value; }
		}
		#endregion

		public int ParentTransactionId => Parent.Id.Value;
	}
}