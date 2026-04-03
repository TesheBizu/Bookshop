using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bookshop.Helpers;

namespace Bookshop
{
    public partial class AdminDashboard : Form
    {
        // Keeps track of selected sidebar button
        private Button currentButton;

        public AdminDashboard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Default: Dashboard
            UIhelper.HighlightButton(btnDashboard, ref currentButton);
            UIhelper.LoadUC(panelMain, new UC_AdminDashboard());
        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnDashboard, ref currentButton);
            UIhelper.LoadUC(panelMain, new UC_AdminDashboard());
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnBooks, ref currentButton);
            UIhelper.LoadUC(panelMain, new UserControl_AdminBooks());
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnOrders, ref currentButton);
            UIhelper.LoadUC(panelMain, new UserControl_AdminOrders());
        }

        private void btnCusomers_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnCustomers, ref currentButton);
            UIhelper.LoadUC(panelMain, new UC_Customers());
        }

        private void btnAdminAndRoles_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnAdminAndRoles, ref currentButton);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnInventory, ref currentButton);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnReports, ref currentButton);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            UIhelper.HighlightButton(btnSettings, ref currentButton);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Close dashboard → open Login Form
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            //    Application.Exit();
            }
        }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
