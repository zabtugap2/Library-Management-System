using LibraryManagementSystem.InterfaceAdmin;
using LibraryManagementSystem.Model;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AdminInterface : Form
    {
        
        // OPTIONAL: keep default constructor if designer complains
        public AdminInterface()
        {
            InitializeComponent();
        }

        // ✅ load username, role, and profile image
        private void AdminInterface_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
           
            LoadForm(new frmAdminDashboard());
        }

        private void LoadUserInfo()
        {
            lblUserName.Text = LoggedInUser.UserName;
            lblRole.Text = LoggedInUser.Role;

            LoadProfileImage(LoggedInUser.ProfileImagePath);
        }


        private void LoadProfileImage(string imagePath)
        {
            try
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = null;

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pictureBox1.Image = Properties.Resources._487391276_8614531771983355_5210645756945235689_n; 
                }
            }
            catch
            {
                pictureBox1.Image = Properties.Resources._487391276_8614531771983355_5210645756945235689_n;
            }
        }


        private void LoadForm(Form childForm)
        {
            PnlFormLoader.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            PnlFormLoader.Controls.Add(childForm);
            PnlFormLoader.Tag = childForm;

            childForm.Show();
        }







        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            frmAdminDashboard dashboard = new frmAdminDashboard();
            dashboard.FormClosed += (s, args) => this.Close(); // close current form when dashboard closes
            dashboard.Show();
            this.Hide();
        }


        private void btnBooks_Click(object sender, EventArgs e) 
        
        {
            LoadForm(new txtBookTitle());
        }
        private void btnFines_Click(object sender, EventArgs e)
        {
            LoadForm(new frmAdminFines());
        }


        private void btnHistory_Click(object sender, EventArgs e) 
        {
            LoadForm(new frmAdminHistory());
        }

        private void btnEditProfile_Click(object sender, EventArgs e) 
        {
            LoadForm(new frmUserValidation());
        }
        private void btnRervation_Click(object sender, EventArgs e) 
        {
            LoadForm(new frmBookReservation());
        }

        private void PnlFormLoader_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void PanelUser_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
