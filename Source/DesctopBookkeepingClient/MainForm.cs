using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace DesktopBookkeepingClient
{
	public partial class MainForm : Form
	{
		bool flag = false;

		public MainForm()
		{
			InitializeComponent();

			InitializeTreeListView();

			var connectionString = ConfigurationManager.ConnectionStrings["BookkeepingDb"].ConnectionString;
			var values = connectionString.Split(';');
			foreach (var value in values)
				if (value.Contains("data source"))
				{
					label1.Text = value.Replace("data source=", "");
					break;
				}
		}

		private void InitializeTreeListView()
		{
			treeListView.AddDecoration(new EditingCellBorderDecoration { UseLightbox = true });

			treeListView.CanExpandGetter = model => ((ITreeListViewModel)model).CanExpand;
			treeListView.ChildrenGetter = model => ((ITreeListViewModel)model).Children;

			var localDb = new LocalDb();
			treeListView.Roots = localDb.GetTransactions();

			treeListView.TreeColumnRenderer.IsShowLines = false;
			treeListView.TreeColumnRenderer.UseTriangles = true;
			treeListView.FullRowSelect = true;
			treeListView.UseCellFormatEvents = true;

			//treeListView.GetColumn(2).AspectPutter = delegate (object model, object newValue)
			//{
			//	ITreeListViewModel m = (ITreeListViewModel)model;
			//	m.Comment = (string)newValue;
			//};

							//this.columnHeaderSalaryRate.AspectPutter = delegate (object model, object newValue) {
							//	Person p = (Person)model;
							//	p.SetRate((double)newValue);
							//};


			//treeListView.CellEditUseWholeCell = true;
			treeListView.CellEditKeyEngine = new FinanceCellEditKeyEngine();
			//treeListView.PossibleFinishCellEditing();

			treeListView.ExpandAll();
		}

		private void treeListView_FormatCell(object sender, FormatCellEventArgs e)
		{
			var cell = (ITreeListViewModel)e.Model;
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
			var cell = (ITreeListViewModel)e.Model;
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

			if (treeListView.CurrentItem != null)
				return;

			var s = date.ToShortDateString();

			if ((treeListView.GetItem(0).RowObject as ITreeListViewModel).Tree == s)
				return;

			var newFinDay = new TreeListViewModelConcrete(date: date, transactions: new List<ITreeListViewModel>());
			//var newFinDay = new ITreeListViewModel(date);

			ArrayList roots = ObjectListView.EnumerableToArray(treeListView.Roots, true);
			roots.Insert(0, newFinDay);
			treeListView.SetObjects(roots);


			treeListView.AddTransaction(newFinDay);
			//treeListView.CurrentItem = newFinDay;


			//treeListView.CancelCellEdit();
			//treeListView.PossibleFinishCellEditing

			//else
			//{
			//	var transact = (ITreeListViewModel)treeListView.GetItem(0).RowObject;
			//	transact.A

			//	//var transact = new ITreeListViewModel(date: s, transactions: new List<ITreeListViewModel> { new ITreeListViewModel(null, "", "", "", "") });

			//	treeListView.InsertObjects(0, new[] { transact });
			//	treeListView.EnsureModelVisible(transact);

			//	treeListView.ExpandAll();

			//	treeListView.CurrentItem = transact;
			//	treeListView.StartCellEdit(treeListView.GetItem(1), 0);
			//}
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
			//var model = treeListView.GetItem(0).RowObject as ITreeListViewModel;
			var model = (ITreeListViewModel)treeListView.SelectedObject;
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
			TotalUpdateListVew();
		}

		void TotalUpdateListVew()
		{
			var localDb = new LocalDb();
			treeListView.Roots = localDb.GetTransactions();
			treeListView.ExpandAll();

			// TODO: hack: update needed because transaction id is set on SQL side and transaction on client side do not know that Id
			//var localDb = new LocalDb();
			//ListView.SetObjects(localDb.GetTransactions());
			//	(ListView as FinanceTreeListView).ExpandAll();
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
			//var roots = treeListView.Roots;

			//var model = (ITreeListViewModel)treeListView.SelectedObject;
			//var index = treeListView.SelectedIndex;
			//treeListView.MoveObjects(index--, new[] { model });
		}

		ITreeListViewModel clickedRow;

		private void treeListView_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			var row = (ITreeListViewModel)e.Model;

			if (row.NestingLevel == NestingLevel.FinDay)
			{
				e.MenuStrip = contextMenuStrip1; //this.DecideRightClickMenu(e.Model, e.Column);
				clickedRow = row;
				//treeListView.CurrentItem = clickedRow;

				//var transact = (ITreeListViewModel)treeListView.GetItem(0).RowObject;
				//transact.A

				//	//var transact = new ITreeListViewModel(date: s, transactions: new List<ITreeListViewModel> { new ITreeListViewModel(null, "", "", "", "") });

				//treeListView.InsertObjects(0, new[] { transact });
				//treeListView.EnsureModelVisible(transact);

				//treeListView.ExpandAll();

				//treeListView.CurrentItem = transact;
				//treeListView.StartCellEdit(treeListView.GetItem(1), 0);
			}

			if (row.NestingLevel == NestingLevel.Transaction)
			{
				e.MenuStrip = contextMenuStrip2;
				clickedRow = row;
				//treeListView.CurrentItem = clickedRow;
			}

			if (row.NestingLevel == NestingLevel.InvoiceLine)
			{
				e.MenuStrip = contextMenuStrip3;
				clickedRow = row;
				//treeListView.CurrentItem = clickedRow;
			}

			//treeListView.CurrentItem = clickedRow;
		}

		private void додатиТрансакціюToolStripMenuItem_Click(object sender, EventArgs e)
		{
			treeListView.AddTransaction(clickedRow as TreeListViewModelConcrete);
		}

		private void виToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//clickedRow.Parent.Children.Remove(clickedRow);
			//treeListView.RebuildAll(true);

			LocalDb.RemoveTransaction(clickedRow); //TODO: return result and then update UI

			if (clickedRow != null)
			{
				clickedRow.Parent.Children.Remove(clickedRow);

				if (clickedRow.Parent.NestingLevel == NestingLevel.FinDay && !clickedRow.Parent.CanExpand)
					treeListView.RemoveObject(clickedRow.Parent);

				//treeListView.CurrentItem = null;
			}

			treeListView.RebuildAll(true);
			//treeListView.BuildList();
		}

		private void додатиЛініюІнвойсаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			treeListView.AddInvoiceLine(clickedRow as TreeListViewModelConcrete);
		}

		//private static void AddInvoiceLine(FinanceTreeListView list, ITreeListViewModel model)
		//{
		//	var n = list.IndexOf(model) + (model.CanExpand ? model.Children.Count : 0);

		//	var newRow = new ITreeListViewModel("", "");
		//	newRow.Parent = model;
		//	model.Add(newRow);
		//	list.RebuildAll(true);
		//	list.ExpandAll();
		//	list.CurrentItem = newRow;

		//	list.StartCellEdit(list.GetItem(n + 1), 0);
		//}

		private void treeListView_CellEditStarting(object sender, CellEditEventArgs e)
		{
			var row = (ITreeListViewModel)e.RowObject;

			switch (row.NestingLevel)
			{
				case NestingLevel.Transaction:
					olvColumn5.IsEditable = true;
					break;
				case NestingLevel.InvoiceLine:
					olvColumn5.IsEditable = false;
					break;
			}
		}

		private void видалитиЛініюToolStripMenuItem_Click(object sender, EventArgs e)
		{
			clickedRow.Parent.Children.Remove(clickedRow);
			treeListView.RebuildAll(true);

			LocalDb.RemoveInvoiceLine(clickedRow);

			//treeListView.BuildList();
		}

		private void treeListView_KeyPress(object sender, KeyPressEventArgs e)
		{
			var row = (ITreeListViewModel) treeListView.SelectedObject;

			var index = row.Parent.Children.IndexOf(row);

			//var index = clickedRow.Parent.Children.IndexOf(clickedRow);
			//var upper = clickedRow.Parent.Children[index - 1];
			//var downer = clickedRow.Parent.Children[index + 1];
			switch (e.KeyChar)
			{
				case '-':
					row.Parent.Children.RemoveAt(index);
					row.Parent.Children.Insert(index + 1, row);
					treeListView.RebuildAll(true);

					var next = row.Parent.Children[index];
					LocalDb.MoveTransaction(row, next);
					break;
				case '+':
					row.Parent.Children.RemoveAt(index);
					row.Parent.Children.Insert(index - 1, row);
					treeListView.RebuildAll(true);

					var prev = row.Parent.Children[index]; // - 1];
					LocalDb.MoveTransaction(row, prev);
					break;
			}
		}
	}

}
