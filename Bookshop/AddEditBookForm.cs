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

namespace Bookshop
{
    public partial class AddEditBookForm : Form
    {
        private int _bookId = 0;
    
        //ADD Mode
        public AddEditBookForm()
        {
            InitializeComponent();
            LoadCategories();
            ConfigureForm();

        }

        // EDIT MODE
        public AddEditBookForm(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            LoadCategories();
            ConfigureForm();
            LoadBookData();
        }


        // FORM CONFIGURATION
        private void ConfigureForm()
        {
            btnSave.Text = (_bookId == 0) ? "Save Book" : "Update Book";
        }

        //LOAD CATEGORIES
        private void LoadCategories()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT CategoryId, CategoryName FROM BookCategories", con);

                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);

                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";
                cmbCategory.SelectedIndex = -1;
            }
        }

        private void LoadBookData()
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Title, Author, CategoryId, Price, Stock,
                             Description, CoverImage
                      FROM Books WHERE BookId=@id", con);

                cmd.Parameters.AddWithValue("@id", _bookId);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtTitle.Text = dr["Title"].ToString();
                    txtAuthor.Text = dr["Author"].ToString();
                    cmbCategory.SelectedValue = dr["CategoryId"];
                    numPrice.Value = Convert.ToDecimal(dr["Price"]);                                   
                    numStock.Value = Convert.ToDecimal(dr["Stock"]);
                    txtDescription.Text = dr["Description"].ToString();

                    if (dr["CoverImage"] != DBNull.Value)
                    {
                        pictureBook.Image =
                            ImageHelper.ByteToImage((byte[])dr["CoverImage"]);
                    }
                }
            }
        }


        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBook.Image = Image.FromFile(ofd.FileName);
            }
        }

        // SAVE (INSERT / UPDATE)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                SqlCommand cmd;

                if (_bookId == 0)
                {
                    // INSERT
                    cmd = new SqlCommand(
                        @"INSERT INTO Books
                          (Title, Author, CategoryId, Price, Stock,
                           Description, CoverImage, CreatedAt)
                          VALUES
                          (@Title,@Author,@Category,@Price,@Stock,
                           @Desc,@Img,GETDATE())", con);
                }
                else
                {
                    // UPDATE
                    cmd = new SqlCommand(
                        @"UPDATE Books SET
                            Title=@Title,
                            Author=@Author,
                            CategoryId=@Category,
                            Price=@Price,
                            Stock=@Stock,
                            Description=@Desc,
                            CoverImage=@Img
                          WHERE BookId=@id", con);

                    cmd.Parameters.AddWithValue("@id", _bookId);
                }

                AddParameters(cmd);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show(
                        _bookId == 0 ? "Book added successfully" : "Book updated successfully",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

        }
        // Browse image

        private void AddEditBookForm_Load(object sender, EventArgs e)
        {

        }


        // ADD SQL PARAMETERS
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
            cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@Price", numPrice.Value);
            cmd.Parameters.AddWithValue("@Stock",numStock.Value);
            cmd.Parameters.AddWithValue("@Desc", txtDescription.Text.Trim());

            // 🔑 IMAGE — FORCE VARBINARY
            SqlParameter imgParam = new SqlParameter("@Img", SqlDbType.VarBinary);

            if (pictureBook.Image != null)
                imgParam.Value = ImageHelper.ImageToByte(pictureBook.Image);
            else
                imgParam.Value = DBNull.Value;

            cmd.Parameters.Add(imgParam);
        }


        // INPUT VALIDATION
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
               string.IsNullOrWhiteSpace(txtAuthor.Text) ||
               cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all required fields.",
                                "Validation",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
