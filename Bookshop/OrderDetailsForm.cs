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

namespace Bookshop
{
    public partial class OrderDetailsForm : Form
    {
        private readonly int _orderId;

        public OrderDetailsForm(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
        }


        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            dgvOrderItems.AutoGenerateColumns = false;
            LoadOrderHeader();
            LoadOrderItems();
        }

        // LOAD ORDER HEADER (NO CALCULATION)
      
        private void LoadOrderHeader()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    SELECT
                        o.OrderId,
                        u.FullName,
                        o.OrderDate,
                        o.Status,
                        o.PaymentMethod,
                        o.Subtotal,
                        o.Shipping,
                        o.TotalAmount
                    FROM Orders o
                    INNER JOIN Users u ON o.UserId = u.UserId
                    WHERE o.OrderId = @orderId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@orderId", _orderId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblOrderId.Text = "Order #: " + dr["OrderId"];
                    lblCustomer.Text = "Customer: " + dr["FullName"];
                    lblDate.Text = "Date: " + Convert.ToDateTime(dr["OrderDate"]).ToShortDateString();
                    lblStatus.Text = "Status: " + dr["Status"];
                    lblPayment.Text = "Payment: " + dr["PaymentMethod"];

                    lblSubtotal.Text = "Subtotal: " + Convert.ToDecimal(dr["Subtotal"]).ToString("0.00");
                    lblShipping.Text = "Shipping: " + Convert.ToDecimal(dr["Shipping"]).ToString("0.00");
                    lblTotal.Text = "Total: " + Convert.ToDecimal(dr["TotalAmount"]).ToString("0.00");
                }
            }
        }


        // LOAD ORDER ITEMS (CALCULATE TOTAL PRICE)
       
        private void LoadOrderItems()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string query = @"
                    SELECT
                        b.Title,
                        oi.UnitPrice,
                        oi.Quantity,
                        (oi.UnitPrice * oi.Quantity) AS TotalPrice
                    FROM OrderItems oi
                    INNER JOIN Books b ON oi.BookId = b.BookId
                    WHERE oi.OrderId = @orderId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@orderId", _orderId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvOrderItems.DataSource = dt;
            }
        }

        private void dgvOrderItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
