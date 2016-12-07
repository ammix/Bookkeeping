using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Collections;

namespace DesktopBookkeepingClient
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			InitializeTreeListView();
		}

		private void InitializeTreeListView()
		{
			treeListView.CanExpandGetter = model => ((TransactionView)model).HasChildren;
			treeListView.ChildrenGetter = model => ((TransactionView)model).Nodes;
			treeListView.Roots = LocalDb.GetTransactions();

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

            if (e.ColumnIndex == 0)
            {
                var model = (TransactionView)e.Model;
                if (model.Balance == null && !model.HasChildren)
                    e.SubItem.Text = "• " + e.SubItem.Text;
            }

			if (e.ColumnIndex == 1)
			{
				var model = (TransactionView)e.Model;
                if (model.Amount != null && model.Acount != null)
                {
                    e.SubItem.ForeColor = double.Parse(model.Amount) < 0 ? Color.DeepPink : Color.Green;
                    //e.SubItem.Text += "  ";
                }
                else
                {
                    var font = e.Item.Font;
                    e.SubItem.Font = new Font(font.Name, font.Size - 1, FontStyle.Regular);
                    //e.SubItem.Text += "   ";
                    //e.SubItem.ForeColor = Color.Gray;
                    //e.Item
                    //var cellPadding = e.SubItem.CellPadding;
                    //cellPadding.Value.X += 10;
                    //e.SubItem.CellPadding = cellPadding;
                }
			}

            //if (e.ColumnIndex == 5)
            //{
            //    var font = e.Item.Font;
            //    //e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Regular);
            //    e.SubItem.ForeColor = Color.Gray;
            //}

            //if (e.ColumnIndex == 3)
            //{
            //	var font = e.Item.Font;
            //	e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Bold);
            //}

            if (e.ColumnIndex == 4)
            {
                var font = e.Item.Font;
                //e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Bold);
                e.SubItem.ForeColor = Color.Gray;
            }

            if (e.ColumnIndex == 5)
            {
                var font = e.Item.Font;
                //e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Bold);
                e.SubItem.ForeColor = Color.LightGray;
            }
        }

		private void treeListView_FormatRow(object sender, FormatRowEventArgs e)
		{
			var row = (TransactionView)e.Model;
			var font = e.Item.Font;

            //if (row.Acount != null)
            //{
            //	e.Item.Font = new Font(font.Name, font.Size, FontStyle.Bold);
            //}

            if (row.Balance == null && !row.HasChildren)
            {
                e.Item.ForeColor = Color.Gray;
            }

            if (row.Balance == null && row.HasChildren)
            {
                e.Item.BackColor = Color.LightGray; // WhiteSmoke;
            }

            if (row.Amount == null)
			{
				e.Item.Font = new Font(font.Name, font.Size, FontStyle.Regular | FontStyle.Underline);
				e.Item.ForeColor = Color.Blue;
			}
		}

        private void toolStripTextBox1_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            var transact = new TransactionView { Counterparty = "8 грудня 2016", Nodes = new List<TransactionView> { new TransactionView { Counterparty="" } } };
            treeListView.AddObject(transact);
            treeListView.EnsureModelVisible(transact);
            //treeListView.Sort??
        }
    }

}
