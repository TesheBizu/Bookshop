using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bookshop
{
    public partial class Register : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M9CM3LD\\SQLEXPRESS;Initial Catalog=BookShop;Integrated Security=True");
        
        public Register()
        {
            InitializeComponent();

           txtUsername.TextChanged += txtUsername_TextChanged;
            btnRegister.Click += btnRegister_Click;

    
        }

        public string EncodePassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder(); //(stringBuilder) Represents a changeable string of characters
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }


        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                lblUsernameCheck.Text = "";
                return;
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE username=@u", con);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);

                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    lblUsernameCheck.ForeColor = System.Drawing.Color.Green;
                    lblUsernameCheck.Text = "✔️ Username available";
                }
                else
                {
                    lblUsernameCheck.ForeColor = System.Drawing.Color.Firebrick;
                    lblUsernameCheck.Text = "✖️ Username already exists";
                }
            }
            catch (Exception ex)
            {
                lblUsernameCheck.Text = "Error Checking Username.";
                
            }
            finally
            {
                con.Close();
            }
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;

            //  Validate empty fields
            if (txtFullName.Text == "" ||
                txtUsername.Text == "" ||
                txtEmail.Text == "" ||
                txtPassword.Text == "" ||
                txtConfirm.Text == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please fill all fields.";
                return;
            }

            //  Username availability check
             if (lblUsernameCheck.Text.Contains("exists"))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Username already exists.";
                return;
            }

            //  Password match
            if (txtPassword.Text != txtConfirm.Text)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Passwords do not match.";
                return;
            }


            try

            {
                con.Open();

                // CHECK EMAIL EXISTS (IMPORTANT)
                SqlCommand checkEmail = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE Email=@Email", con);
                checkEmail.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                int emailExists = (int)checkEmail.ExecuteScalar();

                if (emailExists > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Email already exists.";
                    return;
                }

                //  Hash the password
                string hashedPass = EncodePassword(txtPassword.Text);


                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Users(fullname, username, email, password_hash) " +
                    "VALUES(@f, @u, @e, @p)", con);

                cmd.Parameters.AddWithValue("@f", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@e", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@p", hashedPass);

                cmd.ExecuteNonQuery();

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Account created successfully!";
                lblMessage.Visible = true;

    // Waiting for 2 sec and go to login section
                await Task.Delay(2000);

                LoginForm login = new LoginForm();
                login.Show();

                this.Hide();

                // Optional: clear fields
                txtFullName.Clear();
                txtUsername.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                txtConfirm.Clear();

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = false;
            }
            finally
            {
                con.Close();
            }
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();

            this.Hide(); //Hides the control from user.
        }
    } 
   
}
