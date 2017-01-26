using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Globalization;

namespace DesktopBookkeepingClient
{
	public partial class MainForm : Form
	{
        bool flag = false;

		public MainForm()
		{
			InitializeComponent();

            InitializeTreeListView();
		}

		private void InitializeTreeListView()
		{
            treeListView.AddDecoration(new EditingCellBorderDecoration { UseLightbox = true });

            treeListView.CanExpandGetter = model => ((TreeListViewModel)model).HasChildren;
			treeListView.ChildrenGetter = model => ((TreeListViewModel)model).Nodes;

			var localDb = new LocalDb();
			treeListView.Roots = localDb.GetTransactions();

			treeListView.TreeColumnRenderer.IsShowLines = false;
			treeListView.TreeColumnRenderer.UseTriangles = true;
			treeListView.FullRowSelect = true;
			treeListView.UseCellFormatEvents = true;

            //treeListView.CellEditUseWholeCell = true;
            treeListView.CellEditKeyEngine = new FinanceCellEditKeyEngine();
            //treeListView.PossibleFinishCellEditing();
        }

		private void treeListView_FormatCell(object sender, FormatCellEventArgs e)
		{
            var cell = (TreeListViewModel)e.Model;
            var item = e.SubItem; // use SubItem in cell, Item in cell is like first cell in row

            var font = e.Item.Font;
            switch (e.Column.AspectName)
            {
                case "Tree":
                    switch(cell.NestingLevel)
                    {
                        case NestingLevel.FinDay:
                            item.Font = new Font(font.Name, font.Size, FontStyle.Underline);
                            item.ForeColor = Color.Blue;
                            break;
                        case NestingLevel.InvoiceLine:
                            item.Text = "• " + item.Text;
                            break;
                    }
                    break;

                case "Amount":
                    switch(cell.NestingLevel)
                    {
                        case NestingLevel.Transaction:
                            item.ForeColor = cell.Amount.Contains("-") ? Color.Black : Color.Green; //DeepPink
							item.Font = new Font(font.Name, font.Size + 0, FontStyle.Bold);
							break;
                        case NestingLevel.InvoiceLine:
                            item.Font = new Font(font.Name, font.Size - 0, FontStyle.Regular);
                            break;
                    }
                    break;

				//case "Balance":
				//	item.Font = new Font(font.Name, font.Size, FontStyle.Bold);
				//	break;

				case "Account":
                    item.ForeColor = Color.Gray;
                    break;

                case "Time":
                    item.ForeColor = Color.LightGray;
                    break;
            }
        }

		private void treeListView_FormatRow(object sender, FormatRowEventArgs e)
		{
            var cell = (TreeListViewModel)e.Model;
            var item = e.Item;

            switch (cell.NestingLevel)
            {
                case NestingLevel.FinDay:
                    item.BackColor = Color.LightGray; // WhiteSmoke;
                    break;
                case NestingLevel.InvoiceLine:
                    item.ForeColor = Color.Gray;
                    break;
            }
        }

        private void toolStripTextBox1_Click(object sender, System.EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, System.EventArgs e)
        {
			DateTime date;

			using (DateDialog dateDialog = new DateDialog())
			{
				DialogResult result = dateDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					date = dateDialog.Date;
				}
				else
				{
					return;
				}
			}

			if(treeListView.CurrentItem !=null)
				return;

            var s = $"{date.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month).ToLower().Replace('ь', 'я')} {date.Year}";

			if ((treeListView.GetItem(0).RowObject as TreeListViewModel).Tree == s)
				return;

            var transact = new TreeListViewModel(date: s, transactions: new List<TreeListViewModel> { new TreeListViewModel(null, "", "", "", "") });

            treeListView.InsertObjects(0, new[] { transact });
            treeListView.EnsureModelVisible(transact);

            treeListView.ExpandAll();

            treeListView.CurrentItem = transact;
            treeListView.StartCellEdit(treeListView.GetItem(1), 0);
            //treeListView.CancelCellEdit();

            //treeListView.PossibleFinishCellEditing
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
			//var model = treeListView.GetItem(0).RowObject as TreeListViewModel;
			var model = (TreeListViewModel)treeListView.SelectedObject;
			treeListView.RemoveObject(model);

	        //var enumerator = treeListView.Roots.GetEnumerator();
	        //enumerator.MoveNext();
	        //treeListView.RemoveObject(enumerator.Current);

	        //var q = treeListView.GetSubItem(0, 0);
	        //q.Text = "Hello, world!";
	        //model[0].Tree = "Hello, world!";

	        //e.ListViewItem.SubItems[0].Text = "Hello, world!";
        }

        private void treeListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

		private void toolStripButton6_Click(object sender, EventArgs e)
		{
			var localDb = new LocalDb();
			treeListView.Roots = localDb.GetTransactions();
			treeListView.ExpandAll();
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
			//var roots = treeListView.Roots;

			//var model = (TreeListViewModel)treeListView.SelectedObject;
			//var index = treeListView.SelectedIndex;
			//treeListView.MoveObjects(index--, new[] { model });
		}
	}

}
