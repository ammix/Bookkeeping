﻿namespace DesktopBookkeepingClient
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeListView = new DesktopBookkeepingClient.FinanceTreeListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.валютиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addProfitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.виToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addInvoiceLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeInvoiceLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.contextMenuStrip3.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabControl1.Location = new System.Drawing.Point(0, 82);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1167, 529);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1159, 500);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Головна";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeListView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(1153, 494);
			this.splitContainer1.SplitterDistance = 399;
			this.splitContainer1.TabIndex = 2;
			// 
			// treeListView
			// 
			this.treeListView.AllColumns.Add(this.olvColumn1);
			this.treeListView.AllColumns.Add(this.olvColumn2);
			this.treeListView.AllColumns.Add(this.olvColumn3);
			this.treeListView.AllColumns.Add(this.olvColumn5);
			this.treeListView.AllColumns.Add(this.olvColumn4);
			this.treeListView.AllColumns.Add(this.olvColumn6);
			this.treeListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
			this.treeListView.CellEditUseWholeCell = false;
			this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn5,
            this.olvColumn4,
            this.olvColumn6});
			this.treeListView.CurrentItem = null;
			this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.treeListView.FullRowSelect = true;
			this.treeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.treeListView.Location = new System.Drawing.Point(0, 0);
			this.treeListView.Name = "treeListView";
			this.treeListView.ShowGroups = false;
			this.treeListView.Size = new System.Drawing.Size(1153, 399);
			this.treeListView.SmallImageList = this.imageList;
			this.treeListView.TabIndex = 1;
			this.treeListView.UseCompatibleStateImageBehavior = false;
			this.treeListView.View = System.Windows.Forms.View.Details;
			this.treeListView.VirtualMode = true;
			this.treeListView.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.treeListView_CellEditStarting);
			this.treeListView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.treeListView_CellRightClick);
			this.treeListView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.treeListView_FormatCell);
			this.treeListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.treeListView_FormatRow);
			this.treeListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeListView_KeyPress);
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Column1";
			this.olvColumn1.CellEditUseWholeCell = true;
			this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			this.olvColumn1.Text = "Контрагент";
			this.olvColumn1.Width = 200;
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "Column2";
			this.olvColumn2.CellEditUseWholeCell = true;
			this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn2.Text = "Сума";
			this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.olvColumn2.Width = 100;
			// 
			// olvColumn3
			// 
			this.olvColumn3.AspectName = "Column3";
			this.olvColumn3.CellEditUseWholeCell = true;
			this.olvColumn3.CellPadding = new System.Drawing.Rectangle(10, 0, 0, 0);
			this.olvColumn3.FillsFreeSpace = true;
			this.olvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn3.Text = "Коментар";
			this.olvColumn3.Width = 460;
			this.olvColumn3.WordWrap = true;
			// 
			// olvColumn5
			// 
			this.olvColumn5.AspectName = "Column4";
			this.olvColumn5.CellEditUseWholeCell = true;
			this.olvColumn5.CellPadding = new System.Drawing.Rectangle(10, 0, 0, 0);
			this.olvColumn5.DisplayIndex = 4;
			this.olvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn5.Text = "Рахунок";
			this.olvColumn5.Width = 180;
			// 
			// olvColumn4
			// 
			this.olvColumn4.AspectName = "Column5";
			this.olvColumn4.CellEditUseWholeCell = true;
			this.olvColumn4.DisplayIndex = 3;
			this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.olvColumn4.IsEditable = false;
			this.olvColumn4.Text = "Залишок";
			this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.olvColumn4.Width = 150;
			// 
			// olvColumn6
			// 
			this.olvColumn6.AspectName = "Column6";
			this.olvColumn6.CellEditUseWholeCell = true;
			this.olvColumn6.IsEditable = false;
			this.olvColumn6.Text = "Час";
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1153, 91);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Підсумки";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1159, 500);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Рахунки";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripComboBox1,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7});
			this.toolStrip1.Location = new System.Drawing.Point(0, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1167, 57);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton1.Text = "toolStripButton1";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton2.Text = "toolStripButton2";
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(39, 54);
			this.toolStripButton3.Text = "+";
			this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 57);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton4.Text = "toolStripButton4";
			this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton5.Text = "toolStripButton5";
			this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
			// 
			// toolStripButton6
			// 
			this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
			this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton6.Text = "toolStripButton6";
			this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
			// 
			// toolStripButton7
			// 
			this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
			this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Size = new System.Drawing.Size(54, 54);
			this.toolStripButton7.Text = "toolStripButton7";
			this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.валютиToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1167, 25);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 21);
			this.toolStripMenuItem1.Text = "Сервіс";
			// 
			// валютиToolStripMenuItem
			// 
			this.валютиToolStripMenuItem.Name = "валютиToolStripMenuItem";
			this.валютиToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
			this.валютиToolStripMenuItem.Text = "Валюти";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTransactionToolStripMenuItem,
            this.addProfitToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(169, 48);
			// 
			// addTransactionToolStripMenuItem
			// 
			this.addTransactionToolStripMenuItem.Name = "addTransactionToolStripMenuItem";
			this.addTransactionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.addTransactionToolStripMenuItem.Text = "Додати трату";
			this.addTransactionToolStripMenuItem.Click += new System.EventHandler(this.addTransactionToolStripMenuItem_Click);
			// 
			// addProfitToolStripMenuItem
			// 
			this.addProfitToolStripMenuItem.Name = "addProfitToolStripMenuItem";
			this.addProfitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.addProfitToolStripMenuItem.Text = "Додати прибуток";
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.виToolStripMenuItem,
            this.addInvoiceLineToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(193, 48);
			// 
			// виToolStripMenuItem
			// 
			this.виToolStripMenuItem.Name = "виToolStripMenuItem";
			this.виToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.виToolStripMenuItem.Text = "Видалити трансакцію";
			this.виToolStripMenuItem.Click += new System.EventHandler(this.removeTransactionToolStripMenuItem_Click);
			// 
			// addInvoiceLineToolStripMenuItem
			// 
			this.addInvoiceLineToolStripMenuItem.Name = "addInvoiceLineToolStripMenuItem";
			this.addInvoiceLineToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addInvoiceLineToolStripMenuItem.Text = "Парсити чек";
			this.addInvoiceLineToolStripMenuItem.Click += new System.EventHandler(this.addInvoiceLineToolStripMenuItem_Click);
			// 
			// contextMenuStrip3
			// 
			this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeInvoiceLineToolStripMenuItem});
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(160, 26);
			// 
			// removeInvoiceLineToolStripMenuItem
			// 
			this.removeInvoiceLineToolStripMenuItem.Name = "removeInvoiceLineToolStripMenuItem";
			this.removeInvoiceLineToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.removeInvoiceLineToolStripMenuItem.Text = "Видалити лінію";
			this.removeInvoiceLineToolStripMenuItem.Click += new System.EventHandler(this.removeInvoiceLineToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(662, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 25);
			this.label1.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1167, 611);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.contextMenuStrip3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem валютиToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private FinanceTreeListView treeListView;
		private System.Windows.Forms.ToolStripButton toolStripButton6;
		private System.Windows.Forms.ToolStripButton toolStripButton7;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addTransactionToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem виToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addProfitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addInvoiceLineToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
		private System.Windows.Forms.ToolStripMenuItem removeInvoiceLineToolStripMenuItem;
		private System.Windows.Forms.Label label1;
	}
}

