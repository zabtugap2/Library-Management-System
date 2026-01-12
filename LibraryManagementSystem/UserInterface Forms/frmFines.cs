using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagementSystem.Database;

namespace LibraryManagementSystem.UserInterface_Forms
{
    public partial class frmFines : Form
    {
        private decimal overdueTotal = 0;
        private decimal lostBookFee = 300;
        private decimal damagedBookFee = 150;
        private decimal lostCardFee = 100;

        public frmFines()
        {
            InitializeComponent();

            // Events that match the designer
            cmbPaymentMethod.SelectedIndexChanged += cmbPaymentMethod_SelectedIndexChanged;

            chkLostBook.CheckedChanged += Charges_Changed;
            chkDamagedBook.CheckedChanged += Charges_Changed;
            chkLostCard.CheckedChanged += Charges_Changed;

            btnRequestWaiver.Click += btnRequestWaiver_Click;
            btnPay.Click += btnPay_Click;
        }

        // ===================== FORM LOAD =====================
        private void frmFines_Load(object sender, EventArgs e)
        {
                LoadOverdue();
            LoadWaiverHistory();
            LoadPaymentHistory();

            txtLostFee.Text = lostBookFee.ToString();
            txtDamagedFee.Text = damagedBookFee.ToString();
            txtLostCardFee.Text = lostCardFee.ToString();
        }

        // ===================== LOAD OVERDUE =====================
        private void LoadOverdue()
        {
            overdueTotal = 0;

            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string sql =
                @"SELECT F.FineID, B.Title, F.DaysLate, F.FineAmount
                  FROM Fines F
                  INNER JOIN BookBorrow BB ON F.BorrowID = BB.BorrowID
                  INNER JOIN Books B ON BB.BookID = B.BookID
                  WHERE F.UserID = @UID AND F.IsCleared = 0";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dgvOverdue.DataSource = dt;
                }
            }

            foreach (DataGridViewRow row in dgvOverdue.Rows)
            {
                if (row.Cells["FineAmount"].Value != null)
                {
                    overdueTotal += Convert.ToDecimal(row.Cells["FineAmount"].Value);
                }
            }

            txtTotalOverdue.Text = overdueTotal.ToString("0.00");
            ComputeGrandTotal();
        }

        // ===================== CHARGES CHANGE EVENT =====================
        private void Charges_Changed(object sender, EventArgs e)
        {
            ComputeGrandTotal();
        }

        // ===================== COMPUTE GRAND TOTAL =====================
        private void ComputeGrandTotal()
        {
            decimal total = overdueTotal;

            if (chkLostBook.Checked) total += lostBookFee;
            if (chkDamagedBook.Checked) total += damagedBookFee;
            if (chkLostCard.Checked) total += lostCardFee;

            txtGrandTotal.Text = total.ToString("0.00");
        }

        // ===================== PAYMENT METHOD =====================
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPaymentMethod.Text)
            {
                case "Gcash":
                    txtPaymentDetails.Text = "GCash #: 09279521545";
                    break;
                case "Paypal":
                    txtPaymentDetails.Text = "PayPal: library@email.com";
                    break;
                case "Cash":
                    txtPaymentDetails.Text = "Pay at librarian counter";
                    break;
                default:
                    txtPaymentDetails.Clear();
                    break;
            }
        }

        // ===================== PAY BUTTON =====================
        private void btnPay_Click(object sender, EventArgs e)
        {

            if (txtGrandTotal.Text == "0.00")
            {
                MessageBox.Show("No charges selected to pay.");
                return;
            }

            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method before proceeding.");
                return;
            }

            //====================== PAYMENT METHOD =====================
            if (cmbPaymentMethod.Text == "Cash")
            {
                MessageBox.Show(
                    "Please proceed to the librarian counter to pay in cash.\n" +
                    "Your payment will be processed and approved by the librarian.",
                    "Cash Payment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            string proofPath = null;

            if (cmbPaymentMethod.Text == "Gcash")
            {
                DialogResult result = MessageBox.Show(
                    "GCash Payment Instructions:\n\n" +
                    "Send payment to:\n" +
                    "?? 0927-952-1545\n" +
                    "Name: Library Management System\n\n" +
                    "Send exact amount.\n" +
                    "After sending, click OK to upload your screenshot proof.",
                    "GCash Payment",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                );

                if (result != DialogResult.OK)
                    return;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Upload GCash Payment Screenshot";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    proofPath = ofd.FileName;
                }
                // ?? FINAL VALIDATION BEFORE SAVING
                else 
                {
                    MessageBox.Show("GCash payments require screenshot proof.");
                    return;
                }

            }




            // Check if user has overdue fines
            bool hasOverdue = dgvOverdue.Rows.Count > 0;

            // Check if user has borrowed books
            bool hasBorrowedBooks = false;
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();
                string checkBorrow = @"SELECT COUNT(*) FROM BookBorrow 
                               WHERE UserID = @UID AND Status = 'Borrowed'";
                using (SqlCommand cmd = new SqlCommand(checkBorrow, con))
                {
                    cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);
                    hasBorrowedBooks = ((int)cmd.ExecuteScalar()) > 0;
                }
            }

            // Check if other charges selected
            bool lostBookSelected = chkLostBook.Checked;
            bool damagedBookSelected = chkDamagedBook.Checked;
            bool lostCardSelected = chkLostCard.Checked;

            // Validation for other charges
            if ((lostBookSelected || damagedBookSelected) && !hasBorrowedBooks)
            {
                MessageBox.Show("You don't have any borrowed books. Cannot pay Lost/Damaged charges yet.");
                return;
            }

            // Validation for total payment
            if (!hasOverdue && !lostBookSelected && !damagedBookSelected && !lostCardSelected)
            {
                MessageBox.Show("You don't have any fines or charges to pay.");
                return;
            }

            decimal amount = Convert.ToDecimal(txtGrandTotal.Text);

            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                // Pay overdue fines
                foreach (DataGridViewRow row in dgvOverdue.Rows)
                {
                    int fineId = Convert.ToInt32(row.Cells["FineID"].Value);
                    decimal fineAmount = Convert.ToDecimal(row.Cells["FineAmount"].Value);

                    if (fineAmount > 0)
                    {
                        string sqlFinePayment =
                        @"INSERT INTO Payments (FineID, UserID, AmountPaid, PaymentMethod, ProofImagePath, Status)
                        VALUES (@FineID, @UID, @Amount, @Method, @Proof, 'Pending')";

                        using (SqlCommand cmd = new SqlCommand(sqlFinePayment, con))
                        {
                            cmd.Parameters.AddWithValue("@FineID", fineId);
                            cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);
                            cmd.Parameters.AddWithValue("@Amount", fineAmount);
                            cmd.Parameters.AddWithValue("@Method", cmbPaymentMethod.Text);
                            cmd.Parameters.AddWithValue("@Proof",(object)proofPath ?? DBNull.Value);


                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Pay other charges
                if (lostBookSelected)
                    InsertOtherCharge(con, "Lost Book", lostBookFee);

                if (damagedBookSelected)
                    InsertOtherCharge(con, "Damaged Book", damagedBookFee);

                if (lostCardSelected)
                    InsertOtherCharge(con, "Lost Card", lostCardFee);
            }

            MessageBox.Show("Payment submitted!\nPlease wait for librarian approval.");
            LoadOverdue();
            LoadPaymentHistory();
            ClearFields();
        }


        // ===================== INSERT OTHER CHARGE =====================
        private void InsertOtherCharge(SqlConnection con, string type, decimal amount)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO Payments (UserID, OtherChargeID, AmountPaid, PaymentMethod, Status)
                            VALUES (@UID, @OtherChargeID, @Amount, @Method, 'Pending')";

                // Insert to OtherCharges first
                SqlCommand ocCmd = new SqlCommand(@"INSERT INTO OtherCharges (UserID, ChargeType, Amount)
                                            OUTPUT INSERTED.OtherChargeID
                                            VALUES (@UID, @Type, @Amount)", con);
                ocCmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);
                ocCmd.Parameters.AddWithValue("@Type", type);
                ocCmd.Parameters.AddWithValue("@Amount", amount);

                int otherChargeId = (int)ocCmd.ExecuteScalar();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);
                cmd.Parameters.AddWithValue("@OtherChargeID", otherChargeId);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@Method", cmbPaymentMethod.Text);

                cmd.ExecuteNonQuery();
            }
        }


        // ===================== WAIVER REQUEST =====================
        private void btnRequestWaiver_Click(object sender, EventArgs e)
        {
            if (txtWaiverReason.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a reason for waiver.");
                return;
            }

            if (dgvOverdue.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a fine to request a waiver for.");
                return;
            }

            int fineId = Convert.ToInt32(dgvOverdue.SelectedRows[0].Cells["FineID"].Value);

            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                // Check if waiver already pending
                string checkSql = @"SELECT COUNT(*) FROM WaiverRequests
                                    WHERE FineID = @FineID AND UserID = @UserID AND Status = 'Pending'";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    checkCmd.Parameters.AddWithValue("@FineID", fineId);
                    checkCmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("You already submitted a waiver request for this fine.\nPlease wait for librarian approval.");
                        return;
                    }
                }

                // Insert waiver request
                string sql = @"INSERT INTO WaiverRequests (FineID, UserID, Reason, RequestDate, Status)
                               VALUES (@FineID, @UserID, @Reason, GETDATE(), 'Pending')";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@FineID", fineId);
                    cmd.Parameters.AddWithValue("@UserID", LoggedInUser.UserID);
                    cmd.Parameters.AddWithValue("@Reason", txtWaiverReason.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Your waiver request has been submitted.\nPlease wait for librarian approval.");

            LoadWaiverHistory();
            txtWaiverReason.Clear();
        }

        // ===================== CLEAR =====================
        private void ClearFields()
        {
            chkLostBook.Checked = false;
            chkDamagedBook.Checked = false;
            chkLostCard.Checked = false;

            txtPaymentDetails.Clear();
            cmbPaymentMethod.SelectedIndex = -1;
            txtWaiverReason.Clear();

            ComputeGrandTotal();
        }

        private void LoadWaiverHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string sql = @"
                    SELECT RequestID, FineID, Reason, RequestDate, Status
                    FROM WaiverRequests
                    WHERE UserID = @UID
                    ORDER BY RequestDate DESC";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    dgvWaiverHistory.DataSource = dt;
                }
            }
        }

        private void LoadPaymentHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string sql = @"
            SELECT 
    p.PaymentID,
    p.FineID,
    p.OtherChargeID,
    oc.ChargeType,
    p.AmountPaid,
    p.PaymentMethod,
    p.ProofImagePath,
    p.Status,
    p.PaymentDate
FROM Payments p
LEFT JOIN OtherCharges oc ON p.OtherChargeID = oc.OtherChargeID
WHERE p.UserID = @UID
ORDER BY p.PaymentDate DESC
";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UID", LoggedInUser.UserID);

                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    dgvPaymentHistory.AutoGenerateColumns = false;
                    dgvPaymentHistory.DataSource = dt;

                    foreach (DataGridViewRow row in dgvPaymentHistory.Rows)
                    {
                        string method = row.Cells["PaymentMethod"].Value?.ToString();
                        string status = row.Cells["Status"].Value?.ToString();


                        if (method == "Gcash" && status == "Rejected")
                        {
                            row.Cells["btnReupload"].Value = "Re-Upload";
                            row.Cells["btnReupload"].ReadOnly = false;
                        }
                        else
                        {
                            row.Cells["btnReupload"].Value = "";
                            row.Cells["btnReupload"].ReadOnly = true;
                        }



                    }
                }
            }
        }
        private void dgvPaymentHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            // ? GET DATA FROM CLICKED ROW
            string method = dgvPaymentHistory.Rows[e.RowIndex]
                .Cells["PaymentMethod"].Value?.ToString();

            string status = dgvPaymentHistory.Rows[e.RowIndex]
                .Cells["Status"].Value?.ToString();



            // VIEW PROOF
            if (dgvPaymentHistory.Columns[e.ColumnIndex].Name == "btnViewProof")
            {
                string proofPath = dgvPaymentHistory.Rows[e.RowIndex]
                    .Cells["ProofImagePath"].Value?.ToString();

                if (string.IsNullOrEmpty(proofPath))
                {
                    MessageBox.Show("No receipt uploaded for this payment.");
                    return;
                }

                if (!System.IO.File.Exists(proofPath))
                {
                    MessageBox.Show("Receipt file not found.");
                    return;
                }

                System.Diagnostics.Process.Start(proofPath);
            }

            // ?? RE-UPLOAD (GCash + Rejected ONLY)
            if (dgvPaymentHistory.Columns[e.ColumnIndex].Name == "btnReupload")
            {
                // ?? Prevent clicking empty re-upload cells
                if (string.IsNullOrEmpty(
                    dgvPaymentHistory.Rows[e.RowIndex]
                        .Cells["btnReupload"].Value?.ToString()))

                {
                    return;
                }


                if (method != "Gcash" || status != "Rejected")
                {
                    MessageBox.Show("Only rejected GCash payments can be re-uploaded.");
                    return;
                }

                int paymentId = Convert.ToInt32(
                    dgvPaymentHistory.Rows[e.RowIndex].Cells["PaymentID"].Value
                );

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                ofd.Title = "Re-upload GCash Screenshot";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    string sql = @"
                UPDATE Payments
                SET ProofImagePath = @Proof,
                    Status = 'Pending'
                WHERE PaymentID = @PID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Proof", ofd.FileName);
                        cmd.Parameters.AddWithValue("@PID", paymentId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ? PUT THE MESSAGEBOX HERE (RIGHT AFTER UPDATE)
                MessageBox.Show(
                    "Screenshot re-uploaded successfully.\n" +
                    "Your payment is now pending review by the librarian.",
                    "Re-upload Submitted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadPaymentHistory();
            }
        }



        private void txtWaiverReason_TextChanged(object sender, EventArgs e)
        {

        }

            

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
