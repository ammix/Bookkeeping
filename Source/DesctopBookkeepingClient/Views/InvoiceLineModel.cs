namespace DesktopBookkeepingClient
{
	public class InvoiceLineModel : TreeListViewModel
	{
		string amount;

		public InvoiceLineModel(ITreeListViewModel parent)
		{
			Parent = parent;
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
			get { return sum; }
			private set
			{
				sum = value;
				amount = sum.ToString("N");
			}
		}

		public string Note { get; private set; }

		#region Implementation ITreeListViewModel
		public override string Tree
		{
			get { return Article; }
			set { Article = value; }
		}

		public override string Amount
		{
			//get { return amount; }
			get { return sum.ToString("N"); }//TODO
			//set { Price = value; }
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