using System.Collections.Generic;
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
			treeListView.CanExpandGetter = model => ((Transaction)model).IsInvoiceLine;
			treeListView.ChildrenGetter = model => ((Transaction) model).InvoiceLines;
			treeListView.Roots = Transaction.GetTransactions();

            treeListView.TreeColumnRenderer.IsShowLines = false;
            treeListView.TreeColumnRenderer.UseTriangles = true;
            treeListView.FullRowSelect = true;
            treeListView.UseCellFormatEvents = true;
		}


		private void treeListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
		{
			var decoration = new TextDecoration("ABCDEF", 255);
			decoration.Font = new Font(Font.Name, Font.SizeInPoints +2);
			decoration.TextColor = Color.Red;
            decoration.Alignment = ContentAlignment.MiddleRight;
			e.SubItem.Decoration = decoration;
		}
	}

}
