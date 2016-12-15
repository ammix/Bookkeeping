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
        public List<TreeListViewModel> model;

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
			treeListView.Roots = model = MockDb.GetTransactions();

			treeListView.TreeColumnRenderer.IsShowLines = false;
			treeListView.TreeColumnRenderer.UseTriangles = true;
			treeListView.FullRowSelect = true;
			treeListView.UseCellFormatEvents = true;
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
                            item.ForeColor = cell.Amount.Contains("-") ? Color.DeepPink : Color.Green;
                            break;
                        case NestingLevel.InvoiceLine:
                            item.Font = new Font(font.Name, font.Size - 1, FontStyle.Regular);
                            break;
                    }
                    break;

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
            DateTime date = DateTime.Now;
            var s = $"{date.Day} {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month).ToLower().Replace('ь', 'я')} {date.Year}";


            var transact = new TreeListViewModel(date: s, transactions: new List<TreeListViewModel> { new TreeListViewModel() });
            treeListView.AddObject(transact);
            treeListView.EnsureModelVisible(transact);

            treeListView.Sort(olvColumn7, SortOrder.Descending); //Ascending);
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

        private void treeListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
