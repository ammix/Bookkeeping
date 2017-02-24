using BrightIdeasSoftware;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
	public class FinanceCellEditKeyEngine: CellEditKeyEngine
	{
		protected override void HandleEndEdit()
		{
			//base.HandleEndEdit();

			var row = (TreeListViewModel) ItemBeingEdited.RowObject;
			var list = ListView as FinanceTreeListView;

			if (row.NestingLevel == NestingLevel.Transaction)
			{
				if (string.IsNullOrEmpty(row.Amount) || string.IsNullOrEmpty(row.Account))
				{
					MessageBox.Show("Ціна і рахунок мають бути заповнені", "Помилка створення транзакції",
						MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}

				base.HandleEndEdit();

				if (row.Id == null)
				{
					int id = LocalDb.InsertTransaction(row);
					row.Id = id;
				}
				else
					LocalDb.UpdateTransaction(row);

				list.AddTransaction();

				//(ListView as FinanceTreeListView).CurrentItem = null;
			}
			else if (row.NestingLevel == NestingLevel.InvoiceLine)
			{
				if (string.IsNullOrEmpty(row.Tree) || string.IsNullOrEmpty(row.Amount) || !list.ValidateArticle(row)) //TODO !!!
				{
					MessageBox.Show("Артикул і ціна мають бути заповнені", "Помилка створення invoice line",
						MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}

				base.HandleEndEdit();

				if (row.Id == null)
				{
					LocalDb.InsertInvoiceLine(row);
				}
				else
					LocalDb.UpdateInvoiceLine(row);

				list.AddInvoiceLine();
			}

			//base.HandleEndEdit();
			//(ListView as FinanceTreeListView).CurrentItem = null;
		}
	}

	public class FinanceTreeListView: TreeListView
	{
		public TreeListViewModel _currentItem;

		public TreeListViewModel CurrentItem  //SelectedObject
		{
			get { return _currentItem; }
			set { _currentItem = value; }
		}

		ComboBox treeComboBox;
		TextBox amountTextBox;
		TextBox commentTextBox;
		ComboBox accountComboBox;

		public FinanceTreeListView()
		{
			//treeComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
			//treeComboBox.Items.AddRange(LocalDb.GetCounterparties());

			//amountTextBox = new TextBox();
			//commentTextBox = new TextBox();

			//accountComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
			//accountComboBox.Items.AddRange(LocalDb.GetAccounts());

			CellEditValidating += FinanceTreeListView_CellEditValidating;
		}

		private void FinanceTreeListView_CellEditValidating(object sender, CellEditEventArgs e)
		{
			string s1 = ((TreeListViewModel)e.RowObject).Amount;
			string s2 = ((TreeListViewModel)e.RowObject).Account;
			//e.Cancel = true;
		}

		//protected override OnValidating(CancelEventArgs e)
		//{
		//	base.OnValidating(e);
		//}

		protected override void OnCellEditorValidating(CellEditEventArgs e)
		{
			var row = (TreeListViewModel)e.RowObject;
			if (e.Column.AspectName == "Tree" && row.NestingLevel == NestingLevel.InvoiceLine)
			{
				base.OnCellEditorValidating(e); //TODO: is this line need indeed?

				e.Cancel = true;

				//foreach (var article in LocalDb.GetArticles())
				//{
				//	if (article == e.Control.Text)
				//	{
				//		e.Cancel = false;
				//		break;
				//	}
				//}

				if (ValidateArticle(row))
					e.Cancel = false;
			}

			//var row = (TreeListViewModel)e.RowObject;
			//var result = ValidateTransaction(row);
		}

		public bool ValidateArticle(TreeListViewModel row)
		{
			foreach (var article in LocalDb.GetArticles())
				if (article == row.Tree) // row.Article
					return true;

			return false;
		}

		bool ValidatePrice(TreeListViewModel row)
		{
			return true;
		}

		//bool ValidateTransaction(TreeListViewModel row)
		//{
		//	switch (row.NestingLevel)
		//	{
		//		case NestingLevel.Transaction:
		//			break;
		//		case NestingLevel.InvoiceLine:
		//			foreach (var article in LocalDb.GetArticles())
		//			{
		//				if (article == row.Tree) // row.Article
		//					break;
		//				return false;
		//			}
		//			break;
		//	}




		//	return true;
		//}


		protected override void OnCellEditFinishing(CellEditEventArgs e)
		{
			base.OnCellEditFinishing(e);

			RefreshItem(e.ListViewItem);

			e.Cancel = true;
			e.AutoDispose = false;
		}

		//protected override void OnCellEditFinished(CellEditEventArgs e)
		//{
		//	base.OnCellEditFinished(e);

		//	//if (e.Column.AspectName == "Amount" && string.IsNullOrEmpty(((TreeListViewModel)e.RowObject).Amount))
		//	//	StartCellEdit(GetItem(1), 1);	

		//	//if (string.IsNullOrEmpty(((TreeListViewModel) e.RowObject).Amount))
		//	// StartCellEdit(GetItem(1), 1);
		//	//else if (string.IsNullOrEmpty(((TreeListViewModel) e.RowObject).Account))
		//	// StartCellEdit(GetItem(1), 4);
		//	//else
		//	//{
		//	// CurrentItem = null;
		//	//}

		//	//CurrentItem = null;
		//	RebuildAll(true);
		//}

		public override void CancelCellEdit()
		{
			base.CancelCellEdit();

			if (CurrentItem != null)
			{
				CurrentItem._parent.Nodes.Remove(CurrentItem);
				//CurrentItem.Remove();

				if (CurrentItem._parent.NestingLevel == NestingLevel.FinDay && !CurrentItem._parent.HasChildren)
				{
					//var roots = EnumerableToArray(Roots, true);
					//roots.Remove(CurrentItem._parent);
					//SetObjects(roots);

					RemoveObject(CurrentItem._parent);
				}

				CurrentItem = null;
			}

			RebuildAll(true);
		}

		protected override void OnCellEditStarting(CellEditEventArgs e)
		{
			var row = (TreeListViewModel) e.RowObject;

			//if (row.NestingLevel == NestingLevel.InvoiceLine)
			//{
			//	//CellEditTabChangesRows = true;
			//	//CellEditEnterChangesRows = true;
			//}

			base.OnCellEditStarting(e);

			switch (e.Column.AspectName)
			{
				case "Tree":
					treeComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
					if ((e.RowObject as TreeListViewModel).NestingLevel == NestingLevel.Transaction)
						treeComboBox.Items.AddRange(LocalDb.GetCounterparties());
					else
					{
						//treeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
						treeComboBox.Items.AddRange(LocalDb.GetArticles());
					}

					treeComboBox.Font = Font;
					treeComboBox.Bounds = e.CellBounds;
					treeComboBox.Text = (string)e.Value;
					treeComboBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Counterparty = treeComboBox.Text; //Tree
					e.Control = treeComboBox;
					break;

				case "Amount":
					amountTextBox = new TextBox();

					amountTextBox.Font = Font;
					amountTextBox.Bounds = e.CellBounds;
					amountTextBox.Text = (string)e.Value;
					amountTextBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Amount = amountTextBox.Text;
					e.Control = amountTextBox;
					break;

				case "Comment":
					commentTextBox = new TextBox();

					commentTextBox.Font = Font;
					commentTextBox.Bounds = e.CellBounds;
					commentTextBox.Text = (string)e.Value;
					commentTextBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Comment = commentTextBox.Text;
					e.Control = commentTextBox;
					break;

				case "Account":
					accountComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
					accountComboBox.Items.AddRange(LocalDb.GetAccounts());

					accountComboBox.Font = Font;
					accountComboBox.Bounds = e.CellBounds;
					accountComboBox.Text = (string)e.Value;
					accountComboBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Account = accountComboBox.Text;
					e.Control = accountComboBox;
					break;
			}
		}

		//Known issues:
		// 1. Коли контрол, то починається з нуля, інакше починається із відступом так як в дереві
		// 2. Контрол працює лише після другого кліка

		public void AddInvoiceLine()
		{
			CurrentItem = (TreeListViewModel)SelectedObject;
			AddInvoiceLine(CurrentItem._parent);
		}

		public void AddInvoiceLine(TreeListViewModel model)
		{
			//var model = CurrentItem;

			var n = IndexOf(model) + (model.HasChildren ? model.Nodes.Count : 0);

			var newRow = new TreeListViewModel("", "");
			newRow._parent = model;
			model.Add(newRow);
			RebuildAll(true);
			ExpandAll();

			CurrentItem = newRow; // newRow._parent;
			SelectedObject = CurrentItem;

			StartCellEdit(GetItem(n + 1), 0);
		}

		public void AddTransaction()
		{
			CurrentItem = (TreeListViewModel)SelectedObject;
			AddTransaction(CurrentItem._parent);
		}

		public void AddTransaction(TreeListViewModel model)
		{
			var n = IndexOf(model);

			var newRow = new TreeListViewModel(null, "", "", "", "");

			model.Insert(0, newRow);

			//list.EnsureModelVisible(model);
			RebuildAll(true);
			ExpandAll();

			CurrentItem = newRow;
			SelectedObject = CurrentItem;

			StartCellEdit(GetItem(n + 1), 1);
		}
	}
}
