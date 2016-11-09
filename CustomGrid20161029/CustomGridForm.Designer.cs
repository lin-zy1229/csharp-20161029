namespace CustomGrid20161029
{
    partial class CustomGridForm
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.conMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.state1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.state2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.state3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.conMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.ColumnHeadersVisible = false;
            this.grid.Location = new System.Drawing.Point(4, 4);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.Size = new System.Drawing.Size(469, 295);
            this.grid.TabIndex = 0;
            this.grid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseClick);
            this.grid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellValueChanged);
            this.grid.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnAdded);
            this.grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            this.grid.SizeChanged += new System.EventHandler(this.grid_SizeChanged);
            this.grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grid_MouseClick);
            // 
            // conMenu
            // 
            this.conMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.state1ToolStripMenuItem,
            this.state2ToolStripMenuItem,
            this.state3ToolStripMenuItem});
            this.conMenu.Name = "conMenu";
            this.conMenu.Size = new System.Drawing.Size(153, 142);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Add";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.menuitem_add_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Remove";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.menuitem_remove_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // state1ToolStripMenuItem
            // 
            this.state1ToolStripMenuItem.Name = "state1ToolStripMenuItem";
            this.state1ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.state1ToolStripMenuItem.Text = "State 1";
            this.state1ToolStripMenuItem.Click += new System.EventHandler(this.state1ToolStripMenuItem_Click);
            // 
            // state2ToolStripMenuItem
            // 
            this.state2ToolStripMenuItem.Name = "state2ToolStripMenuItem";
            this.state2ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.state2ToolStripMenuItem.Text = "State 2";
            this.state2ToolStripMenuItem.Click += new System.EventHandler(this.state2ToolStripMenuItem_Click);
            // 
            // state3ToolStripMenuItem
            // 
            this.state3ToolStripMenuItem.Name = "state3ToolStripMenuItem";
            this.state3ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.state3ToolStripMenuItem.Text = "State 3";
            this.state3ToolStripMenuItem.Click += new System.EventHandler(this.state3ToolStripMenuItem_Click);
            // 
            // CustomGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 301);
            this.Controls.Add(this.grid);
            this.Name = "CustomGridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomGrid Window";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.conMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ContextMenuStrip conMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem state1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem state2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem state3ToolStripMenuItem;
    }
}

