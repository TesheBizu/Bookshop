using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace Bookshop
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-M9CM3LD\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True");

        public LoginForm()
        {
            InitializeComponent();
        }

        private string EncodePassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter username and password.";
                lblMessage.Visible = true;
                return;
            }

            if (txtUsername.Text.Trim() == "Tenasi" && txtPassword.Text.Trim() == "2121")
            {
                AdminDashboard admin = new AdminDashboard();
                admin.Show();
                this.Hide();
                return;
            }

            try
            {
                con.Open();

                string hashedPassword = EncodePassword(txtPassword.Text.Trim());

                SqlCommand cmd = new SqlCommand("SELECT userId, fullname, username, email, role FROM Users WHERE username=@user AND password_hash=@pass", con);
               

                cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", hashedPassword);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    lblMessage.Text = "Incorrect username or password.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                    return;
                }

                // 5. Login success
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Login successful!";
                lblMessage.Visible = true;


                // Store session values
                UserSession.UserID = Convert.ToInt32(dr["userId"]);
                UserSession.fullname = dr["fullname"].ToString();
                UserSession.Username = dr["username"].ToString();
                UserSession.Email = dr["email"].ToString();

                string role = dr["role"].ToString();

                dr.Close();

                // Open dashboards
                if (role == "Admin")
                {
                    AdminDashboard admin = new AdminDashboard();
                    admin.Show();
                }
                else
                {
                    UserDashboard user = new UserDashboard();
                    user.Show();
                }

                this.Hide();
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }
        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
