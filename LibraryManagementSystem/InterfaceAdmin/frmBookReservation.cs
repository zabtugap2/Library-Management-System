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
    public partial class frmBookReservation : Form
    {
        public frmBookReservation()
        {
            InitializeComponent();
            
        }

        private void frmBookReservation_Load(object sender, EventArgs e)
        {
            StyleReservationDGV();
            LoadReservations();
            dgvReservations.CellContentClick += dgvReservations_CellContentClick;
        }







        private void LoadReservations()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"
        SELECT r.ReservationID, 
               u.UserName AS StaffName, 
               b.Title AS BookTitle, 
               r.ReservationDate, 
               r.Status
        FROM BookReservation r
        JOIN Users u ON r.UserID = u.UserID
        JOIN Books b ON r.BookID = b.BookID
        ORDER BY r.ReservationDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvReservations.DataSource = dt;

                // Hide ReservationID
                if (dgvReservations.Columns.Contains("ReservationID"))
                    dgvReservations.Columns["ReservationID"].Visible = false;

                // Format Date and Time
                if (dgvReservations.Columns.Contains("ReservationDate"))
                {
                    dgvReservations.Columns["ReservationDate"].HeaderText = "Date";
                    dgvReservations.Columns["ReservationDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

                    // Add Time column separately if not exist
                    if (!dgvReservations.Columns.Contains("Time"))
                    {
                        DataGridViewTextBoxColumn timeCol = new DataGridViewTextBoxColumn();
                        timeCol.Name = "Time";
                        timeCol.HeaderText = "Time";
                        dgvReservations.Columns.Add(timeCol);
                    }

                    foreach (DataGridViewRow row in dgvReservations.Rows)
                    {
                        if (!row.IsNewRow && row.Cells["ReservationDate"].Value != DBNull.Value)
                        {
                            row.Cells["Time"].Value = Convert.ToDateTime(row.Cells["ReservationDate"].Value)
                                                       .ToString("hh:mm tt");
                        }
                    }
                }

                // --- BUTTONS HANDLING ---
                // Remove Approve/Reject columns first
                if (dgvReservations.Columns.Contains("Approve"))
                    dgvReservations.Columns.Remove("Approve");
                if (dgvReservations.Columns.Contains("Reject"))
                    dgvReservations.Columns.Remove("Reject");

                // Only add buttons if there are rows
                if (dt.Rows.Count > 0)
                {
                    DataGridViewButtonColumn approveCol = new DataGridViewButtonColumn();
                    approveCol.Name = "Approve";
                    approveCol.Text = "Approve";
                    approveCol.UseColumnTextForButtonValue = true;
                    dgvReservations.Columns.Add(approveCol);

                    DataGridViewButtonColumn rejectCol = new DataGridViewButtonColumn();
                    rejectCol.Name = "Reject";
                    rejectCol.Text = "Reject";
                    rejectCol.UseColumnTextForButtonValue = true;
                    dgvReservations.Columns.Add(rejectCol);
                }
                else
                {
                    MessageBox.Show("No reservations yet!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        private void dgvReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header or empty clicks
            if (e.RowIndex < 0 || dgvReservations.Rows[e.RowIndex].IsNewRow) return;

            // Make sure the ReservationID cell is not DBNull
            object val = dgvReservations.Rows[e.RowIndex].Cells["ReservationID"].Value;
            if (val == DBNull.Value) return;

            int reservationId = Convert.ToInt32(val);

            if (dgvReservations.Columns[e.ColumnIndex].Name == "Approve")
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE BookReservation
                SET Status = 'Approved'
                WHERE ReservationID = @id;
            ", con);
                    cmd.Parameters.AddWithValue("@id", reservationId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Reservation approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dgvReservations.Columns[e.ColumnIndex].Name == "Reject")
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE BookReservation
                SET Status = 'Rejected'
                WHERE ReservationID = @id;
            ", con);
                    cmd.Parameters.AddWithValue("@id", reservationId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Reservation rejected!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadReservations(); // reload grid
        }




















































































        private void StyleReservationDGV()
        {
            dgvReservations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservations.RowTemplate.Height = 35;
            dgvReservations.BackgroundColor = Color.FromArgb(46, 51, 73);
            dgvReservations.GridColor = Color.FromArgb(60, 65, 90);
            dgvReservations.BorderStyle = BorderStyle.None;
            dgvReservations.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReservations.RowHeadersVisible = false;

            dgvReservations.EnableHeadersVisualStyles = false;
            dgvReservations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 42, 64);
            dgvReservations.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReservations.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvReservations.ColumnHeadersHeight = 40;

            dgvReservations.DefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvReservations.DefaultCellStyle.ForeColor = Color.White;
            dgvReservations.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 76, 110);
            dgvReservations.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReservations.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            dgvReservations.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 85);
        }

        private void dgvReservations_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
