using BrightIdeasSoftware;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
    public class FinanceCellEditKeyEngine: CellEditKeyEngine
    {
	    protected override void HandleEndEdit()
	    {
		    var row = (TreeListViewModel) ItemBeingEdited.RowObject;

		    if (string.IsNullOrEmpty(row.Amount) || string.IsNullOrEmpty(row.Account))
		    {
			    MessageBox.Show("Ціна і рахунок мають бути заповнені", "Помилка створення транзакції",
				    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			    return;
		    }

		    base.HandleEndEdit();

			if (row.Id == 0)
				LocalDb.InsertTransaction(row);
			else
				LocalDb.UpdateTransaction(row);
	    }
    }

    public class FinanceTreeListView: TreeListView
    {
	    public TreeListViewModel CurrentItem;
	    ComboBox treeComboBox;
	    TextBox amountTextBox;
		TextBox commentTextBox;
		ComboBox accountComboBox;

		//public struct A
		//{
		//	public string Coun;
		//	int id;
		//}

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
        //    base.OnValidating(e);
        //}

        protected override void OnCellEditFinishing(CellEditEventArgs e)
		{
			base.OnCellEditFinishing(e);

			RefreshItem(e.ListViewItem);

			e.Cancel = true;
		    e.AutoDispose = false;
		}

	    protected override void OnCellEditFinished(CellEditEventArgs e)
	    {
		    base.OnCellEditFinished(e);

			//if (e.Column.AspectName == "Amount" && string.IsNullOrEmpty(((TreeListViewModel)e.RowObject).Amount))
			//	StartCellEdit(GetItem(1), 1);	

			//if (string.IsNullOrEmpty(((TreeListViewModel) e.RowObject).Amount))
			// StartCellEdit(GetItem(1), 1);
			//else if (string.IsNullOrEmpty(((TreeListViewModel) e.RowObject).Account))
			// StartCellEdit(GetItem(1), 4);
			//else
			//{
			// CurrentItem = null;
			//}

			CurrentItem = null;
			RebuildAll(true);
		}

	    public override void CancelCellEdit()
		{
			base.CancelCellEdit();

			if (CurrentItem != null)
			{
				CurrentItem._parent.Nodes.Remove(CurrentItem);
				CurrentItem = null;
			}

			RebuildAll(true);
		}

		protected override void OnCellEditStarting(CellEditEventArgs e)
	    {
			base.OnCellEditStarting(e);

			switch (e.Column.AspectName)
		    {
				case "Tree":
					treeComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
					treeComboBox.Items.AddRange(LocalDb.GetCounterparties());

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

				//case "Comment":
				//	//commentTextBox = new TextBox();

				//	commentTextBox.Font = Font;
				//	commentTextBox.Bounds = e.CellBounds;
				//	commentTextBox.Text = (string)e.Value;
				//	commentTextBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Comment = commentTextBox.Text;
				//	e.Control = commentTextBox;
				//	break;

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
    }
}
