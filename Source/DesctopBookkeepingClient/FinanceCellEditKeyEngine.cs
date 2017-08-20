using System.Windows.Forms;
using BrightIdeasSoftware;

namespace DesktopBookkeepingClient
{
	public class FinanceCellEditKeyEngine : CellEditKeyEngine
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
				if (string.IsNullOrEmpty(row.Column2) || string.IsNullOrEmpty(row.Account))
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
				if (string.IsNullOrEmpty(rowLine.Article) || string.IsNullOrEmpty(rowLine.Column2) ||
				    !list.ValidateArticle(rowLine)) //TODO !!!
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
}