namespace LibraryManagementSystem.InterfaceAdmin
{
    partial class frmUserValidation
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAllUsers = new System.Windows.Forms.TabPage();
            this.tabPendingUsers = new System.Windows.Forms.TabPage();
            this.dgvAllUsers = new System.Windows.Forms.DataGridView();
            this.dgvPendingUsers = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabAllUsers.SuspendLayout();
            this.tabPendingUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAllUsers);
            this.tabControl1.Controls.Add(this.tabPendingUsers);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(977, 706);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAllUsers
            // 
            this.tabAllUsers.Controls.Add(this.dgvAllUsers);
            this.tabAllUsers.Location = new System.Drawing.Point(4, 25);
            this.tabAllUsers.Name = "tabAllUsers";
            this.tabAllUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllUsers.Size = new System.Drawing.Size(969, 677);
            this.tabAllUsers.TabIndex = 0;
            this.tabAllUsers.Text = "All Users";
            this.tabAllUsers.UseVisualStyleBackColor = true;
            // 
            // tabPendingUsers
            // 
            this.tabPendingUsers.Controls.Add(this.dgvPendingUsers);
            this.tabPendingUsers.Location = new System.Drawing.Point(4, 25);
            this.tabPendingUsers.Name = "tabPendingUsers";
            this.tabPendingUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPendingUsers.Size = new System.Drawing.Size(969, 677);
            this.tabPendingUsers.TabIndex = 1;
            this.tabPendingUsers.Text = "User Validation";
            this.tabPendingUsers.UseVisualStyleBackColor = true;
            // 
            // dgvAllUsers
            // 
            this.dgvAllUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllUsers.Location = new System.Drawing.Point(3, 3);
            this.dgvAllUsers.Name = "dgvAllUsers";
            this.dgvAllUsers.RowHeadersWidth = 51;
            this.dgvAllUsers.RowTemplate.Height = 24;
            this.dgvAllUsers.Size = new System.Drawing.Size(963, 671);
            this.dgvAllUsers.TabIndex = 0;
            this.dgvAllUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllUsers_CellContentClick);
            // 
            // dgvPendingUsers
            // 
            this.dgvPendingUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPendingUsers.Location = new System.Drawing.Point(3, 3);
            this.dgvPendingUsers.Name = "dgvPendingUsers";
            this.dgvPendingUsers.RowHeadersWidth = 51;
            this.dgvPendingUsers.RowTemplate.Height = 24;
            this.dgvPendingUsers.Size = new System.Drawing.Size(963, 671);
            this.dgvPendingUsers.TabIndex = 1;
            this.dgvPendingUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPendingUsers_CellContentClick_1);
            // 
            // frmUserValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(977, 706);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUserValidation";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.frmUserValidation_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAllUsers.ResumeLayout(false);
            this.tabPendingUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingUsers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAllUsers;
        private System.Windows.Forms.TabPage tabPendingUsers;
        private System.Windows.Forms.DataGridView dgvAllUsers;
        private System.Windows.Forms.DataGridView dgvPendingUsers;
    }
}