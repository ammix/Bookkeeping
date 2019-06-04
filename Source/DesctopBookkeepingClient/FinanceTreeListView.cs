using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BrightIdeasSoftware;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
	/*

public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
{
    Rectangle bounds = this.CalculateBounds();
    if (!bounds.IsEmpty)
        this.DrawFilledBorder(g, bounds);
}

        protected void DrawFilledBorder(Graphics g, Rectangle bounds)
        {
            bounds.Inflate(this.BoundsPadding);
            GraphicsPath path = this.GetRoundedRect(bounds, this.CornerRounding);
            if (this.FillGradientFrom != null && this.FillGradientTo != null) {
                if (this.FillBrush != null)
                    this.FillBrush.Dispose();
                this.FillBrush = new LinearGradientBrush(bounds, this.FillGradientFrom.Value, this.FillGradientTo.Value, this.FillGradientMode);
            }
            if (this.FillBrush != null)
                g.FillPath(this.FillBrush, path);
            if (this.BorderPen != null)
                g.DrawPath(this.BorderPen, path);
        }
        
        ------------------------------------------------
        
        
        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (!olv.IsCellEditing) 
                return;

            Rectangle bounds = olv.CellEditor.Bounds;
            if (bounds.IsEmpty)
                return;

            bounds.Inflate(this.BoundsPadding);
            GraphicsPath path = this.GetRoundedRect(bounds, this.CornerRounding);
            if (this.FillBrush != null) {
                if (this.UseLightbox) {
                    using (Region newClip = new Region(r)) {
                        newClip.Exclude(path);
                        Region originalClip = g.Clip;
                        g.Clip = newClip;
                        g.FillRectangle(this.FillBrush, r);
                        g.Clip = originalClip;
                    }
                } else {
                    g.FillPath(this.FillBrush, path);
                }
            }
            if (this.BorderPen != null)
                g.DrawPath(this.BorderPen, path);
        }
	*/
	public class FinanceRowBorderDecoration : RowBorderDecoration
	{
		public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			//base.Draw(olv, g, r);
			Rectangle bounds = this.CalculateBounds();
			if (!bounds.IsEmpty)
				this.DrawFilledBorderNew(g, bounds, r);
		}

		private void DrawFilledBorderNew(Graphics g, Rectangle bounds, Rectangle r)
		{
			bounds.Inflate(this.BoundsPadding);
			GraphicsPath path = this.GetRoundedRect(bounds, this.CornerRounding);
			if (this.FillGradientFrom != null && this.FillGradientTo != null)
			{
				if (this.FillBrush != null)
					this.FillBrush.Dispose();
				this.FillBrush = new LinearGradientBrush(bounds, this.FillGradientFrom.Value, this.FillGradientTo.Value, this.FillGradientMode);
			}
			if (this.FillBrush != null)
				//g.FillPath(this.FillBrush, path);
			{
				using (Region newClip = new Region(r))
				{
					newClip.Exclude(path);
					Region originalClip = g.Clip;
					g.Clip = newClip;
					g.FillRectangle(this.FillBrush, r);
					g.Clip = originalClip;
				}
			}
			if (this.BorderPen != null)
				g.DrawPath(this.BorderPen, path);
		}
	}

	public class FinanceTreeListView : TreeListView
	{
		protected override bool ProcessDialogKey(Keys keyData)
		{
			// Treat F2 as a request to edit the primary column
			if (keyData == (Keys.F2 | Keys.Shift))
			{
				//this.EditSubItem((OLVListItem)this.FocusedItem, 0);
				MessageBox.Show(this, "SHIFT + F2");
				return base.ProcessDialogKey(keyData);
			}

			//this.EditSubItem();

			return base.ProcessDialogKey(keyData);
		}

		//protected override bool ProcessDialogKey(Keys keyData)
		//{

		//	if (this.IsCellEditing)
		//		return this.CellEditKeyEngine.HandleKey(this, keyData);

		//	// Treat F2 as a request to edit the primary column
		//	if (keyData == Keys.F2)
		//	{
		//		this.EditSubItem((OLVListItem)this.FocusedItem, 0);
		//		return base.ProcessDialogKey(keyData);
		//	}

		//	// Treat Ctrl-C as Copy To Clipboard. 
		//	if (this.CopySelectionOnControlC && keyData == (Keys.C | Keys.Control))
		//	{
		//		this.CopySelectionToClipboard();
		//		return true;
		//	}

		//	// Treat Ctrl-A as Select All.
		//	if (this.SelectAllOnControlA && keyData == (Keys.A | Keys.Control))
		//	{
		//		this.SelectAll();
		//		return true;
		//	}

		//	return base.ProcessDialogKey(keyData);
		//}

		private ITreeListViewModel _currentItem;
		private ITreeListViewModel treeListViewModel;

		public ITreeListViewModel CurrentItem //SelectedObject
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
			string s1 = ((ITreeListViewModel) e.RowObject).Column2;
			string s2 = ((ITreeListViewModel) e.RowObject).Column5;
			//e.Cancel = true;
		}

		//protected override OnValidating(CancelEventArgs e)
		//{
		//	base.OnValidating(e);
		//}

		protected override void OnCellEditorValidating(CellEditEventArgs e)
		{
			var row = (ITreeListViewModel) e.RowObject;
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

				if (ValidateArticle((InvoiceLineModel) row))
					e.Cancel = false;
			}
			else if (e.Column.AspectName == "Column2" && (row is TransactionModel || row is InvoiceLineModel))
			{
				base.OnCellEditorValidating(e);
				e.Cancel = true;

				decimal amount;
				var ok = decimal.TryParse(e.Control.Text, out amount);
				if (ok)
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

		//	//if (e.Column.AspectName == "Column2" && string.IsNullOrEmpty(((ITreeListViewModel)e.RowObject).Column2))
		//	//	StartCellEdit(GetItem(1), 1);	

		//	//if (string.IsNullOrEmpty(((ITreeListViewModel) e.RowObject).Column2))
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
			//amountTextBox.Text = (SelectedObject as TreeListViewModel).Column2; //TODO: remove subscription
			//amountTextBox.Leave -= (o, args) =>
			//{
			//	((TreeListViewModel)e.RowObject).Value = decimal.Parse(amountTextBox.Text);
			//};

			if (amountTextBox != null) // F2 -> Esc => Exception
				amountTextBox.Leave -= SetColumn2;

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

			base.CancelCellEdit();
		}

		private void SetColumn2(object o, EventArgs args)
		{
			treeListViewModel.Column2 = amountTextBox.Text;
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			if (SelectedItem != null)
			{
				// TODO: refactor this
				if (SelectedItem.RowObject is FinDayModel)
				{
					AllColumns[0].IsEditable = false;
					AllColumns[1].IsEditable = false;
					AllColumns[2].IsEditable = false;
					AllColumns[3].IsEditable = false;
					AllColumns[4].IsEditable = false;
					AllColumns[5].IsEditable = false;
				}
				else if (SelectedItem.RowObject is TransactionModel)
				{
					AllColumns[0].IsEditable = true;
					AllColumns[1].IsEditable = true;
					AllColumns[2].IsEditable = true;
					AllColumns[3].IsEditable = false;
					AllColumns[4].IsEditable = true;
					AllColumns[5].IsEditable = false;
				}
				else if (SelectedItem.RowObject is InvoiceLineModel)
				{
					AllColumns[0].IsEditable = true;
					AllColumns[1].IsEditable = true;
					AllColumns[2].IsEditable = true;
					AllColumns[3].IsEditable = false;
					AllColumns[4].IsEditable = false;
					AllColumns[5].IsEditable = false;
				}
			}

			base.OnSelectionChanged(e);
		}

		protected override void OnCellEditStarting(CellEditEventArgs e)
		{
			treeListViewModel = (ITreeListViewModel)e.RowObject;

			//if (row.NestingLevel == NestingLevel.InvoiceLine)
			//{
			//	//CellEditTabChangesRows = true;
			//	//CellEditEnterChangesRows = true;
			//}

			switch (e.Column.AspectName)
			{
				case "Column1":
					treeComboBox = new ComboBox {DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/};
					if (e.RowObject is TransactionModel)
						treeComboBox.Items.AddRange(LocalDb.GetCounterparties());
					else
					{
						//treeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
						treeComboBox.Items.AddRange(LocalDb.GetArticles());
					}

					treeComboBox.Font = Font;
					treeComboBox.Bounds = e.CellBounds;
					treeComboBox.Text = (string) e.Value;
					treeComboBox.TextChanged += (o, args) => treeListViewModel.Column1 = treeComboBox.Text;
					e.Control = treeComboBox;
					break;

				case "Column2":
					amountTextBox = new TextBox();

					amountTextBox.Font = Font;
					amountTextBox.Bounds = e.CellBounds;
					if (treeListViewModel is TransactionModel)
						amountTextBox.Text = ((TransactionModel) treeListViewModel).Amount.ToString("F2");
					else if (treeListViewModel is InvoiceLineModel)
						amountTextBox.Text = ((InvoiceLineModel) treeListViewModel).Price.ToString("F2");
					amountTextBox.Leave += SetColumn2;
					e.Control = amountTextBox;
					break;

				case "Column3":
					commentTextBox = new TextBox();

					commentTextBox.Font = Font;
					commentTextBox.Bounds = e.CellBounds;
					commentTextBox.Text = (string) e.Value;
					commentTextBox.TextChanged += (o, args) => treeListViewModel.Column3 = commentTextBox.Text;
					e.Control = commentTextBox;
					break;

				case "Column5":
					accountComboBox = new ComboBox {DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/};
					accountComboBox.Items.AddRange(LocalDb.GetAccounts());

					accountComboBox.Font = Font;
					accountComboBox.Bounds = e.CellBounds;
					accountComboBox.Text = (string) e.Value;
					accountComboBox.TextChanged += (o, args) => treeListViewModel.Column5 = accountComboBox.Text;
					e.Control = accountComboBox;
					break;
			}

			base.OnCellEditStarting(e);
		}

		//Known issues:
		// 1. Коли контрол, то починається з нуля, інакше починається із відступом так як в дереві
		// 2. Контрол працює лише після другого кліка

		public void AddInvoiceLine()
		{
			CurrentItem = (ITreeListViewModel) SelectedObject;
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
			CurrentItem = (ITreeListViewModel) SelectedObject;
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

		public void MoveSelectedTransactionUp()
		{
			//var index = clickedRow.Parent.Children.IndexOf(clickedRow);
			//var upper = clickedRow.Parent.Children[index - 1];
			//var downer = clickedRow.Parent.Children[index + 1];

			var row = SelectedObject as TransactionModel;
			if (row == null) return;

			var index = row.Parent.Children.IndexOf(row);

			if (index != 0)
			{
				row.Parent.Children.RemoveAt(index);
				row.Parent.Children.Insert(index - 1, row);

				var prev = (TransactionModel)row.Parent.Children[index]; // - 1];
				LocalDb.MoveTransaction(row, prev);
			}
			else
			{
				row.Parent.Children.RemoveAt(index);
				var roots = EnumerableToArray(Roots, true);
				var k = roots.IndexOf(row.Parent);
				var finDay = (FinDayModel)roots[k - 1];
				finDay.AddChild(row);

				LocalDb.MoveTransactionIntoDate(row, finDay, "TOP");
			}
			RebuildAll(true);
		}

		public void MoveSelectedTransactionDown()
		{
			var row = SelectedObject as TransactionModel;
			if (row == null) return;

			var index = row.Parent.Children.IndexOf(row);

			if (index < row.Parent.Children.Count - 1)
			{
				row.Parent.Children.RemoveAt(index);
				row.Parent.Children.Insert(index + 1, row);
				var next = (TransactionModel)row.Parent.Children[index];
				LocalDb.MoveTransaction(row, next);
			}
			else
			{
				row.Parent.Children.RemoveAt(index);
				var roots = EnumerableToArray(Roots, true);
				var k = roots.IndexOf(row.Parent);
				var finDay = (FinDayModel) roots[k + 1];
				finDay.InsertChild(0, row);

				LocalDb.MoveTransactionIntoDate(row, finDay, "BOTTOM");
			}

			RebuildAll(true);
		}

		protected override void OnFormatCell(FormatCellEventArgs args)
		{
			var cell = (ITreeListViewModel)args.Model;
			var item = args.SubItem; // use SubItem in cell, Item in cell is like first cell in row

			var font = args.Item.Font;
			switch (args.Column.AspectName)
			{
				case "Column1":
					if (cell is FinDayModel)
					{
						item.Font = new Font(font.Name, font.Size, FontStyle.Underline);
						item.ForeColor = Color.Blue;
					}
					else if (cell is InvoiceLineModel)
					{
						item.Text = "• " + item.Text;
					}
					break;

				case "Column2":
					if (cell is InvoiceLineModel)
					{
						item.Font = new Font(font.Name, font.Size - 0, FontStyle.Regular);
					}
					else if (cell is TransactionModel)
					{
						item.ForeColor = cell.Column5 != null && cell.Column2.Contains("-") ? Color.Black : Color.Green; //DeepPink
						item.Font = new Font(font.Name, font.Size + 0, FontStyle.Bold);
					}
					break;

				//case "Balance":
				//	item.Font = new Font(font.Name, font.Size, FontStyle.Bold);
				//	break;

				case "Column5":
					item.ForeColor = Color.Gray;
					break;

				case "Column6":
					item.ForeColor = Color.LightGray;
					break;
			}

			base.OnFormatCell(args);
		}

		public void BuildBalance()
		{
			
		}
	}
}