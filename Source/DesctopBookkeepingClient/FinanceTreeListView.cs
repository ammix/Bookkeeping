using System;
using System.Collections;
using BrightIdeasSoftware;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
	public class FinanceCellEditKeyEngine: CellEditKeyEngine
	{
		//protected override void InitializeCellEditKeyMaps()
		//{
		//	base.InitializeCellEditKeyMaps();
		//	CellEditKeyMap[Keys.F1] = CellEditCharacterBehaviour.ChangeRowUp;
		//}

		//protected override bool HandleCustomVerb(Keys keyData, CellEditCharacterBehaviour behaviour)
		//{
		//	MessageBox.Show("123", "456",
		//				MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		//	return false;
		//}

		protected override void HandleEndEdit()
		{
			//base.HandleEndEdit();

			var row = ItemBeingEdited.RowObject as TransactionModel;
			var list = ListView as FinanceTreeListView;

			if (row != null)
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

			var rowLine = ItemBeingEdited.RowObject as InvoiceLineModel;
			//else if (row.NestingLevel == NestingLevel.InvoiceLine)
			if (rowLine != null)
			{
				if (string.IsNullOrEmpty(rowLine.Article) || string.IsNullOrEmpty(rowLine.Amount) || !list.ValidateArticle(rowLine)) //TODO !!!
				{
					MessageBox.Show("Артикул і ціна мають бути заповнені", "Помилка створення invoice line",
						MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}

				base.HandleEndEdit();

				if (rowLine.Id == null)
				{
					LocalDb.InsertInvoiceLine(rowLine);
				}
				else
					LocalDb.UpdateInvoiceLine(rowLine);

				list.AddInvoiceLine();
			}

			//base.HandleEndEdit();
			//(ListView as FinanceTreeListView).CurrentItem = null;
		}
	}

	public class FinanceTreeListView: TreeListView
	{
		ITreeListViewModel _currentItem;

		public ITreeListViewModel CurrentItem  //SelectedObject
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
			string s1 = ((ITreeListViewModel)e.RowObject).Amount;
			string s2 = ((ITreeListViewModel)e.RowObject).Account;
			//e.Cancel = true;
		}

		//protected override OnValidating(CancelEventArgs e)
		//{
		//	base.OnValidating(e);
		//}

		protected override void OnCellEditorValidating(CellEditEventArgs e)
		{
			var row = (ITreeListViewModel)e.RowObject;
			if (e.Column.AspectName == "Column1" && row is InvoiceLineModel)
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

				if (ValidateArticle((InvoiceLineModel)row))
					e.Cancel = false;
			}

			//var row = (ITreeListViewModel)e.RowObject;
			//var result = ValidateTransaction(row);
		}

		public bool ValidateArticle(InvoiceLineModel row)
		{
			foreach (var article in LocalDb.GetArticles())
				if (article == row.Article) // row.Article
					return true;

			return false;
		}

		bool ValidatePrice(ITreeListViewModel row)
		{
			return true;
		}

		//bool ValidateTransaction(ITreeListViewModel row)
		//{
		//	switch (row.NestingLevel)
		//	{
		//		case NestingLevel.Transaction:
		//			break;
		//		case NestingLevel.InvoiceLine:
		//			foreach (var article in LocalDb.GetArticles())
		//			{
		//				if (article == row.Column1) // row.Article
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

		//	//if (e.Column.AspectName == "Amount" && string.IsNullOrEmpty(((ITreeListViewModel)e.RowObject).Amount))
		//	//	StartCellEdit(GetItem(1), 1);	

		//	//if (string.IsNullOrEmpty(((ITreeListViewModel) e.RowObject).Amount))
		//	// StartCellEdit(GetItem(1), 1);
		//	//else if (string.IsNullOrEmpty(((ITreeListViewModel) e.RowObject).Account))
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
				CurrentItem.Parent.Children.Remove(CurrentItem);
				//CurrentItem.Remove();

				if (CurrentItem.Parent is FinDayModel && !CurrentItem.Parent.CanExpand)
				{
					//var roots = EnumerableToArray(Roots, true);
					//roots.Remove(CurrentItem.Parent);
					//SetObjects(roots);

					RemoveObject(CurrentItem.Parent);
				}

				CurrentItem = null;
			}

			RebuildAll(true);
		}

		protected override void OnCellEditStarting(CellEditEventArgs e)
		{
			var row = (ITreeListViewModel) e.RowObject;

			//if (row.NestingLevel == NestingLevel.InvoiceLine)
			//{
			//	//CellEditTabChangesRows = true;
			//	//CellEditEnterChangesRows = true;
			//}

			base.OnCellEditStarting(e);

			switch (e.Column.AspectName)
			{
				case "Column1":
					treeComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
					if (e.RowObject is TransactionModel)
						treeComboBox.Items.AddRange(LocalDb.GetCounterparties());
					else
					{
						//treeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
						treeComboBox.Items.AddRange(LocalDb.GetArticles());
					}

					treeComboBox.Font = Font;
					treeComboBox.Bounds = e.CellBounds;
					treeComboBox.Text = (string)e.Value;
					treeComboBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Column1 = treeComboBox.Text;
					e.Control = treeComboBox;
					break;

				case "Amount":
					amountTextBox = new TextBox();

					amountTextBox.Font = Font;
					amountTextBox.Bounds = e.CellBounds;
					amountTextBox.Text = (string)e.Value;
					amountTextBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Amount = amountTextBox.Text.Replace(',', '.');
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
			CurrentItem = (ITreeListViewModel)SelectedObject;
			AddInvoiceLine(CurrentItem.Parent as TransactionModel);
		}

		public void AddInvoiceLine(TransactionModel model)
		{
			//var model = CurrentItem;

			var n = IndexOf(model) + (model.CanExpand ? model.Children.Count : 0);

			var newRow = new InvoiceLineModel();
			model.AddChild(newRow);
			RebuildAll(true);
			ExpandAll();

			CurrentItem = newRow; // newRow.Parent;
			SelectedObject = CurrentItem;

			StartCellEdit(GetItem(n + 1), 0);
		}

		public void AddTransaction()
		{
			CurrentItem = (ITreeListViewModel)SelectedObject;
			AddTransaction(CurrentItem.Parent as FinDayModel);
		}

	    public void AddTransaction(DateTime date)
	    {
	        if (CurrentItem != null)
	            return;

	        var roots = ObjectListView.EnumerableToArray(Roots, true);

	        FinDayModel finDay = null;
	        foreach (var element in roots)
	        {
	            if (((FinDayModel) element).Date.Date == date.Date)
	            {
	                finDay = element as FinDayModel;
	                break;
	            }
	        }

	        if (finDay == null)
	        {
	            finDay = new FinDayModel(date);
                roots.Insert(0, finDay);
	            SetObjects(roots);
	        }

	        AddTransaction(finDay);
	    }

		public void AddTransaction(FinDayModel model)
		{
			var n = IndexOf(model);

			var newRow = new TransactionModel();
			//newRow.SetDate(model.GetDate());

			model.InsertChild(0, newRow);

			//list.EnsureModelVisible(model);
			RebuildAll(true);
			ExpandAll();

			CurrentItem = newRow;
			SelectedObject = CurrentItem;

			StartCellEdit(GetItem(n + 1), 1);
		}

		public void RemoveTransaction(TransactionModel model)
		{
			LocalDb.RemoveTransaction(model); //TODO: return result and then update UI

			// update UI
			if (model != null)
			{
				model.RemoveChild();

				if (!model.Parent.CanExpand)
					RemoveObject(model.Parent);
			}

			RebuildAll(true);
			// end update UI

			/*
			//clickedRow.Parent.Children.Remove(clickedRow);
			//treeListView.RebuildAll(true);

			LocalDb.RemoveTransaction((TransactionModel)clickedRow); //TODO: return result and then update UI

			if (clickedRow != null)
			{
				clickedRow.Parent.Children.Remove(clickedRow);

				if (clickedRow.Parent is FinDayModel && !clickedRow.Parent.CanExpand)
					treeListView.RemoveObject(clickedRow.Parent);

				//treeListView.CurrentItem = null;
			}

			treeListView.RebuildAll(true);
			//treeListView.BuildList();
			*/
		}

		public void RemoveInvoiceLine(InvoiceLineModel model)
		{
			LocalDb.RemoveInvoiceLine(model); //TODO: return result and then update UI

			//model.Parent.Children.Remove(model);
			model.RemoveChild();
			RebuildAll(true);

			//treeListView.BuildList();
		}
	}
}
