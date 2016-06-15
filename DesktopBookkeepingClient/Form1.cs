using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			InitializeListView();
		}

		private void InitializeListView()
		{
			objectListView.SetObjects(Transaction.GetTransactions());
		}
	}
}
