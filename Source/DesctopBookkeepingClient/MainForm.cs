﻿using System;
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

			treeListView.CanExpandGetter = model => ((TreeListViewModel)model).HasChildren;
			treeListView.ChildrenGetter = model => ((TreeListViewModel)model).Nodes;

			var localDb = new LocalDb();
			treeListView.Roots = localDb.GetTransactions();

			treeListView.TreeColumnRenderer.IsShowLines = false;
			treeListView.TreeColumnRenderer.UseTriangles = true;
			treeListView.FullRowSelect = true;
			treeListView.UseCellFormatEvents = true;

			//treeListView.GetColumn(2).AspectPutter = delegate (object model, object newValue)
			//{
			//	TreeListViewModel m = (TreeListViewModel)model;
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

			if (treeListView.CurrentItem != null)
				return;

			var s = date.ToShortDateString();

			if ((treeListView.GetItem(0).RowObject as TreeListViewModel).Tree == s)
				return;

			var newFinDay = new TreeListViewModel(date: date, transactions: new List<TreeListViewModel>());
			//var newFinDay = new TreeListViewModel(date);

			ArrayList roots = ObjectListView.EnumerableToArray(treeListView.Roots, true);
			roots.Insert(0, newFinDay);
			treeListView.SetObjects(roots);


			treeListView.AddTransaction(newFinDay);
			//treeListView.CurrentItem = newFinDay;


			//treeListView.CancelCellEdit();
			//treeListView.PossibleFinishCellEditing

			//else
			//{
			//	var transact = (TreeListViewModel)treeListView.GetItem(0).RowObject;
			//	transact.A

			//	//var transact = new TreeListViewModel(date: s, transactions: new List<TreeListViewModel> { new TreeListViewModel(null, "", "", "", "") });

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

			//var model = (TreeListViewModel)treeListView.SelectedObject;
			//var index = treeListView.SelectedIndex;
			//treeListView.MoveObjects(index--, new[] { model });
		}

		TreeListViewModel clickedRow;

		private void treeListView_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			var row = (TreeListViewModel)e.Model;

			if (row.NestingLevel == NestingLevel.FinDay)
			{
				e.MenuStrip = contextMenuStrip1; //this.DecideRightClickMenu(e.Model, e.Column);
				clickedRow = row;
				//treeListView.CurrentItem = clickedRow;

				//var transact = (TreeListViewModel)treeListView.GetItem(0).RowObject;
				//transact.A

				//	//var transact = new TreeListViewModel(date: s, transactions: new List<TreeListViewModel> { new TreeListViewModel(null, "", "", "", "") });

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
			treeListView.AddTransaction(clickedRow);
		}

		private void виToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//clickedRow._parent.Nodes.Remove(clickedRow);
			//treeListView.RebuildAll(true);

			LocalDb.RemoveTransaction(clickedRow); //TODO: return result and then update UI

			if (clickedRow != null)
			{
				clickedRow._parent.Nodes.Remove(clickedRow);

				if (clickedRow._parent.NestingLevel == NestingLevel.FinDay && !clickedRow._parent.HasChildren)
					treeListView.RemoveObject(clickedRow._parent);

				//treeListView.CurrentItem = null;
			}

			treeListView.RebuildAll(true);
			//treeListView.BuildList();
		}

		private void додатиЛініюІнвойсаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			treeListView.AddInvoiceLine(clickedRow);
		}

		//private static void AddInvoiceLine(FinanceTreeListView list, TreeListViewModel model)
		//{
		//	var n = list.IndexOf(model) + (model.HasChildren ? model.Nodes.Count : 0);

		//	var newRow = new TreeListViewModel("", "");
		//	newRow._parent = model;
		//	model.Add(newRow);
		//	list.RebuildAll(true);
		//	list.ExpandAll();
		//	list.CurrentItem = newRow;

		//	list.StartCellEdit(list.GetItem(n + 1), 0);
		//}

		private void treeListView_CellEditStarting(object sender, CellEditEventArgs e)
		{
			var row = (TreeListViewModel)e.RowObject;

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
			clickedRow._parent.Nodes.Remove(clickedRow);
			treeListView.RebuildAll(true);

			LocalDb.RemoveInvoiceLine(clickedRow);

			//treeListView.BuildList();
		}

		private void treeListView_KeyPress(object sender, KeyPressEventArgs e)
		{
			var row = (TreeListViewModel) treeListView.SelectedObject;

			var index = row._parent.Nodes.IndexOf(row);

			//var index = clickedRow._parent.Nodes.IndexOf(clickedRow);
			//var upper = clickedRow._parent.Nodes[index - 1];
			//var downer = clickedRow._parent.Nodes[index + 1];
			switch (e.KeyChar)
			{
				case '-':
					row._parent.Nodes.RemoveAt(index);
					row._parent.Nodes.Insert(index + 1, row);
					treeListView.RebuildAll(true);

					var next = row._parent.Nodes[index];
					LocalDb.MoveTransaction(row, next);
					break;
				case '+':
					row._parent.Nodes.RemoveAt(index);
					row._parent.Nodes.Insert(index - 1, row);
					treeListView.RebuildAll(true);

					var prev = row._parent.Nodes[index]; // - 1];
					LocalDb.MoveTransaction(row, prev);
					break;
			}
		}
	}

}
