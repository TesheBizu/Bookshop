using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop
{
    public partial class UC_Customers : UserControl
    {
     
        private DataTable customersTable;

        public UC_Customers()
        {
            InitializeComponent();

            dgvCustomers.AutoGenerateColumns = false;
            SetupSearchPlaceholder();
           
        }

        private void UC_Customers_Load(object sender, EventArgs e)
        {
            LoadFilters();
            LoadCustomers();
        }

        // LOAD CUSTOMERS
       
        private void LoadCustomers()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        UserId,
                        FullName,
                        Username,
                        Email,
                        Role,
                        Status,
                        Created_at
                    FROM Users
                    WHERE Role = 'User'
                    ORDER BY Created_at DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                customersTable = new DataTable();
                da.Fill(customersTable);

                dgvCustomers.DataSource = customersTable;

                UpdateSummary();
            }
        }

        // FILTER SETUP

        private void LoadFilters()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new object[] { "All", "Customer", "Admin" });
            cmbRole.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "All", "Active", "Disabled" });
            cmbStatus.SelectedIndex = 0;

            cmbRole.SelectedIndexChanged += FilterChanged;
            cmbStatus.SelectedIndexChanged += FilterChanged;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // SEARCH + FILTER
       
        private void ApplyFilters()
        {
            if (customersTable == null) return;

            string filter = "";

            if (txtSearchCustomer.Text != "Search customers..." &&
                !string.IsNullOrWhiteSpace(txtSearchCustomer.Text))
            {
                string text = txtSearchCustomer.Text.Replace("'", "''");

                filter = $@"
                    (FullName LIKE '%{text}%'
                     OR Username LIKE '%{text}%'
                     OR Email LIKE '%{text}%')";
            }

            if (cmbRole.SelectedItem.ToString() != "All")
            {
                if (filter != "") filter += " AND ";
                filter += $"Role = '{cmbRole.SelectedItem}'";
            }

            if (cmbStatus.SelectedItem.ToString() != "All")
            {
                if (filter != "") filter += " AND ";
                filter += $"Status = '{cmbStatus.SelectedItem}'";
            }

            customersTable.DefaultView.RowFilter = filter;
            UpdateSummary();
        }


        // SEARCH PLACEHOLDER

        private void SetupSearchPlaceholder()
        {
            txtSearchCustomer.Text = "Search customers...";
            txtSearchCustomer.ForeColor = Color.Gray;

            txtSearchCustomer.Enter += (s, e) =>
            {
                if (txtSearchCustomer.Text == "Search customers...")
                {
                    txtSearchCustomer.Text = "";
                    txtSearchCustomer.ForeColor = Color.Black;
                }
            };

            txtSearchCustomer.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearchCustomer.Text))
                {
                    txtSearchCustomer.Text = "Search customers...";
                    txtSearchCustomer.ForeColor = Color.Gray;
                }
            };

            txtSearchCustomer.TextChanged += (s, e) => ApplyFilters();
        }

        // SUMMARY
      
        private void UpdateSummary()
        {
            lblTotalCustomers.Text =
                $"Total Customers: {customersTable.DefaultView.Count}";

            int active = 0;
            foreach (DataRowView row in customersTable.DefaultView)
            {
                if (row["Status"].ToString() == "Active")
                    active++;
            }

            lblActiveCustomers.Text =
                $"Active Customers: {active}";
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int userId = Convert.ToInt32(
                dgvCustomers.Rows[e.RowIndex].Cells["colUserId"].Value);

            string colName = dgvCustomers.Columns[e.ColumnIndex].Name;

            if (colName == "colView")
            {
                MessageBox.Show($"Customer ID: {userId}",
                    "View Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (colName == "colDisable")
            {
                ToggleStatus(userId);
                LoadCustomers();
            }
        }

        // ENABLE / DISABLE
    
        private void ToggleStatus(int userId)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();

                string query = @"
                    UPDATE Users
                    SET Status = CASE
                        WHEN Status = 'Active' THEN 'Disabled'
                        ELSE 'Active'
                    END
                    WHERE UserId = @UserId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
