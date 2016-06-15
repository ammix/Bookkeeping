namespace DesktopBookkeepingClient
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.objectListView = new BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.treeListView1 = new BrightIdeasSoftware.TreeListView();
			this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
			this.SuspendLayout();
			// 
			// objectListView
			// 
			this.objectListView.AllColumns.Add(this.olvColumn1);
			this.objectListView.AllColumns.Add(this.olvColumn2);
			this.objectListView.AllColumns.Add(this.olvColumn3);
			this.objectListView.AllColumns.Add(this.olvColumn4);
			this.objectListView.AllColumns.Add(this.olvColumn5);
			this.objectListView.AllColumns.Add(this.olvColumn6);
			this.objectListView.CellEditUseWholeCell = false;
			this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6});
			this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.objectListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.objectListView.HasCollapsibleGroups = false;
			this.objectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.objectListView.Location = new System.Drawing.Point(0, 0);
			this.objectListView.Name = "objectListView";
			this.objectListView.Size = new System.Drawing.Size(767, 225);
			this.objectListView.TabIndex = 0;
			this.objectListView.UseCompatibleStateImageBehavior = false;
			this.objectListView.View = System.Windows.Forms.View.Details;
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Date";
			this.olvColumn1.IsVisible = false;
			this.olvColumn1.Text = "Дата";
			this.olvColumn1.Width = 0;
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "Account";
			this.olvColumn2.Text = "Рахунок";
			// 
			// olvColumn3
			// 
			this.olvColumn3.AspectName = "Increment";
			this.olvColumn3.Text = "Рух";
			// 
			// olvColumn4
			// 
			this.olvColumn4.AspectName = "Supplier";
			this.olvColumn4.Text = "Постачальник";
			// 
			// olvColumn5
			// 
			this.olvColumn5.AspectName = "Amount";
			this.olvColumn5.Text = "Сума";
			// 
			// olvColumn6
			// 
			this.olvColumn6.AspectName = "Comment";
			this.olvColumn6.Text = "Примітка";
			// 
			// treeListView1
			// 
			this.treeListView1.AllColumns.Add(this.olvColumn7);
			this.treeListView1.AllColumns.Add(this.olvColumn8);
			this.treeListView1.AllColumns.Add(this.olvColumn9);
			this.treeListView1.CellEditUseWholeCell = false;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9});
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.treeListView1.Location = new System.Drawing.Point(0, 276);
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.ShowGroups = false;
			this.treeListView1.Size = new System.Drawing.Size(767, 267);
			this.treeListView1.TabIndex = 1;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.View = System.Windows.Forms.View.Details;
			this.treeListView1.VirtualMode = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 543);
			this.Controls.Add(this.treeListView1);
			this.Controls.Add(this.objectListView);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BrightIdeasSoftware.ObjectListView objectListView;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private BrightIdeasSoftware.OLVColumn olvColumn3;
		private BrightIdeasSoftware.OLVColumn olvColumn4;
		private BrightIdeasSoftware.OLVColumn olvColumn5;
		private BrightIdeasSoftware.OLVColumn olvColumn6;
		private BrightIdeasSoftware.TreeListView treeListView1;
		private BrightIdeasSoftware.OLVColumn olvColumn7;
		private BrightIdeasSoftware.OLVColumn olvColumn8;
		private BrightIdeasSoftware.OLVColumn olvColumn9;
	}
}

