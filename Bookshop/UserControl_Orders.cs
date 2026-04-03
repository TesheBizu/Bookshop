using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bookshop
{
    public partial class UserControl_AdminOrders : UserControl
    {
        // WINAPI PLACEHOLDER (NO HELPER)
        // =============================
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            string lParam);


        public UserControl_AdminOrders()
        {
            InitializeComponent();
        }

        private void UserControl_AdminOrders_Load(object sender, EventArgs e)
        {
            // Turn off auto-generated columns
            dgvOrders.AutoGenerateColumns = false;

            // Set placeholder text
            SendMessage(
                txtSearch.Handle,
                EM_SETCUEBANNER,
                0,
                "Search by Order ID or Customer");

            // Optional defaults
            cmbStatus.SelectedIndex = 0;
            dtFrom.Value = DateTime.Today.AddMonths(-1);
            dtTo.Value = DateTime.Today;

            LoadOrders();
        }

        // LOAD ORDERS WITH FILTERS
        // ============================
        private void LoadOrders()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query =
                 @"SELECT 
                    o.OrderId,
                    u.FullName AS CustomerName,
                    o.TotalAmount,
                    o.Status,
                    o.orderDate
                  FROM Orders o
                  INNER JOIN Users u ON o.UserId = u.UserId
                  WHERE
                    (@search IS NULL OR 
                        CAST(o.OrderId AS NVARCHAR) LIKE @search OR
                        u.FullName LIKE @search)
                  AND
                    (@status IS NULL OR o.Status = @status)
                  AND
                    o.orderDate BETWEEN @from AND @to
                  ORDER BY o.orderDate DESC";

                SqlCommand cmd = new SqlCommand(query, con);

                // SEARCH
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    cmd.Parameters.AddWithValue("@search", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                // STATUS
                if (cmbStatus.SelectedIndex == 0)
                    cmd.Parameters.AddWithValue("@status", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@status", cmbStatus.Text);

                // DATE
                cmd.Parameters.AddWithValue("@from", dtFrom.Value.Date);
                cmd.Parameters.AddWithValue("@to", dtTo.Value.Date.AddDays(1));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvOrders.DataSource = null;
                dgvOrders.DataSource = dt;
            }
        }



        // SUMMARY
        // ============================
        private void LoadSummary(DataTable dt)
        {
            lblTotalOrders.Text = "Total Orders: " + dt.Rows.Count;

            decimal revenue = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Status"].ToString() == "Completed")
                    revenue += Convert.ToDecimal(row["TotalAmount"]);
            }

            lblRevenue.Text = "Revenue: " + revenue.ToString("0.00");
        }


        //EVENTS
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }



        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int orderId = Convert.ToInt32(
                dgvOrders.Rows[e.RowIndex].Cells["colOrderId"].Value);

            // VIEW DETAILS
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colView")
            {
                int _orderId = Convert.ToInt32(
            dgvOrders.Rows[e.RowIndex].Cells["colOrderId"].Value);

              //  MessageBox.Show("Opening Order Details for OrderId = " + orderId);

                OrderDetailsForm detailsForm = new OrderDetailsForm(orderId);

                // IMPORTANT: use ShowDialog from UC
                detailsForm.ShowDialog();
            }

            // CANCEL ORDER
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colCancel")
            {
                DialogResult result = MessageBox.Show(
                    "Cancel this order?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    CancelOrder(orderId);
                    LoadOrders();
                }
            }
        }

        // CANCEL ORDER
        // ============================
        private void CancelOrder(int orderId)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Orders SET Status='Cancelled' WHERE OrderId=@id", con);
                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.ExecuteNonQuery();
            }
        }
    }

}