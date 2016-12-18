﻿using BrightIdeasSoftware;
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
        private ComboBox cb;
		public List<TreeListViewModel> Model;

		public FinanceTreeListView()
        {
			//treeListView.Roots = model = MockDb.GetTransactions();

			cb = new ComboBox();
            //cb.Bounds = e.CellBounds;
            //cb.Font = ((ObjectListView)sender).Font;
            //cb.DropDownStyle = ComboBoxStyle.DropDown; // DropDownList;
            //cb.Items.AddRange(new object[] { "Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef" });

            CellEditStarting += financeTreeListView_CellEditStarting;
            CellEditFinished += FinanceTreeListView_CellEditFinished;
        }

	    public void AddModel(List<TreeListViewModel> model)
	    {
		    Roots = model;
		    this.Model = model;
	    }

        private void FinanceTreeListView_CellEditFinished(object sender, CellEditEventArgs e)
        {
            //var q = Roots.GetEnumerator().Current; //cb.Text
            //var cell = GetSubItem(1,0); // (e.ListViewItem.Index, e.SubItemIndex);
            //cell.Text = "Hello, world"; // cb.Text;

            //e.ListViewItem.SubItems[0].Text = "Hello, world!";
            //e.ListViewItem.SubItems[0].Text = e.Control.Text;
            //Model[0].Nodes[0].Tree = "Hello, world!";
            //Model[0].Nodes[0].Tree = e.Control.Text;
            //Model.Add();

            var enumerator = Objects.GetEnumerator();
            enumerator.MoveNext();
            TreeListViewModel test = (TreeListViewModel)enumerator.Current;
            test.Nodes[0].Tree = _text; // e.Control.Text;
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
            //if (e.Column.AspectName != "Tree")
            //    return;

            //Person personBeingEdited = (Person)e.RowObject;
            //ComboBox cb = new ComboBox();
            //cb.SelectedValueChanged += Cb_SelectedValueChanged;
            //cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
            cb.Bounds = e.CellBounds;
            cb.Font = ((ObjectListView)sender).Font;
            cb.DropDownStyle = ComboBoxStyle.DropDown; // DropDownList;
            cb.Items.AddRange(new object[] { "Pay to eat out", "Suggest take-away", "Passable", "Seek dinner invitation", "Hire as chef" });
            //cb.Text = (string)e.Value;
            //cb.SelectedIndex = Math.Max(0, Math.Min(cb.Items.Count - 1, ((int)e.Value) / 10));
            //cb.SelectedIndexChanged += delegate (object o, EventArgs args) {
            //    personBeingEdited.CulinaryRating = cb.SelectedIndex * 10;
            //};
            e.Control = cb;
        }

        private void Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        string _text;
        private void Cb_SelectedValueChanged(object sender, EventArgs e)
        {

            _text = ((ComboBox)sender).SelectedItem.ToString();
        }

        //Known issues:
        // 1. Коли контрол, то починається з нуля, інакше починається із відступом так як в дереві
        // 2. Контрол працює лише після другого кліка
    }
}
