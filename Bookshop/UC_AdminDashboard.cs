
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bookshop
{
    public partial class UC_AdminDashboard : UserControl
    {
        public UC_AdminDashboard()
        {

            InitializeComponent();
            
        }


        private void UC_AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadSummaryCards();
            LoadSalesChart();
            LoadOrderPieChart();
            LoadRecentOrders();
        }

        // SUMMARY CARDS
        private void LoadSummaryCards()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();

                lblTotalOrders.Text =
                    new SqlCommand("SELECT COUNT(*) FROM Orders", con)
                    .ExecuteScalar().ToString();

                lblTotalCustomers.Text =
                    new SqlCommand(
                        "SELECT COUNT(*) FROM Users WHERE Role='Customer'", con)
                    .ExecuteScalar().ToString();

                lblTotalBooks.Text =
                    new SqlCommand("SELECT COUNT(*) FROM Books", con)
                    .ExecuteScalar().ToString();

                lblTotalRevenue.Text =
                    new SqlCommand(
                        "SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE Status='Completed'",
                        con)
                    .ExecuteScalar().ToString();
            }
        }

        // SALES REVENUE CHART
        private void LoadSalesChart()
        {
            chartSales.Series.Clear();

            Series series = new Series("Revenue")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };

            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                        CONVERT(date, orderDate) AS OrderDate,
                        SUM(TotalAmount) AS Revenue
                      FROM Orders
                      WHERE Status='Completed'
                      GROUP BY CONVERT(date, orderDate)
                      ORDER BY OrderDate", con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    series.Points.AddXY(
                        dr["OrderDate"].ToString(),
                        dr["Revenue"]);
                }
            }

            chartSales.Series.Add(series);
        }

        // ORDER STATUS PIE CHART
        private void LoadOrderPieChart()
        {
            chartOrders.Series.Clear();

            Series series = new Series("Orders")
            {
                ChartType = SeriesChartType.Doughnut
            };

            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT Status, COUNT(*) FROM Orders GROUP BY Status", con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    series.Points.AddXY(
                        dr["Status"].ToString(),
                        dr[1]);
                }
            }

            chartOrders.Series.Add(series);
        }

        // RECENT ORDERS TABLE
        // ============================
        private void LoadRecentOrders()
        {
            dgvRecentOrders.AutoGenerateColumns = false;
            using (SqlConnection con = DbConnection.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT     
                            o.OrderID,
                            u.FullName,
                            o.Status,
                            o.TotalAmount,
                            o.OrderDate
                     FROM Orders o
                     JOIN Users u ON o.UserID = u.UserID",
                    con);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecentOrders.DataSource = dt;
            }
        }

        private void panelOrderBreakDown_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

