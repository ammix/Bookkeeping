using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public partial class MainForm : Form
	{
        bool flag = false;
        List<TransactionView> model;

		public MainForm()
		{
			InitializeComponent();

            InitializeTreeListView();
		}

		private void InitializeTreeListView()
		{
            treeListView.AddDecoration(new EditingCellBorderDecoration { UseLightbox = true });

            treeListView.CanExpandGetter = model => ((TransactionView)model).HasChildren;
			treeListView.ChildrenGetter = model => ((TransactionView)model).Nodes;
			treeListView.Roots = model = MockDb.GetTransactions();

			treeListView.TreeColumnRenderer.IsShowLines = false;
			treeListView.TreeColumnRenderer.UseTriangles = true;
			treeListView.FullRowSelect = true;
			treeListView.UseCellFormatEvents = true;
		}


		private void treeListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
		{
			var cell = (TransactionView)e.Model;
			var font = e.Item.Font;

			if (cell.NestingLevel == 0)
				e.Item.BackColor = Color.Red; // LightGray; // WhiteSmoke;

			if (e.Column.AspectName == "Counterparty")
            {
	            if (cell.NestingLevel == 2)
	            {
		            e.SubItem.Text = "• " + e.SubItem.Text;
					e.Item.ForeColor = Color.Gray;
				}

	            if (cell.NestingLevel == 0)
				{
					e.Item.Font = new Font(font.Name, font.Size, FontStyle.Regular | FontStyle.Underline);
					e.Item.ForeColor = Color.Blue;
					//e.Item.BackColor = Color.LightGray; // WhiteSmoke;
				}
			}

			if (e.Column.AspectName == "Amount")
			{
				if (cell.NestingLevel == 1)
				{
					e.SubItem.ForeColor = cell.Amount.Contains("-") ? Color.DeepPink : Color.Green;
				}
                else if (cell.NestingLevel == 2)
                {
                    e.SubItem.Font = new Font(font.Name, font.Size - 1, FontStyle.Regular);
                    e.SubItem.ForeColor = Color.Gray;
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

            //if (e.ColumnIndex == 4)
			if (e.Column.AspectName == "Acount")
            {
                //var font = e.Item.Font;
                //e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Bold);
	            e.SubItem.ForeColor = Color.Gray;
            }

            //if (e.ColumnIndex == 5)
			if (e.Column.AspectName == "Time")
            {
                //var font = e.Item.Font;
                //e.SubItem.Font = new Font(font.Name, font.Size, FontStyle.Bold);
                e.SubItem.ForeColor = Color.LightGray;
            }
        }

		private void treeListView_FormatRow(object sender, FormatRowEventArgs e)
		{
			var row = (TransactionView)e.Model;
			//var font = e.Item.Font;

            //if (row.Acount != null)
            //{
            //	e.Item.Font = new Font(font.Name, font.Size, FontStyle.Bold);
            //}

            //if (row.Balance == null && !row.HasChildren)
            //{
            //    e.Item.ForeColor = Color.Gray;
            //}

            //if (row.Balance == null && row.HasChildren)
            //{
	           // e.Item.BackColor = Color.LightGray; // WhiteSmoke;
            //}

   //         if (row.Amount == null)
			//{
			//	e.Item.Font = new Font(font.Name, font.Size, FontStyle.Regular | FontStyle.Underline);
			//	e.Item.ForeColor = Color.Blue;
			//}
		}

        private void toolStripTextBox1_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
            DateTime date = DateTime.Now;
            var s = $"{date.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month).ToLower().Replace('ь', 'я')} {date.Year}";


            var transact = new TransactionView { Counterparty = s, Nodes = new List<TransactionView> { new TransactionView { Id = 10 } } };
            treeListView.AddObject(transact);
            treeListView.EnsureModelVisible(transact);

            treeListView.Sort(olvColumn7, SortOrder.Ascending);
            treeListView.RebuildColumns();


            treeListView.ExpandAll();
            var x = treeListView.GetItem(1);

            treeListView.StartCellEdit(x, 0);

            (treeListView as FinanceTreeListView).newDayTransaction = true;

            //treeListView.CancelCellEdit();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            flag = !flag;
            if (flag)
                treeListView.ExpandAll();
            else
                treeListView.CollapseAll();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var enumerator = treeListView.Roots.GetEnumerator();
            enumerator.MoveNext();
            treeListView.RemoveObject(enumerator.Current);
        }
    }

}
