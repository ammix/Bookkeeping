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
			this.treeListView = new BrightIdeasSoftware.TreeListView();
			this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
			this.SuspendLayout();
			// 
			// treeListView
			// 
			this.treeListView.AllColumns.Add(this.olvColumn7);
			this.treeListView.AllColumns.Add(this.olvColumn8);
			this.treeListView.AllColumns.Add(this.olvColumn9);
			this.treeListView.AllColumns.Add(this.olvColumn10);
			this.treeListView.AllColumns.Add(this.olvColumn11);
			this.treeListView.CellEditUseWholeCell = false;
			this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10,
            this.olvColumn11});
			this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.treeListView.Location = new System.Drawing.Point(0, 0);
			this.treeListView.Name = "treeListView";
			this.treeListView.ShowGroups = false;
			this.treeListView.Size = new System.Drawing.Size(767, 611);
			this.treeListView.SmallImageList = this.imageList;
			this.treeListView.TabIndex = 1;
			this.treeListView.UseCompatibleStateImageBehavior = false;
			this.treeListView.View = System.Windows.Forms.View.Details;
			this.treeListView.VirtualMode = true;
			this.treeListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.treeListView_FormatCell);
			// 
			// olvColumn7
			// 
			this.olvColumn7.AspectName = "Account";
			this.olvColumn7.Text = "Рахунок";
			this.olvColumn7.Width = 200;
			// 
			// olvColumn8
			// 
			this.olvColumn8.AspectName = "CustomerOrSupplier";
			this.olvColumn8.Text = "Постачальник";
			this.olvColumn8.Width = 101;
			// 
			// olvColumn9
			// 
			this.olvColumn9.AspectName = "Amount";
			this.olvColumn9.Text = "Сума";
			this.olvColumn9.Width = 100;
			// 
			// olvColumn10
			// 
			this.olvColumn10.AspectName = "Comment";
			this.olvColumn10.Text = "Примітка";
			this.olvColumn10.Width = 200;
			// 
			// olvColumn11
			// 
			this.olvColumn11.AspectName = "Balance";
			this.olvColumn11.Text = "Залишок";
			this.olvColumn11.Width = 150;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 611);
			this.Controls.Add(this.treeListView);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private BrightIdeasSoftware.TreeListView treeListView;
		private BrightIdeasSoftware.OLVColumn olvColumn7;
		private BrightIdeasSoftware.OLVColumn olvColumn8;
		private BrightIdeasSoftware.OLVColumn olvColumn9;
		private BrightIdeasSoftware.OLVColumn olvColumn10;
		private BrightIdeasSoftware.OLVColumn olvColumn11;
		private System.Windows.Forms.ImageList imageList;
	}
}

