using System;
using BrightIdeasSoftware;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
    public class FinanceTreeListView: TreeListView
    {
	    public TreeListViewModel CurrentItem;
	    readonly ComboBox treeComboBox;
	    readonly TextBox amountTextBox;
	    readonly ComboBox accountComboBox;

	    public FinanceTreeListView()
	    {
		    treeComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
		    treeComboBox.Items.AddRange(new object[] { "Сільпо", "Алейка", "Кишеня", "Твінфілд", "Аптека" });

			amountTextBox = new TextBox();

			accountComboBox = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown /*DropDownList*/ };
			accountComboBox.Items.AddRange(new object[] { "Готівка", "Картка", "Ничка" });
        }

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
	    }

	    public override void CancelCellEdit()
		{
			base.CancelCellEdit();

			if (CurrentItem != null)
			{
				RemoveObject(CurrentItem);
				CurrentItem = null;
			}
		}

		protected override void OnCellEditStarting(CellEditEventArgs e)
	    {
			base.OnCellEditStarting(e);

			switch (e.Column.AspectName)
		    {
				case "Tree":
					treeComboBox.Font = Font;
					treeComboBox.Bounds = e.CellBounds;
					treeComboBox.Text = (string)e.Value;
					treeComboBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Tree = treeComboBox.Text;
					e.Control = treeComboBox;
					break;

				case "Amount":
					amountTextBox.Font = Font;
					amountTextBox.Bounds = e.CellBounds;
					amountTextBox.Text = (string)e.Value;
					amountTextBox.TextChanged += (o, args) => ((TreeListViewModel)e.RowObject).Amount = amountTextBox.Text;
					e.Control = amountTextBox;
					break;

				case "Account":
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
