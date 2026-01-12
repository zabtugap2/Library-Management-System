using LibraryManagementSystem.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LibraryManagementSystem.UserInterface_Forms
{
    public partial class frmHistory : Form
    {
        private int currentUserId;

        public frmHistory(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {


            StyleDataGridView(grdDashboard);
            StyleDataGridView(dataGridView1);
            StyleDataGridView(dataGridView2);
            LoadBorrowHistory();
            LoadPenaltyHistory();
            LoadAccountHistory();
        }

        // ======================================================
        // 1️⃣ BORROW & RETURN HISTORY
        // ======================================================
        private void LoadBorrowHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string query = @"
                    SELECT bb.BookID, bb.BorrowDate, br.ReturnDate,
                           b.Title, b.Author, b.Publisher, b.Edition, b.Category,
                           b.Language, b.PublicationYear
                    FROM BookBorrow bb
                    INNER JOIN Books b ON bb.BookID = b.BookID
                    LEFT JOIN BookReturn br ON bb.BorrowID = br.BorrowID
                    WHERE bb.UserID = @uid
                    ORDER BY bb.BorrowDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@uid", currentUserId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                grdDashboard.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    grdDashboard.Rows.Add(
                        row["BookID"],
                        Convert.ToDateTime(row["BorrowDate"]).ToShortDateString(),
                        row["ReturnDate"] == DBNull.Value ? "Not Returned"
                                                          : Convert.ToDateTime(row["ReturnDate"]).ToShortDateString(),
                        "—", // Librarian placeholder
                        row["Title"],
                        row["Author"],
                        row["Publisher"],
                        row["Edition"],
                        row["Category"],
                        row["Language"],
                        row["PublicationYear"]
                    );
                }
            }
        }

        // ======================================================
        // 2️⃣ PENALTY / FINES HISTORY
        // ======================================================
        private void LoadPenaltyHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string query = @"
                    SELECT bb.BookID, b.Title, b.Author, b.Publisher, b.Edition,
                           b.Category, b.Language, b.PublicationYear,
                           f.DaysLate, f.FineAmount
                    FROM Fines f
                    INNER JOIN BookBorrow bb ON f.BorrowID = bb.BorrowID
                    INNER JOIN Books b ON bb.BookID = b.BookID
                    WHERE f.UserID = @uid
                    ORDER BY f.CreatedAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@uid", currentUserId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["BookID"],
                        row["Title"],
                        row["Author"],
                        row["DaysLate"],
                        row["FineAmount"],
                        row["Publisher"],
                        row["Edition"],
                        row["Category"],
                        row["Language"],
                        row["PublicationYear"]
                    );
                }
            }
        }

        // ======================================================
        // 3️⃣ ACCOUNT ACTIVITY HISTORY
        // ======================================================
        private void LoadAccountHistory()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                string query = @"
                    SELECT RegistrationDate, MemberType, ExpirationDate
                    FROM Users
                    WHERE UserID = @uid";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@uid", currentUserId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dataGridView2.Rows.Add(
                        Convert.ToDateTime(row["RegistrationDate"]).ToShortDateString(),
                        row["MemberType"],
                        Convert.ToDateTime(row["ExpirationDate"]).ToShortDateString(),
                        Convert.ToDateTime(row["ExpirationDate"]) >= DateTime.Now ? "Active" : "Expired"
                    );
                }
            }
        }

        // ================================
        // UNUSED EVENTS (SAFE)
        // ================================
        private void grdDashboard_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }











        private void StyleDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;

            // Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
            dgv.RowTemplate.Height = 35;

            // Alternate row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 85);

            // Grid and border
            dgv.BackgroundColor = Color.FromArgb(46, 51, 73);
            dgv.GridColor = Color.FromArgb(60, 65, 90);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            // Optional: center any column named "Action"
            if (dgv.Columns.Contains("Action"))
                dgv.Columns["Action"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }






    }
}
