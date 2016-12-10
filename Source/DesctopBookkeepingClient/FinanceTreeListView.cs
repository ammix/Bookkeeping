using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopBookkeepingClient
{
    public class FinanceTreeListView: TreeListView
    {
        public bool newDayTransaction;

        public FinanceTreeListView()
        {
            CellEditStarting += financeTreeListView_CellEditStarting;
        }

        public override void CancelCellEdit()
        {
            base.CancelCellEdit();

            if (newDayTransaction)
            {
                var enumerator = Roots.GetEnumerator();
                enumerator.MoveNext();

                RemoveObject(enumerator.Current);
                newDayTransaction = false;
            }
        }

        private void financeTreeListView_CellEditStarting(object sender, CellEditEventArgs e)
        {
            // We only want to mess with the Cooking Skill column
            if (e.Column.Text != "Контрагент")
                return;

            //Person personBeingEdited = (Person)e.RowObject;
            ComboBox cb = new ComboBox();
            cb.Bounds = e.CellBounds;
            cb.Font = ((ObjectListView)sender).Font;
            cb.DropDownStyle = ComboBoxStyle.DropDown; // DropDownList;
            cb.Items.AddRange(new object[] { "Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef" });
            //cb.SelectedIndex = Math.Max(0, Math.Min(cb.Items.Count - 1, ((int)e.Value) / 10));
            //cb.SelectedIndexChanged += delegate (object o, EventArgs args) {
            //    personBeingEdited.CulinaryRating = cb.SelectedIndex * 10;
            //};
            e.Control = cb;
        }
    }
}
