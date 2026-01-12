using LibraryManagementSystem.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace LibraryManagementSystem.UserInterface_Forms
{
    public partial class frmDashboard : Form
    {

        private int currentUserId;

        public frmDashboard(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
        }


        private void frmDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardCounts();
            LoadBooks();
            LoadSummaryChart();
            LoadFinesSummary();
        }

        // ================================
        // DASHBOARD COUNTS
        // ================================
        private void LoadDashboardCounts()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                // Borrowed
                SqlCommand cmdBorrowed = new SqlCommand(
                    "SELECT COUNT(*) FROM BookBorrow WHERE UserID=@uid AND Status='Borrowed'", con);
                cmdBorrowed.Parameters.AddWithValue("@uid", currentUserId);
                lblQntyBorrowed.Text = cmdBorrowed.ExecuteScalar().ToString();

                // Overdue
                SqlCommand cmdOverdue = new SqlCommand(
                    @"SELECT COUNT(*) FROM BookBorrow 
                      WHERE UserID=@uid AND DueDate < GETDATE() AND Status='Borrowed'", con);
                cmdOverdue.Parameters.AddWithValue("@uid", currentUserId);
                lblQntyOverdue.Text = cmdOverdue.ExecuteScalar().ToString();

                // Returned
                SqlCommand cmdReturned = new SqlCommand(
                    @"SELECT COUNT(*) FROM BookReturn r
                      JOIN BookBorrow b ON r.BorrowID = b.BorrowID
                      WHERE b.UserID=@uid", con);
                cmdReturned.Parameters.AddWithValue("@uid", currentUserId);
                label7.Text = cmdReturned.ExecuteScalar().ToString();

            }
        }
        private void LoadSummaryChart()
        {
            chartDashboard.Series.Clear();
            chartDashboard.ChartAreas.Clear();
            chartDashboard.Legends.Clear();

            ChartArea area = new ChartArea();
            area.AxisY.Minimum = 0;
            area.AxisY.Interval = 1;
            area.AxisY.LabelStyle.Format = "0";
            chartDashboard.ChartAreas.Add(area);

            Legend legend = new Legend();
            legend.Docking = Docking.Right;
            chartDashboard.Legends.Add(legend);

            int borrowed = 0, returned = 0, overdue = 0, fines = 0;

            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                borrowed = (int)new SqlCommand(
                    "SELECT COUNT(*) FROM BookBorrow WHERE UserID=@uid", con)
                { Parameters = { new SqlParameter("@uid", currentUserId) } }.ExecuteScalar();

                overdue = (int)new SqlCommand(
                    @"SELECT COUNT(*) FROM BookBorrow 
              WHERE UserID=@uid AND DueDate < GETDATE() AND Status='Borrowed'", con)
                { Parameters = { new SqlParameter("@uid", currentUserId) } }.ExecuteScalar();

                returned = (int)new SqlCommand(
                    @"SELECT COUNT(*) FROM BookReturn r
              JOIN BookBorrow b ON r.BorrowID=b.BorrowID
              WHERE b.UserID=@uid", con)
                { Parameters = { new SqlParameter("@uid", currentUserId) } }.ExecuteScalar();

                fines = Convert.ToInt32(new SqlCommand(
                    "SELECT ISNULL(SUM(FineAmount),0) FROM Fines WHERE UserID=@uid", con)
                { Parameters = { new SqlParameter("@uid", currentUserId) } }.ExecuteScalar());
            }

            // ---- CREATE SERIES (LEGEND ITEMS) ----
            Series sBorrowed = new Series("Borrowed");
            Series sReturned = new Series("Returned");
            Series sOverdue = new Series("Overdue");
            Series sFines = new Series("Fines");

            foreach (var s in new[] { sBorrowed, sReturned, sOverdue, sFines })
            {
                s.ChartType = SeriesChartType.Column;
                s.IsValueShownAsLabel = true;
                s.LabelFormat = "0";
            }

            sBorrowed.Points.AddY(borrowed);
            sReturned.Points.AddY(returned);
            sOverdue.Points.AddY(overdue);
            sFines.Points.AddY(fines);

            sBorrowed.Color = Color.SteelBlue;
            sReturned.Color = Color.SeaGreen;
            sOverdue.Color = Color.OrangeRed;
            sFines.Color = Color.Goldenrod;

            chartDashboard.Series.Add(sBorrowed);
            chartDashboard.Series.Add(sReturned);
            chartDashboard.Series.Add(sOverdue);
            chartDashboard.Series.Add(sFines);

            // ---- AUTO MAX (OPTIONAL BUT RECOMMENDED) ----
            int maxValue = Math.Max(Math.Max(borrowed, returned), Math.Max(overdue, fines));
            area.AxisY.Maximum = maxValue + 2;
        }



        private void LoadFinesSummary()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                SqlCommand totalCmd = new SqlCommand(
                    "SELECT ISNULL(SUM(FineAmount),0) FROM Fines WHERE UserID=@uid", con);
                totalCmd.Parameters.AddWithValue("@uid", currentUserId);
                lblTotalFines.Text = "₱ " + totalCmd.ExecuteScalar().ToString();

                SqlCommand paidCmd = new SqlCommand(
                    @"SELECT ISNULL(SUM(AmountPaid),0)
              FROM Payments WHERE UserID=@uid", con);
                paidCmd.Parameters.AddWithValue("@uid", currentUserId);
                lblPaidFines.Text = "₱ " + paidCmd.ExecuteScalar().ToString();

                SqlCommand unpaidCmd = new SqlCommand(
                    @"SELECT ISNULL(SUM(FineAmount),0)
              FROM Fines WHERE UserID=@uid AND IsCleared=0", con);
                unpaidCmd.Parameters.AddWithValue("@uid", currentUserId);
                lblUnpaidFines.Text = "₱ " + unpaidCmd.ExecuteScalar().ToString();
            }
        }


        // ================================
        // BOOKS TABLE
        // ================================
        private void LoadBooks()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT 
                        Title,
                        Author,
                        Publisher,
                        Language,
                        PublicationYear
                      FROM Books", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                grdDashboard.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    grdDashboard.Rows.Add(
                        row["Title"],
                        row["Author"],
                        row["Publisher"],
                        "", // Category placeholder
                        row["Language"],
                        row["PublicationYear"]
                    );
                }
            }
        }

        // ================================
        // UNUSED EVENTS (SAFE)
        // ================================
        private void label5_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void grdDashboard_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void labelTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblQntyReturned_Click(object sender, EventArgs e)
        {

        }

        private void lblQntyBorrowed_Click(object sender, EventArgs e)
        {

        }
    }
}
