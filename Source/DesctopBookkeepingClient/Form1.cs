using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace DesktopBookkeepingClient
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			InitializeTreeListView();
		}

		private void InitializeTreeListView()
		{
			treeListView.CanExpandGetter = model => ((TransactionView)model).HasChildren;
			treeListView.ChildrenGetter = model => ((TransactionView) model).Children;
			treeListView.Roots = TransactionView.GetTransactionsFromDb();

            treeListView.TreeColumnRenderer.IsShowLines = false;
            treeListView.TreeColumnRenderer.UseTriangles = true;
            treeListView.FullRowSelect = true;
            treeListView.UseCellFormatEvents = true;
		}


		private void treeListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
		{
			var cell = (TransactionView)e.Model;

			//var decoration = new TextDecoration("ABCDEF", 255);
			//decoration.Font = new Font(Font.Name, Font.SizeInPoints +2);
			//decoration.TextColor = Color.Red;
			//decoration.Alignment = ContentAlignment.MiddleRight;
			//e.SubItem.Decoration = decoration;
			//e.SubItem.CellPadding = 

			if (e.ColumnIndex == 1)
			{
				var model = (TransactionView)e.Model;
				if (model.Amount != null && model.Acount != null)
					e.SubItem.ForeColor = double.Parse(model.Amount) < 0 ? Color.Red : Color.Green;
			}
			if (e.ColumnIndex == 2)
			{
				var font = e.Item.Font;
				e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Regular);
			}
			if (e.ColumnIndex == 3)
			{
				var font = e.Item.Font;
				e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Regular);
			}
		}

		private void treeListView_FormatRow(object sender, FormatRowEventArgs e)
		{
			var row = (TransactionView) e.Model;
			var font = e.Item.Font;

			if (row.Acount != null)
			{
				e.Item.Font = new Font(font.Name, font.Size, FontStyle.Bold);
			}

			if (row.Amount == null)
			{
				e.Item.Font = new Font(font.Name, font.Size, FontStyle.Regular | FontStyle.Underline);
				e.Item.ForeColor = Color.Blue;
			}
		}
	}

}
