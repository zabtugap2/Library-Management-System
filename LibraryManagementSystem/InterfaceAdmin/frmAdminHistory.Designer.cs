namespace LibraryManagementSystem.InterfaceAdmin
{
    partial class frmAdminHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabAdminHistory = new System.Windows.Forms.TabControl();
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.tabBorrow = new System.Windows.Forms.TabPage();
            this.tabPayments = new System.Windows.Forms.TabPage();
            this.dgvBookHistory = new System.Windows.Forms.DataGridView();
            this.dgvBorrowHistory = new System.Windows.Forms.DataGridView();
            this.dgvPaymentHistory = new System.Windows.Forms.DataGridView();
            this.tabAdminHistory.SuspendLayout();
            this.tabBooks.SuspendLayout();
            this.tabBorrow.SuspendLayout();
            this.tabPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tabAdminHistory
            // 
            this.tabAdminHistory.Controls.Add(this.tabBooks);
            this.tabAdminHistory.Controls.Add(this.tabBorrow);
            this.tabAdminHistory.Controls.Add(this.tabPayments);
            this.tabAdminHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAdminHistory.Location = new System.Drawing.Point(0, 0);
            this.tabAdminHistory.Name = "tabAdminHistory";
            this.tabAdminHistory.SelectedIndex = 0;
            this.tabAdminHistory.Size = new System.Drawing.Size(977, 706);
            this.tabAdminHistory.TabIndex = 0;
            // 
            // tabBooks
            // 
            this.tabBooks.Controls.Add(this.dgvBookHistory);
            this.tabBooks.Location = new System.Drawing.Point(4, 25);
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooks.Size = new System.Drawing.Size(969, 677);
            this.tabBooks.TabIndex = 0;
            this.tabBooks.Text = "📚 Book History";
            this.tabBooks.UseVisualStyleBackColor = true;
            // 
            // tabBorrow
            // 
            this.tabBorrow.Controls.Add(this.dgvBorrowHistory);
            this.tabBorrow.Location = new System.Drawing.Point(4, 25);
            this.tabBorrow.Name = "tabBorrow";
            this.tabBorrow.Padding = new System.Windows.Forms.Padding(3);
            this.tabBorrow.Size = new System.Drawing.Size(969, 677);
            this.tabBorrow.TabIndex = 1;
            this.tabBorrow.Text = "🔄 Borrow / Return";
            this.tabBorrow.UseVisualStyleBackColor = true;
            // 
            // tabPayments
            // 
            this.tabPayments.Controls.Add(this.dgvPaymentHistory);
            this.tabPayments.Location = new System.Drawing.Point(4, 25);
            this.tabPayments.Name = "tabPayments";
            this.tabPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayments.Size = new System.Drawing.Size(969, 677);
            this.tabPayments.TabIndex = 2;
            this.tabPayments.Text = "💰 Payments";
            this.tabPayments.UseVisualStyleBackColor = true;
            // 
            // dgvBookHistory
            // 
            this.dgvBookHistory.AllowUserToAddRows = false;
            this.dgvBookHistory.AllowUserToDeleteRows = false;
            this.dgvBookHistory.BackgroundColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBookHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBookHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvBookHistory.Name = "dgvBookHistory";
            this.dgvBookHistory.ReadOnly = true;
            this.dgvBookHistory.RowHeadersWidth = 51;
            this.dgvBookHistory.RowTemplate.Height = 24;
            this.dgvBookHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookHistory.Size = new System.Drawing.Size(963, 671);
            this.dgvBookHistory.TabIndex = 0;
            this.dgvBookHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBookHistory_CellContentClick);
            // 
            // dgvBorrowHistory
            // 
            this.dgvBorrowHistory.AllowUserToAddRows = false;
            this.dgvBorrowHistory.AllowUserToDeleteRows = false;
            this.dgvBorrowHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBorrowHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvBorrowHistory.Name = "dgvBorrowHistory";
            this.dgvBorrowHistory.ReadOnly = true;
            this.dgvBorrowHistory.RowHeadersWidth = 51;
            this.dgvBorrowHistory.RowTemplate.Height = 24;
            this.dgvBorrowHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowHistory.Size = new System.Drawing.Size(963, 671);
            this.dgvBorrowHistory.TabIndex = 1;
            this.dgvBorrowHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBorrowHistory_CellContentClick);
            // 
            // dgvPaymentHistory
            // 
            this.dgvPaymentHistory.AllowUserToAddRows = false;
            this.dgvPaymentHistory.AllowUserToDeleteRows = false;
            this.dgvPaymentHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPaymentHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvPaymentHistory.Name = "dgvPaymentHistory";
            this.dgvPaymentHistory.ReadOnly = true;
            this.dgvPaymentHistory.RowHeadersWidth = 51;
            this.dgvPaymentHistory.RowTemplate.Height = 24;
            this.dgvPaymentHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaymentHistory.Size = new System.Drawing.Size(963, 671);
            this.dgvPaymentHistory.TabIndex = 1;
            // 
            // frmAdminHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(977, 706);
            this.Controls.Add(this.tabAdminHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmAdminHistory";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.frmAdminHistory_Load);
            this.tabAdminHistory.ResumeLayout(false);
            this.tabBooks.ResumeLayout(false);
            this.tabBorrow.ResumeLayout(false);
            this.tabPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAdminHistory;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.TabPage tabBorrow;
        private System.Windows.Forms.DataGridView dgvBookHistory;
        private System.Windows.Forms.TabPage tabPayments;
        private System.Windows.Forms.DataGridView dgvBorrowHistory;
        private System.Windows.Forms.DataGridView dgvPaymentHistory;
    }
}