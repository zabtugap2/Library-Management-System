namespace LibraryManagementSystem.UserInterface_Forms
{
    partial class frmHistory
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
            this.grdDashboard = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdOverdue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdPenalty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.grdBookID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdBorrowDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdReturnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdLibarian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdBookTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdPublisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdEdition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdPublicationYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdAccountRenew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdPenaties = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDashboard
            // 
            this.grdDashboard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdDashboard.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.grdDashboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdDashboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDashboard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdBookID,
            this.grdBorrowDate,
            this.grdReturnDate,
            this.grdLibarian,
            this.grdBookTitle,
            this.grdAuthor,
            this.grdPublisher,
            this.grdEdition,
            this.grdCategory,
            this.grdLanguage,
            this.grdPublicationYear});
            this.grdDashboard.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.grdDashboard.Location = new System.Drawing.Point(12, 44);
            this.grdDashboard.Name = "grdDashboard";
            this.grdDashboard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdDashboard.Size = new System.Drawing.Size(733, 126);
            this.grdDashboard.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn5,
            this.grdOverdue,
            this.grdPenalty,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView1.Location = new System.Drawing.Point(12, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(733, 126);
            this.dataGridView1.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "BookID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Author";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // grdOverdue
            // 
            this.grdOverdue.HeaderText = "Overdue";
            this.grdOverdue.Name = "grdOverdue";
            // 
            // grdPenalty
            // 
            this.grdPenalty.HeaderText = "Penalty ₱";
            this.grdPenalty.Name = "grdPenalty";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Publisher";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Edition";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Category";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Language";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Publication Year";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdLogin,
            this.grdAccountRenew,
            this.grdPenaties,
            this.grdStatus});
            this.dataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dataGridView2.Location = new System.Drawing.Point(12, 390);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView2.Size = new System.Drawing.Size(733, 126);
            this.dataGridView2.TabIndex = 5;
            // 
            // grdBookID
            // 
            this.grdBookID.HeaderText = "BookID";
            this.grdBookID.Name = "grdBookID";
            // 
            // grdBorrowDate
            // 
            this.grdBorrowDate.HeaderText = "Borrow Date";
            this.grdBorrowDate.Name = "grdBorrowDate";
            // 
            // grdReturnDate
            // 
            this.grdReturnDate.HeaderText = "Return Date";
            this.grdReturnDate.Name = "grdReturnDate";
            // 
            // grdLibarian
            // 
            this.grdLibarian.HeaderText = "Librarian Name";
            this.grdLibarian.Name = "grdLibarian";
            // 
            // grdBookTitle
            // 
            this.grdBookTitle.HeaderText = "Book Title";
            this.grdBookTitle.Name = "grdBookTitle";
            // 
            // grdAuthor
            // 
            this.grdAuthor.HeaderText = "Author";
            this.grdAuthor.Name = "grdAuthor";
            // 
            // grdPublisher
            // 
            this.grdPublisher.HeaderText = "Publisher";
            this.grdPublisher.Name = "grdPublisher";
            // 
            // grdEdition
            // 
            this.grdEdition.HeaderText = "Edition";
            this.grdEdition.Name = "grdEdition";
            // 
            // grdCategory
            // 
            this.grdCategory.HeaderText = "Category";
            this.grdCategory.Name = "grdCategory";
            // 
            // grdLanguage
            // 
            this.grdLanguage.HeaderText = "Language";
            this.grdLanguage.Name = "grdLanguage";
            // 
            // grdPublicationYear
            // 
            this.grdPublicationYear.HeaderText = "Publication Year";
            this.grdPublicationYear.Name = "grdPublicationYear";
            // 
            // grdLogin
            // 
            this.grdLogin.HeaderText = "Login Date";
            this.grdLogin.Name = "grdLogin";
            // 
            // grdAccountRenew
            // 
            this.grdAccountRenew.HeaderText = "Times Renewed";
            this.grdAccountRenew.Name = "grdAccountRenew";
            // 
            // grdPenaties
            // 
            this.grdPenaties.HeaderText = "Penalties ";
            this.grdPenaties.Name = "grdPenaties";
            // 
            // grdStatus
            // 
            this.grdStatus.HeaderText = "Account Status";
            this.grdStatus.Name = "grdStatus";
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(749, 613);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.grdDashboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistory";
            this.Text = "frmHistory";
            ((System.ComponentModel.ISupportInitialize)(this.grdDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdDashboard;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdOverdue;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdPenalty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdBookID;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdBorrowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdReturnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdLibarian;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdBookTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdPublisher;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdEdition;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdPublicationYear;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdAccountRenew;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdPenaties;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdStatus;
    }
}