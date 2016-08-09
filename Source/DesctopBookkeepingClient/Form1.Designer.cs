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
			this.olvColumn12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn13 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn15 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn16 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
			this.SuspendLayout();
			// 
			// treeListView
			// 
			this.treeListView.AllColumns.Add(this.olvColumn12);
			this.treeListView.AllColumns.Add(this.olvColumn13);
			this.treeListView.AllColumns.Add(this.olvColumn14);
			this.treeListView.AllColumns.Add(this.olvColumn15);
			this.treeListView.AllColumns.Add(this.olvColumn16);
			this.treeListView.AllColumns.Add(this.olvColumn1);
			this.treeListView.CellEditUseWholeCell = false;
			this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn12,
            this.olvColumn13,
            this.olvColumn14,
            this.olvColumn15,
            this.olvColumn16,
            this.olvColumn1});
			this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListView.FullRowSelect = true;
			this.treeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.treeListView.Location = new System.Drawing.Point(0, 0);
			this.treeListView.Name = "treeListView";
			this.treeListView.ShowGroups = false;
			this.treeListView.Size = new System.Drawing.Size(982, 611);
			this.treeListView.SmallImageList = this.imageList;
			this.treeListView.TabIndex = 1;
			this.treeListView.UseCompatibleStateImageBehavior = false;
			this.treeListView.View = System.Windows.Forms.View.Details;
			this.treeListView.VirtualMode = true;
			this.treeListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.treeListView_FormatCell);
			this.treeListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.treeListView_FormatRow);
			// 
			// olvColumn12
			// 
			this.olvColumn12.AspectName = "ColumnWithHierarchy";
			this.olvColumn12.Text = "Дата";
			this.olvColumn12.Width = 208;
			// 
			// olvColumn13
			// 
			this.olvColumn13.AspectName = "Amount";
			this.olvColumn13.Text = "Сума";
			this.olvColumn13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn13.Width = 192;
			// 
			// olvColumn14
			// 
			this.olvColumn14.AspectName = "Comment";
			this.olvColumn14.Text = "Примітка";
			this.olvColumn14.Width = 266;
			// 
			// olvColumn15
			// 
			this.olvColumn15.AspectName = "Acount";
			this.olvColumn15.Text = "Рахунок";
			this.olvColumn15.Width = 130;
			// 
			// olvColumn16
			// 
			this.olvColumn16.AspectName = "Balance";
			this.olvColumn16.Text = "Залишок";
			this.olvColumn16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.olvColumn16.Width = 120;
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Currency";
			this.olvColumn1.Text = "Валюта";
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
			this.ClientSize = new System.Drawing.Size(982, 611);
			this.Controls.Add(this.treeListView);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private BrightIdeasSoftware.TreeListView treeListView;
		private BrightIdeasSoftware.OLVColumn olvColumn12;
		private BrightIdeasSoftware.OLVColumn olvColumn15;
		private BrightIdeasSoftware.OLVColumn olvColumn13;
		private BrightIdeasSoftware.OLVColumn olvColumn14;
		private BrightIdeasSoftware.OLVColumn olvColumn16;
		private System.Windows.Forms.ImageList imageList;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
	}
}

