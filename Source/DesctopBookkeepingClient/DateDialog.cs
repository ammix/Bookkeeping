using System;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
	public partial class DateDialog : Form
	{
		public DateTime Date {get; set;}

		public DateDialog()
		{
			InitializeComponent();
		}

		private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
		{
			DialogResult = DialogResult.OK;
			Date = e.Start;
		}

		private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				DialogResult = DialogResult.Cancel;
		}
	}
}
