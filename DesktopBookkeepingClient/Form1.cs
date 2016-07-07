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
		}

		private void treeListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
		{
			var decoration = new TextDecoration("Готівка", 125);
			decoration.Font = new Font(Font.Name, Font.SizeInPoints +2);
			decoration.TextColor = Color.Red;
			e.SubItem.Decoration = decoration;
		}
	}

}
