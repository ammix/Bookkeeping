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
			this.objectListView = new BrightIdeasSoftware.ObjectListView();
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
			this.SuspendLayout();
			// 
			// objectListView
			// 
			this.objectListView.CellEditUseWholeCell = false;
			this.objectListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.objectListView.Location = new System.Drawing.Point(0, 0);
			this.objectListView.Name = "objectListView";
			this.objectListView.Size = new System.Drawing.Size(767, 262);
			this.objectListView.TabIndex = 0;
			this.objectListView.UseCompatibleStateImageBehavior = false;
			this.objectListView.View = System.Windows.Forms.View.Details;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 262);
			this.Controls.Add(this.objectListView);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private BrightIdeasSoftware.ObjectListView objectListView;
	}
}

