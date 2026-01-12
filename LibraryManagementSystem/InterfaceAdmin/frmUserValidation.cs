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
    public partial class frmUserValidation : Form
    {
        public frmUserValidation()
        {
            InitializeComponent();
        }

       

        private void frmUserValidation_Load(object sender, EventArgs e)
        {
            StyleUserDGV(dgvAllUsers);
            LoadAllUsers();

            StyleUserDGV(dgvPendingUsers);
            LoadPendingUsers();

        }

        private void dgvAllUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void LoadAllUsers()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"
            SELECT 
                UserID, 
                UserName, 
                EmailAddress, 
                MemberType, 
                RegistrationDate, 
                ExpirationDate,
                Status -- <-- use actual status from DB
            FROM Users
            ORDER BY UserName";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAllUsers.DataSource = dt;

                // Hide UserID
                if (dgvAllUsers.Columns.Contains("UserID"))
                    dgvAllUsers.Columns["UserID"].Visible = false;

                // Optional: Format Dates
                if (dgvAllUsers.Columns.Contains("RegistrationDate"))
                    dgvAllUsers.Columns["RegistrationDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

                if (dgvAllUsers.Columns.Contains("ExpirationDate"))
                    dgvAllUsers.Columns["ExpirationDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
        }







        private void LoadPendingUsers()
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                string sql = @"SELECT UserID, UserName, EmailAddress, MemberType, RegistrationDate, 'Pending' AS Status
                       FROM Users
                       WHERE Status = 'Pending'
                       ORDER BY RegistrationDate";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPendingUsers.DataSource = dt;

                // Hide UserID
                if (dgvPendingUsers.Columns.Contains("UserID"))
                    dgvPendingUsers.Columns["UserID"].Visible = false;

                // Add Approve/Reject buttons
                if (!dgvPendingUsers.Columns.Contains("Approve"))
                {
                    DataGridViewButtonColumn approveCol = new DataGridViewButtonColumn();
                    approveCol.Name = "Approve";
                    approveCol.Text = "Approve";
                    approveCol.UseColumnTextForButtonValue = true;
                    dgvPendingUsers.Columns.Add(approveCol);
                }
                if (!dgvPendingUsers.Columns.Contains("Reject"))
                {
                    DataGridViewButtonColumn rejectCol = new DataGridViewButtonColumn();
                    rejectCol.Name = "Reject";
                    rejectCol.Text = "Reject";
                    rejectCol.UseColumnTextForButtonValue = true;
                    dgvPendingUsers.Columns.Add(rejectCol);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No pending users!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }







        ///Button///////////////
        ///
        
        

        


       private void dgvPendingUsers_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvPendingUsers.Rows[e.RowIndex].IsNewRow) return;

            object val = dgvPendingUsers.Rows[e.RowIndex].Cells["UserID"].Value;
            if (val == DBNull.Value) return;

            int userId = Convert.ToInt32(val);

            if (dgvPendingUsers.Columns[e.ColumnIndex].Name == "Approve")
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Status='Active' WHERE UserID=@id", con);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("User approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dgvPendingUsers.Columns[e.ColumnIndex].Name == "Reject")
            {
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Status='Rejected' WHERE UserID=@id", con);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("User rejected!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadPendingUsers(); // reload
        }





        private void StyleUserDGV(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowTemplate.Height = 35;
            dgv.BackgroundColor = Color.FromArgb(46, 51, 73);
            dgv.GridColor = Color.FromArgb(60, 65, 90);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 42, 64);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 76, 110);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 85);
        }

       
    }
}
