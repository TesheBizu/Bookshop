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
    public partial class UserDashboard : Form
    {
        // shared state
        int selectedBookId = 0;
        decimal checkoutSubtotal = 0m;
        decimal checkoutShipping = 50m; // default shipping
        decimal checkoutTotal = 0m;
        string paymentMethod = "";


        public UserDashboard()
        {
            InitializeComponent();
        }


        private void HighlightNavButton(Button btn)
        {
            // Reset all other buttons inside panelLeft
            if (pnlLeftNavigation != null)
            {
                foreach (Control c in pnlLeftNavigation.Controls)
                {
                    Button b = c as Button;
                    if (b !=null)
                {
                        b.BackColor = Color.RoyalBlue;          // default sidebar color
                        b.ForeColor = Color.White;             // default text color
                        b.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                    }
                }
            }
            // Apply highlight to selected button

            if (btn != null)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.RoyalBlue;
                btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            }

        }
        private void ShowPanel(Panel panelToShow)
        {
            pnlHome.Visible = false;
            pnlBooks.Visible = false;
            pnlCategories.Visible = false;
            pnlWishList.Visible = false;
            pnlOrders.Visible = false;
            pnlCart.Visible = false;
            pnlProfile.Visible = false;

            panelToShow.Visible = true;
            panelToShow.BringToFront();
        }


        private void UserDashboard_Load(object sender, EventArgs e)
        {
            HighlightNavButton(btnHome);
            ShowPanel(pnlHome);
            LoadHomeData();
        }


        private void LoadHomeData()
        {
            LoadGreeting();
            LoadTotalBooks();
            LoadTotalCategories();
            LoadWishlist();
            LoadRecommendedBooks();
        }

        private void LoadGreeting()
        {
            lblFullname.Text = "Welcome, " + UserSession.fullname;
        }

        private void LoadTotalBooks()
        {
            string q = "SELECT COUNT(*) FROM Books";
            lblTotalBooks.Text = DbHelper.ExecuteScalar(q).ToString();
        }



        private void LoadTotalCategories()
        {
            string q = "SELECT COUNT(*) FROM BookCategories";
            lblTotalCategories.Text = DbHelper.ExecuteScalar(q).ToString();
        }



        private void LoadWishlist()
        {
            flowWishlist.Controls.Clear();

            SqlParameter[] p =
            {
        new SqlParameter("@uid", UserSession.UserID)
    };

            DataTable dt = DbHelper.GetTable(
                @"SELECT b.BookID, b.Title, b.Author, b.Price, b.CoverImage
          FROM Wishlist w
          INNER JOIN Books b ON w.BookID = b.BookID
          WHERE w.UserID = @uid", p);

            if (dt.Rows.Count == 0)
            {
                Label lbl = new Label();
                lbl.Text = "Your wishlist is empty.";
                lbl.AutoSize = true;
                flowWishlist.Controls.Add(lbl);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                Panel item = CreateWishlistItem(row);
                flowWishlist.Controls.Add(item);
            }
        }


        private Panel CreateWishlistItem(DataRow row)
        {
            Panel item = new Panel();
            item = ClonePanel(pnlWishlistTemplete);
            item.Visible = true;

            int bookId = Convert.ToInt32(row["BookID"]);

            // Text
            ((Label)item.Controls["lblWishTitle"]).Text = row["Title"].ToString();
            ((Label)item.Controls["lblWishAuthor"]).Text = "by " + row["Author"].ToString();
            ((Label)item.Controls["lblWishPrice"]).Text =
                "ETB " + Convert.ToDecimal(row["Price"]).ToString("0.00");

            // Image
            if (row["CoverImage"] != DBNull.Value)
            {
                ((PictureBox)item.Controls["pbWishImage"])
                    .Image = ImageHelper.ByteToImage((byte[])row["CoverImage"]);
            }

 // Buttons
 ((Button)item.Controls["btnRemoveWish"]).Click += (s, e) =>
 {
     RemoveFromWishlist(bookId);
     LoadWishlist();
 };

            ((Button)item.Controls["btnMoveToCart"]).Click += (s, e) =>
            {
                AddBookToCart(bookId);
                RemoveFromWishlist(bookId);
                LoadWishlist();
            };

            return item;
        }



        private Panel ClonePanel(Panel source)
        {
            Panel clone = new Panel();
            clone.Size = source.Size;
            clone.BackColor = source.BackColor;

            foreach (Control c in source.Controls)
            {
                Control copy = (Control)Activator.CreateInstance(c.GetType());
                copy.Name = c.Name;
                copy.Text = c.Text;
                copy.Size = c.Size;
                copy.Location = c.Location;
                copy.Font = c.Font;
                copy.ForeColor = c.ForeColor;
                copy.BackColor = c.BackColor;

                if (c is PictureBox)
                    ((PictureBox)copy).SizeMode = ((PictureBox)c).SizeMode;

                clone.Controls.Add(copy);
            }

            return clone;
        }


        private void AddBookToCart(int bookId)
        {
            try
            {
                // CHECK IF EXISTS
                SqlParameter[] checkParams =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", bookId)
        };

                DataTable dt = DbHelper.GetTable(
                    "SELECT Quantity FROM Cart WHERE UserID=@uid AND BookID=@bid",
                    checkParams
                );

                if (dt.Rows.Count > 0)
                {
                    // UPDATE
                    SqlParameter[] updateParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", bookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@uid AND BookID=@bid",
                        updateParams
                    );
                }
                else
                {
                    // INSERT
                    SqlParameter[] insertParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", bookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "INSERT INTO Cart (UserID, BookID, Quantity) VALUES (@uid, @bid, 1)",
                        insertParams
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add to cart error: " + ex.Message);
            }
        }


        private void RemoveFromWishlist(int bookId)
        {
            try
            {
                SqlParameter[] p =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", bookId)
        };

                DbHelper.ExecuteNonQuery(
                    "DELETE FROM Wishlist WHERE UserID=@uid AND BookID=@bid",
                    p
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Remove wishlist error: " + ex.Message);
            }
        }



        private void LoadRecommendedBooks()
        {
            if (flowRecommended != null) flowRecommended.Controls.Clear();

            string q = "SELECT TOP 4 BookID, Title,Author, Price, CoverImage FROM Books ORDER BY NEWID()";
            DataTable dt = DbHelper.GetTable(q);
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                // safe id read
                int id = row.Table.Columns.Contains("BookID") ? Convert.ToInt32(row["BookID"]) : 0;
                string title = row.Table.Columns.Contains("Title") ? row["Title"].ToString() : "";
                string author = row.Table.Columns.Contains("Author") ? row["Author"].ToString() : "";
                decimal price = row.Table.Columns.Contains("Price") ? Convert.ToDecimal(row["Price"]) : 0m;
                byte[] coverImage = (row.Table.Columns.Contains("CoverImage") && row["CoverImage"] != DBNull.Value) ? (byte[])row["CoverImage"] : null;

               Panel card = CreateBookCard(id, title, author, price, coverImage);    // <-- 5 args
               flowRecommended.Controls.Add(card);
            }
        }

        private Panel CreateBookCard(int bookId, string title, string author, decimal price, byte[] imageData)
        {
            Panel card = new Panel
            {
                Size = new Size(180, 300),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(8)
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(160, 120),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (imageData != null) pic.Image = ImageHelper.ByteToImage(imageData);
            card.Controls.Add(pic);

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 140),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            Label lblAuthor = new Label
            {
                Text = author,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(10, 165),
                AutoSize = true
            };
            card.Controls.Add(lblAuthor);

            Label lblPrice = new Label
            {
                Text = "ETB " + price.ToString("0.00"),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.RoyalBlue,
                Location = new Point(10, 195),
                AutoSize = true
            };
            card.Controls.Add(lblPrice);

            Button btnView = new Button
            {
                Text = "View",
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                Size = new Size(150, 30),
                Location = new Point(10, 220),
                FlatStyle = FlatStyle.Flat
            };
            btnView.Click += (s, e) =>
            {
                selectedBookId = bookId;
                ShowBookDetails(bookId);
            };
            card.Controls.Add(btnView);

            Button btnAdd = new Button
            {
                Text = "Add to Cart",
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                Size = new Size(150, 30),
                Location = new Point(10, 255),
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.Click += (s, e) => AddToCartFromCard(bookId);
            card.Controls.Add(btnAdd);

            return card;
        }




        private void LoadAllBooks()
        {
            flowBooks.Controls.Clear();

            string q = @"SELECT BookID, Title, Author, Price, CoverImage
                 FROM Books
                 ORDER BY NEWID()";

            DataTable dt = DbHelper.GetTable(q);
            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["BookID"]);
                string title = row["Title"].ToString();
                string author = row.Table.Columns.Contains("Author")
    ? row["Author"].ToString()
    : "";
                decimal price = Convert.ToDecimal(row["Price"]);

                byte[] coverImage =
                    row["CoverImage"] != DBNull.Value
                        ? (byte[])row["CoverImage"]
                        : null;

                Panel card = CreateBookCard(id, title, author, price, coverImage);
                flowBooks.Controls.Add(card);
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnBook);
            lblTitle.Text = "Books";
            ShowPanel(pnlBooks);

            LoadAllBooks();

        }


        private void LoadBookDetails(int bookId)
        {
            selectedBookId = bookId;

            string query = @"SELECT b.BookID, b.Title, b.Author, b.Price, b.Description, 
                            b.CoverImage, c.CategoryName, b.Stock
                     FROM Books b
                     JOIN BookCategories c ON b.CategoryID = c.CategoryID
                     WHERE b.BookID = @id";

            SqlParameter[] p =
            {
        new SqlParameter("@id", bookId)
    };

            DataTable dt = DbHelper.GetTable(query, p);

            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            // TEXT
            lblBookTitle.Text = row["Title"].ToString();
            lblBookAuthor.Text = "By " + row["Author"].ToString();
            lblBookPrice.Text = "ETB " + Convert.ToDecimal(row["Price"]).ToString("0.00");
            lblDescription.Text = row["Description"].ToString();
            lblBookCategory.Text = row["CategoryName"].ToString();
            lblBookStock.Text = "Stock: " + row["Stock"].ToString();

            // IMAGE
            if (row["CoverImage"] != DBNull.Value)
            {
                byte[] coverImage = (byte[])row["CoverImage"];
                picBookCover.Image = ImageHelper.ByteToImage(coverImage);
            }

            // SHOW PANEL
            ShowPanel(pnlBookDetails);
            lblTitle.Text = "Book Details";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlBooks);
            HighlightNavButton(btnBook);
            lblTitle.Text = "Boooks";
            LoadAllBooks();
        }



        private void btnAddBookToWishList_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
                return;

            try
            {
                // ----------- CHECK IF EXISTS -----------
                SqlParameter[] checkParams =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", selectedBookId)
        };

                DataTable dt = DbHelper.GetTable(
                    "SELECT 1 FROM Wishlist WHERE UserID=@uid AND BookID=@bid",
                    checkParams
                );

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("This book is already in your wishlist.");
                    return;
                }

                // ----------- INSERT -----------
                SqlParameter[] insertParams =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", selectedBookId)
        };

                DbHelper.ExecuteNonQuery(
                    "INSERT INTO Wishlist (UserID, BookID) VALUES (@uid, @bid)",
                    insertParams
                );

                MessageBox.Show("Book added to wishlist.");

                // Optional UX improvement
                btnAddBookToWishList.Enabled = false;
                btnAddBookToWishList.Text = "In Wishlist";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wishlist error: " + ex.Message);
            }
        }




        private void btnAddBookToCart_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
                return;

            try
            {
                // ---------- CHECK ----------
                SqlParameter[] checkParams =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", selectedBookId)
        };

                DataTable dt = DbHelper.GetTable(
                    "SELECT Quantity FROM Cart WHERE UserID=@uid AND BookID=@bid",
                    checkParams
                );

                if (dt.Rows.Count > 0)
                {
                    // ---------- UPDATE ----------
                    SqlParameter[] updateParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", selectedBookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@uid AND BookID=@bid",
                        updateParams
                    );

                    MessageBox.Show("Quantity increased in cart.");
                }
                else
                {
                    // ---------- INSERT ----------
                    SqlParameter[] insertParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", selectedBookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "INSERT INTO Cart (UserID, BookID, Quantity) VALUES (@uid, @bid, 1)",
                        insertParams
                    );

                    MessageBox.Show("Book added to cart.");
                }

                // Disable button after add
                btnAddBookToCart.Text = "Already in Cart";
                btnAddBookToCart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add to cart error: " + ex.Message);
            }
        }




        private void LoadCart()
        {
            flowCart.Controls.Clear();
            decimal totalAmount = 0;

            string q = @"
        SELECT c.CartID, c.Quantity, 
               b.Title, b.Author, b.Price, b.CoverImage
        FROM Cart c
        INNER JOIN Books b ON c.BookID = b.BookID
        WHERE c.UserID = @uid
    ";

            SqlParameter[] p =
            {
        new SqlParameter("@uid", UserSession.UserID)
    };

            DataTable dt = DbHelper.GetTable(q, p);

            foreach (DataRow row in dt.Rows)
            {
                Panel card = CreateCartItemCard(row);
                flowCart.Controls.Add(card);

                // Calculate total
                decimal price = Convert.ToDecimal(row["Price"]);
                int qty = Convert.ToInt32(row["Quantity"]);
                totalAmount += price * qty;
            }

            lblTotal.Text = "ETB " + totalAmount.ToString("0.00");
        }


        private void btnCart_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnCart);
            lblTitle.Text = "My Cart";
            ShowPanel(pnlCart);
            LoadCart();
        }


        private void btnCheckout_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnCart);
            ShowPanel(pnlCheckout);
            lblTitle.Text = "Checkout";
            LoadCheckoutData();
        }

        private void LoadCheckoutData()
        {

            string q = @"SELECT c.Quantity, b.Price
                         FROM Cart c
                         INNER JOIN Books b ON c.BookID = b.BookID
                         WHERE c.UserID=@u";

            SqlParameter[] p =
            {
                new SqlParameter("@u", UserSession.UserID)
            };

            DataTable dt = DbHelper.GetTable(q, p);

            checkoutSubtotal = 0;

            foreach (DataRow r in dt.Rows)
            {
                checkoutSubtotal += Convert.ToInt32(r["Quantity"]) * Convert.ToDecimal(r["Price"]);
            }

            checkoutShipping = (checkoutSubtotal >= 500) ? 0 : 50;
            checkoutTotal = checkoutSubtotal + checkoutShipping;

            lblCheckoutSubtotal.Text = "ETB " + checkoutSubtotal.ToString("0.00");
            lblCheckoutShipping.Text = "ETB " + checkoutShipping.ToString("0.00");
            lblCheckoutTotal.Text = "ETB " + checkoutTotal.ToString("0.00");

            lblFullname.Text = UserSession.fullname;
            lblEmailCheckout.Text = UserSession.Email;
            cmbPayment.SelectedIndex = 0;
        }


        private void btnContinuePayment_Click(object sender, EventArgs e)
        {
            if (cmbPayment.SelectedIndex == -1)
            {
                MessageBox.Show("Choose payment method");
                return;
            }

            paymentMethod = cmbPayment.SelectedItem.ToString();

            ShowPanel(pnlPayment);
            lblTitle.Text = "Payment";

            lblPaymentSubtotal.Text = "ETB " + checkoutSubtotal.ToString("0.00");
            lblPaymentShipping.Text = "ETB " + checkoutShipping.ToString("0.00");
            lblPaymentTotal.Text = "ETB " + checkoutTotal.ToString("0.00");
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // -----------------------------
                // VALIDATION
                // -----------------------------
                if (cmbPayment.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a payment method.");
                    return;
                }

                paymentMethod = cmbPayment.SelectedItem.ToString();

                // Read totals safely (remove ETB and spaces)
                checkoutSubtotal = Convert.ToDecimal(lblCheckoutSubtotal.Text.Replace("ETB", "").Trim());
                checkoutShipping = Convert.ToDecimal(lblCheckoutShipping.Text.Replace("ETB", "").Trim());
                checkoutTotal = Convert.ToDecimal(lblCheckoutTotal.Text.Replace("ETB", "").Trim());


                // -----------------------------
                // 1️⃣ INSERT INTO ORDERS
                // -----------------------------
                string orderQ = @"
              INSERT INTO Orders (UserID, OrderDate, SubTotal, Shipping, totalAmount, PaymentMethod, Status)
              OUTPUT INSERTED.OrderID
              VALUES (@u, GETDATE(), @sub, @ship, @tot, @pay, 'Processing')
          ";

                SqlParameter[] p =
                {
              new SqlParameter("@u", UserSession.UserID),
              new SqlParameter("@sub", checkoutSubtotal),
              new SqlParameter("@ship", checkoutShipping),
              new SqlParameter("@tot", checkoutTotal),
              new SqlParameter("@pay", paymentMethod),
          };

                object result = DbHelper.ExecuteScalar(orderQ, p);

                if (result == null)
                {
                    MessageBox.Show("Order failed!");
                    return;
                }

                int orderId = Convert.ToInt32(result);


                // -----------------------------
                // 2️⃣ INSERT ORDER ITEMS
                // -----------------------------
                string cartQ = @"
              SELECT c.BookID, c.Quantity, b.Price
              FROM Cart c
              INNER JOIN Books b ON c.BookID = b.BookID
              WHERE UserID=@u
          ";

                DataTable cartItems = DbHelper.GetTable(cartQ,
                    new SqlParameter[] { new SqlParameter("@u", UserSession.UserID) });

                foreach (DataRow row in cartItems.Rows)
                {
                    string itemQ = @"
                  INSERT INTO OrderItems (OrderID, BookID, Quantity, Price)
                  VALUES (@oid, @bid, @qty, @price)
              ";

                    SqlParameter[] p3 =
                    {
                  new SqlParameter("@oid", orderId),
                  new SqlParameter("@bid", Convert.ToInt32(row["BookID"])),
                  new SqlParameter("@qty", Convert.ToInt32(row["Quantity"])),
                  new SqlParameter("@price", Convert.ToDecimal(row["Price"]))
              };

                    DbHelper.ExecuteNonQuery(itemQ, p3);
                }


                // -----------------------------
                // 3️⃣ CLEAR CART
                // -----------------------------
                DbHelper.ExecuteNonQuery(
                    "DELETE FROM Cart WHERE UserID=@u",
                    new SqlParameter[] { new SqlParameter("@u", UserSession.UserID) }
                );


                // -----------------------------
                // 4️⃣ FINISH
                // -----------------------------
                MessageBox.Show("Order placed successfully!");

                ShowPanel(pnlOrders);
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Order error: " + ex.Message);
            }
        }





        private void LoadOrders()
        {
            flowOrders.Controls.Clear();

            string q = @"
        SELECT OrderID, OrderDate, totalAmount, Status
        FROM Orders
        WHERE UserID = @uid
        ORDER BY OrderID DESC
    ";

            SqlParameter[] p =
            {
        new SqlParameter("@uid", UserSession.UserID)
    };

            DataTable dt = DbHelper.GetTable(q, p);

            foreach (DataRow row in dt.Rows)
            {
                Panel card = new Panel();
                card.Size = new Size(600, 100);
                card.BackColor = Color.White;
                card.Margin = new Padding(5);

                Label lbl = new Label();
                lbl.Text = "Order #" + row["OrderID"]
                    + "\nDate: " + Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd")
                    + "\nTotal: ETB " + row["totalAmount"]
                    + "\nStatus: " + row["Status"];
                lbl.Font = new Font("Segoe UI", 10);
                lbl.Location = new Point(10, 10);
                lbl.AutoSize = true;

                card.Controls.Add(lbl);
                flowOrders.Controls.Add(card);
            }
        }


        private void btnOrders_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnOrders);
            lblTitle.Text = "My Orders";
            ShowPanel(pnlOrders);
            LoadOrders();

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnProfile);
            lblTitle.Text = "Profile";
            ShowPanel(pnlProfile);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnLogout);

            // Close dashboard → open Login Form
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }


        private Panel CreateCartItemCard(DataRow row)
        {
            int cartId = Convert.ToInt32(row["CartID"]);
            int quantity = Convert.ToInt32(row["Quantity"]);

            Panel card = new Panel();
            card.Size = new Size(620, 120);
            card.BackColor = Color.White;
            card.Margin = new Padding(5);
            card.Padding = new Padding(10);

            // IMAGE
            PictureBox pic = new PictureBox();
            pic.Size = new Size(80, 100);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Location = new Point(10, 10);

            if (row["CoverImage"] != DBNull.Value)
            {
                byte[] img = (byte[])row["CoverImage"];
                pic.Image = ImageHelper.ByteToImage(img);
            }
            card.Controls.Add(pic);

            // TITLE 
            Label lblTitle = new Label();
            lblTitle.Text = row["Title"].ToString();
            lblTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTitle.Location = new Point(110, 10);
            lblTitle.AutoSize = true;
            card.Controls.Add(lblTitle);

            //  PRICE
            Label lblPrice = new Label();
            lblPrice.Text = "ETB " + Convert.ToDecimal(row["Price"]).ToString("0.00");
            lblPrice.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPrice.Location = new Point(110, 40);
            lblPrice.AutoSize = true;
            card.Controls.Add(lblPrice);

            //  QUANTITY 
            Label lblQty = new Label();
            lblQty.Text = quantity.ToString();
            lblQty.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblQty.Location = new Point(220, 70);
            lblQty.AutoSize = true;

            // Increase button (+)
            Button btnPlus = new Button();
            btnPlus.Text = "+";
            btnPlus.Size = new Size(30, 30);
            btnPlus.Location = new Point(260, 65);
            btnPlus.Click += (s, e) => UpdateCartQuantity(cartId, quantity + 1);

            // Decrease button (–)
            Button btnMinus = new Button();
            btnMinus.Text = "-";
            btnMinus.Size = new Size(30, 30);
            btnMinus.Location = new Point(180, 65);
            btnMinus.Click += (s, e) =>
            {
                if (quantity > 1)
                    UpdateCartQuantity(cartId, quantity - 1);
            };

            card.Controls.Add(btnMinus);
            card.Controls.Add(lblQty);
            card.Controls.Add(btnPlus);

            // ----------- REMOVE BUTTON -------------
            Button btnRemove = new Button();
            btnRemove.Text = "Remove";
            btnRemove.BackColor = Color.Firebrick;
            btnRemove.ForeColor = Color.White;
            btnRemove.Size = new Size(80, 30);
            btnRemove.Location = new Point(350, 65);

            btnRemove.Click += (s, e) => RemoveCartItem(cartId);
            card.Controls.Add(btnRemove);

            return card;
        }

        private void UpdateCartQuantity(int cartId, int newQty)
        {
            string q = "UPDATE Cart SET Quantity=@qty WHERE CartID=@cid";

            SqlParameter[] p =
            {
        new SqlParameter("@qty", newQty),
        new SqlParameter("@cid", cartId)
    };

            DbHelper.ExecuteNonQuery(q, p);

            LoadCart();
        }

        private void RemoveCartItem(int cartId)
        {
            string q = "DELETE FROM Cart WHERE CartID=@cid";

            SqlParameter[] p =
            {
        new SqlParameter("@cid", cartId)
    };

            DbHelper.ExecuteNonQuery(q, p);

            LoadCart();
        }


     



        private void AddToCartFromCard(int bookId)
        {
            try
            {
                SqlParameter[] checkParams =
                {
            new SqlParameter("@uid", UserSession.UserID),
            new SqlParameter("@bid", bookId)
        };

                DataTable dt = DbHelper.GetTable(
                    "SELECT Quantity FROM Cart WHERE UserID=@uid AND BookID=@bid",
                    checkParams
                );

                if (dt.Rows.Count > 0)
                {
                    SqlParameter[] updateParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", bookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@uid AND BookID=@bid",
                        updateParams
                    );
                }
                else
                {
                    SqlParameter[] insertParams =
                    {
                new SqlParameter("@uid", UserSession.UserID),
                new SqlParameter("@bid", bookId)
            };

                    DbHelper.ExecuteNonQuery(
                        "INSERT INTO Cart (UserID, BookID, Quantity) VALUES (@uid, @bid, 1)",
                        insertParams
                    );
                }

                MessageBox.Show("Added to cart!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void ShowBookDetails(int bookId)
        {
            try
            {
                selectedBookId = bookId;

                // Reset button state every time
                btnAddBookToCart.Enabled = true;
                btnAddBookToCart.Text = "Add to Cart";

                // SQL with JOIN (VERY IMPORTANT)
                string q = @"
        SELECT 
            b.BookID,
            b.Title,
            b.Author,
            b.Price,
            b.Description,
            b.Stock,
            b.CoverImage,
            c.CategoryName
        FROM Books b
        INNER JOIN BookCategories c ON b.CategoryID = c.CategoryID
        WHERE b.BookID = @id";

                SqlParameter[] p =
                {
            new SqlParameter("@id", bookId)
        };

                DataTable dt = DbHelper.GetTable(q, p);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Book not found.");
                    return;
                }

                DataRow row = dt.Rows[0];

                // ---------------- TEXT ----------------
                lblTitleBook.Text = row["Title"].ToString();
                lblAuthorBook.Text = "By " + row["Author"].ToString();
                lblBookCategory.Text = "Category: " + row["categoryName"].ToString();
                lblBookStock.Text = "Stock: " + row["Stock"].ToString();
                lblPriceBook.Text = "ETB " + Convert.ToDecimal(row["Price"]).ToString("0.00");
                lblDescription.Text = row["Description"].ToString();

                // ---------------- IMAGE ----------------
                if (row["CoverImage"] != DBNull.Value)
                {
                    byte[] coverImage = (byte[])row["CoverImage"];
                    picBookCover.Image = ImageHelper.ByteToImage(coverImage);
                }
                else
                {
                    picBookCover.Image = null;
                }

                // ---------------- CART STATE ----------------
                if (IsBookInCart(bookId))
                {
                    btnAddBookToCart.Text = "Already in Cart";
                    btnAddBookToCart.Enabled = false;
                }
                // reset first
                btnAddBookToWishList.Enabled = true;
                btnAddBookToWishList.Text = "Add to Wishlist";

                // ---------------- WISHLIST STATE ----------------
                if (IsBookInWishlist(bookId))
                {
                    btnAddBookToWishList.Text = "In Wishlist";
                    btnAddBookToWishList.Enabled = false;
                }

                // ---------------- SHOW PANEL ----------------
                lblTitle.Text = "Book Details";
                ShowPanel(pnlBookDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Show details error: " + ex.Message);
            }

        }


        private bool IsBookInCart(int bookId)
        {
            string q = "SELECT COUNT(*) FROM Cart WHERE UserID=@uid AND BookID=@bid";
            SqlParameter[] p =
            {
        new SqlParameter("@uid", UserSession.UserID),
        new SqlParameter("@bid", bookId)
    };

            int count = Convert.ToInt32(DbHelper.ExecuteScalar(q, p));
            return count > 0;
        }

        private bool IsBookInWishlist(int bookId)
        {
            SqlParameter[] p =
            {
        new SqlParameter("@uid", UserSession.UserID),
        new SqlParameter("@bid", bookId)
    };

            int count = Convert.ToInt32(
                DbHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Wishlist WHERE UserID=@uid AND BookID=@bid",
                    p
                )
            );

            return count > 0;
        }




        private void btnHome_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnHome);
            lblTitle.Text = "Home";
            ShowPanel(pnlHome);

            LoadHomeData();
        }




        private void btnCategories_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnCategories);
            lblTitle.Text = "Categories";
            ShowPanel(pnlCategories);
        }

        private void btnWishlist_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnWishlist);
            lblTitle.Text = "Wishlist";
            ShowPanel(pnlWishList);
            LoadWishlist();
        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Searching: " + txtSearch.Text);
        }
    


    }

    
   

}
