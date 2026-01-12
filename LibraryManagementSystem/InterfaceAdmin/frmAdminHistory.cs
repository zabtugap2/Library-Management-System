using LibraryManagementSystem.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem.InterfaceAdmin
{
    public partial class frmAdminHistory : Form
    {
        public frmAdminHistory()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void frmAdminHistory_Load(object sender, EventArgs e)
        {

            ApplyDgvStyle(dgvBookHistory);
            ApplyDgvStyle(dgvBorrowHistory);
            ApplyDgvStyle(dgvPaymentHistory);

            LoadBookHistory();
            LoadBorrowHistory();
            LoadPaymentsHistory();

        }









        private void LoadBookHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"
        SELECT 
            b.Title,
            u.UserName AS AdminName,
            l.Action,
            l.ActionDate
        FROM BookActivityLogs l
        JOIN Books b ON l.BookID = b.BookID
        JOIN Users u ON l.AdminID = u.UserID
        ORDER BY l.ActionDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvBookHistory.DataSource = dt;


            }
        }

        private void LoadBorrowHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"
SELECT
    b.Title,
    u.UserName AS Borrower,
    bb.BorrowDate,
    ISNULL(CONVERT(VARCHAR(10), br.ReturnDate, 101), 'Not Returned') AS ReturnDate,
    bb.Status
FROM BookBorrow bb
JOIN Books b ON bb.BookID = b.BookID
JOIN Users u ON bb.UserID = u.UserID
LEFT JOIN BookReturn br ON bb.BorrowID = br.BorrowID
ORDER BY bb.BorrowDate DESC";


                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Replace NULLs in ReturnDate column
                foreach (DataRow row in dt.Rows)
                {
                    if (row["ReturnDate"] == DBNull.Value)
                        row["ReturnDate"] = "Not Returned";
                }

                dgvBorrowHistory.DataSource = dt;


                dgvBorrowHistory.DataSource = dt;
            }
        }



        private void LoadPaymentsHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"
SELECT
    p.PaymentID,
    u.UserName AS Payer,
    COALESCE(f.FineAmount, o.Amount) AS AmountDue,
    p.AmountPaid,
    COALESCE(f.FineID, o.OtherChargeID) AS LinkedID,
    COALESCE(f.FineID, 0) AS FinePaymentFlag,
    COALESCE(o.ChargeType, '') AS ChargeType,
    p.PaymentMethod,
    p.Status,
    p.PaymentDate
FROM Payments p
JOIN Users u ON p.UserID = u.UserID
LEFT JOIN Fines f ON p.FineID = f.FineID
LEFT JOIN OtherCharges o ON p.OtherChargeID = o.OtherChargeID
ORDER BY p.PaymentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPaymentHistory.DataSource = dt;

                dgvPaymentHistory.Columns["PaymentID"].Visible = false;
                dgvPaymentHistory.Columns["LinkedID"].Visible = false;


                


            }
        }






        private void dgvBookHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBorrowHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        private void ApplyDgvStyle(DataGridView dgv)
        {
            // Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Row height
            dgv.RowTemplate.Height = 35;

            // Background and grid
            dgv.BackgroundColor = Color.FromArgb(46, 51, 73);
            dgv.GridColor = Color.FromArgb(60, 65, 90);

            // Remove borders
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 42, 64);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            // Row style
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 76, 110);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // Alternate row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 85);

            // Optional: center "Action" column if it exists
            if (dgv.Columns.Contains("Action"))
            {
                dgv.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

    }
}
