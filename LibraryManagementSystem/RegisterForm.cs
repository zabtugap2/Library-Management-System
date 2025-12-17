using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace LibraryManagementSystem
{
    public partial class frmRegister : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["LMSdb"].ConnectionString;

        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
               string.IsNullOrWhiteSpace(txtRegisterEmail.Text) ||
               string.IsNullOrWhiteSpace(txtRegisterPassword.Text) ||

               listRegisterType.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    INSERT INTO Members (Username,Email,MemberType, PasswordHash)
                    VALUES (@Username, @RegisterEmail, @MemberType, @PasswordHash)
                ";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtRegisterEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@MemberType", listRegisterType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PasswordHash", txtRegisterPassword.Text); // hash later

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "Registration successful!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        FrmLogin login = new FrmLogin();
                        login.Show();
                        this.Hide();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(
                            "Username already exists or database error.\n\n" + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Hide();
        }


    }
}
