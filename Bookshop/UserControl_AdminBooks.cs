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

namespace Bookshop
{
    public partial class UserControl_AdminBooks : UserControl
    {
        private readonly string placeholderText = "Search books...";

        public UserControl_AdminBooks()
        {
            InitializeComponent();
            SetupPlaceholder();

        }

        private void UserControl_AdminBooks_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void SetupPlaceholder()
        {
            txtSearch.Text = placeholderText;
            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == placeholderText)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = placeholderText;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == placeholderText)
                return;

            LoadBooks(txtSearch.Text.Trim());
        }


        private void LoadBooks(string search = "")
        {
            dataGridViewBooks.AutoGenerateColumns = false;
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT 
                B.BookId,
                B.Title,
                B.Author,
                C.CategoryName AS Category,
                B.Price,
                B.Stock,
                B.Status
            FROM Books B
            INNER JOIN BookCategories C
                ON B.CategoryId = C.CategoryId
            WHERE (@s = ''
                   OR B.Title LIKE @like
                   OR B.Author LIKE @like
                   OR C.CategoryName LIKE @like)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@s", search);
                cmd.Parameters.AddWithValue("@like", "%" + search + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewBooks.DataSource = dt;
            }
        }

        private void DeleteBook(int bookId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Books WHERE BookID = @BookID", conn);

                cmd.Parameters.AddWithValue("@BookID", bookId);
                cmd.ExecuteNonQuery();
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditBookForm frm = new AddEditBookForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int bookId = Convert.ToInt32(
                dataGridViewBooks.Rows[e.RowIndex].Cells["colBookID"].Value);

            if (dataGridViewBooks.Columns[e.ColumnIndex].Name == "colEdit")
            {
                AddEditBookForm frm = new AddEditBookForm(bookId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
            else if (dataGridViewBooks.Columns[e.ColumnIndex].Name == "colDelete")
            {
                // CONFIRM DELETE
                if (MessageBox.Show("Delete this book?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteBook(bookId);
                    LoadBooks();
                }
            }
        }

       


    }
}
