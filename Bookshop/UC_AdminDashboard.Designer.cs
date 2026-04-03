namespace Bookshop
{
    partial class UC_AdminDashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelBooks = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.lblBooks = new System.Windows.Forms.Label();
            this.panelCard3 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblCustomers = new System.Windows.Forms.Label();
            this.panelCard2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblOrders = new System.Windows.Forms.Label();
            this.panelCard4 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.panelSales = new System.Windows.Forms.Panel();
            this.chartSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelTitleSales = new System.Windows.Forms.Label();
            this.panelOrderBreakDown = new System.Windows.Forms.Panel();
            this.chartOrders = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTitleDashboard = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            this.ColOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelCard3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panelCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelCard4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panelSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).BeginInit();
            this.panelOrderBreakDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBooks
            // 
            this.panelBooks.BackColor = System.Drawing.Color.White;
            this.panelBooks.Controls.Add(this.pictureBox1);
            this.panelBooks.Controls.Add(this.lblTotalBooks);
            this.panelBooks.Controls.Add(this.lblBooks);
            this.panelBooks.Location = new System.Drawing.Point(20, 80);
            this.panelBooks.Margin = new System.Windows.Forms.Padding(10);
            this.panelBooks.Name = "panelBooks";
            this.panelBooks.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelBooks.Size = new System.Drawing.Size(230, 110);
            this.panelBooks.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(20, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooks.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalBooks.Location = new System.Drawing.Point(80, 50);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(33, 37);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "0";
            // 
            // lblBooks
            // 
            this.lblBooks.AutoSize = true;
            this.lblBooks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBooks.ForeColor = System.Drawing.Color.SlateGray;
            this.lblBooks.Location = new System.Drawing.Point(80, 20);
            this.lblBooks.Name = "lblBooks";
            this.lblBooks.Size = new System.Drawing.Size(75, 17);
            this.lblBooks.TabIndex = 0;
            this.lblBooks.Text = "Total Books";
            // 
            // panelCard3
            // 
            this.panelCard3.BackColor = System.Drawing.Color.White;
            this.panelCard3.Controls.Add(this.pictureBox3);
            this.panelCard3.Controls.Add(this.lblTotalCustomers);
            this.panelCard3.Controls.Add(this.lblCustomers);
            this.panelCard3.Location = new System.Drawing.Point(540, 80);
            this.panelCard3.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard3.Name = "panelCard3";
            this.panelCard3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelCard3.Size = new System.Drawing.Size(230, 110);
            this.panelCard3.TabIndex = 1;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(20, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 40);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalCustomers.Location = new System.Drawing.Point(80, 50);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(33, 37);
            this.lblTotalCustomers.TabIndex = 2;
            this.lblTotalCustomers.Text = "0";
            // 
            // lblCustomers
            // 
            this.lblCustomers.AutoSize = true;
            this.lblCustomers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomers.ForeColor = System.Drawing.Color.DimGray;
            this.lblCustomers.Location = new System.Drawing.Point(80, 20);
            this.lblCustomers.Name = "lblCustomers";
            this.lblCustomers.Size = new System.Drawing.Size(102, 17);
            this.lblCustomers.TabIndex = 1;
            this.lblCustomers.Text = "Total Customers";
            // 
            // panelCard2
            // 
            this.panelCard2.BackColor = System.Drawing.Color.White;
            this.panelCard2.Controls.Add(this.pictureBox2);
            this.panelCard2.Controls.Add(this.lblTotalOrders);
            this.panelCard2.Controls.Add(this.lblOrders);
            this.panelCard2.Location = new System.Drawing.Point(280, 80);
            this.panelCard2.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard2.Name = "panelCard2";
            this.panelCard2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelCard2.Size = new System.Drawing.Size(230, 110);
            this.panelCard2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(20, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalOrders.Location = new System.Drawing.Point(80, 50);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(33, 37);
            this.lblTotalOrders.TabIndex = 2;
            this.lblTotalOrders.Text = "0";
            // 
            // lblOrders
            // 
            this.lblOrders.AutoSize = true;
            this.lblOrders.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrders.ForeColor = System.Drawing.Color.DimGray;
            this.lblOrders.Location = new System.Drawing.Point(80, 20);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(81, 17);
            this.lblOrders.TabIndex = 1;
            this.lblOrders.Text = "Total Orders";
            // 
            // panelCard4
            // 
            this.panelCard4.BackColor = System.Drawing.Color.White;
            this.panelCard4.Controls.Add(this.pictureBox4);
            this.panelCard4.Controls.Add(this.lblTotalRevenue);
            this.panelCard4.Controls.Add(this.lblRevenue);
            this.panelCard4.Location = new System.Drawing.Point(800, 80);
            this.panelCard4.Margin = new System.Windows.Forms.Padding(10);
            this.panelCard4.Name = "panelCard4";
            this.panelCard4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelCard4.Size = new System.Drawing.Size(230, 110);
            this.panelCard4.TabIndex = 2;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(20, 20);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 40);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalRevenue.Location = new System.Drawing.Point(80, 50);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(33, 37);
            this.lblTotalRevenue.TabIndex = 2;
            this.lblTotalRevenue.Text = "0";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenue.ForeColor = System.Drawing.Color.DimGray;
            this.lblRevenue.Location = new System.Drawing.Point(80, 20);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(57, 17);
            this.lblRevenue.TabIndex = 1;
            this.lblRevenue.Text = "Revenue";
            // 
            // panelSales
            // 
            this.panelSales.BackColor = System.Drawing.Color.White;
            this.panelSales.Controls.Add(this.chartSales);
            this.panelSales.Controls.Add(this.labelTitleSales);
            this.panelSales.Location = new System.Drawing.Point(20, 220);
            this.panelSales.Name = "panelSales";
            this.panelSales.Size = new System.Drawing.Size(510, 280);
            this.panelSales.TabIndex = 3;
            // 
            // chartSales
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSales.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSales.Legends.Add(legend1);
            this.chartSales.Location = new System.Drawing.Point(20, 40);
            this.chartSales.Name = "chartSales";
            this.chartSales.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 0;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series1.Color = System.Drawing.Color.RoyalBlue;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.ShadowColor = System.Drawing.Color.Transparent;
            this.chartSales.Series.Add(series1);
            this.chartSales.Size = new System.Drawing.Size(470, 220);
            this.chartSales.TabIndex = 1;
            this.chartSales.Text = "chart1";
            // 
            // labelTitleSales
            // 
            this.labelTitleSales.AutoSize = true;
            this.labelTitleSales.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleSales.Location = new System.Drawing.Point(20, 10);
            this.labelTitleSales.Name = "labelTitleSales";
            this.labelTitleSales.Size = new System.Drawing.Size(114, 20);
            this.labelTitleSales.TabIndex = 0;
            this.labelTitleSales.Text = "Sales Overview";
            // 
            // panelOrderBreakDown
            // 
            this.panelOrderBreakDown.BackColor = System.Drawing.Color.White;
            this.panelOrderBreakDown.Controls.Add(this.chartOrders);
            this.panelOrderBreakDown.Controls.Add(this.label7);
            this.panelOrderBreakDown.Location = new System.Drawing.Point(560, 220);
            this.panelOrderBreakDown.Name = "panelOrderBreakDown";
            this.panelOrderBreakDown.Size = new System.Drawing.Size(470, 280);
            this.panelOrderBreakDown.TabIndex = 4;
            this.panelOrderBreakDown.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOrderBreakDown_Paint);
            // 
            // chartOrders
            // 
            chartArea2.Name = "ChartArea1";
            this.chartOrders.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartOrders.Legends.Add(legend2);
            this.chartOrders.Location = new System.Drawing.Point(20, 40);
            this.chartOrders.Name = "chartOrders";
            series2.BorderColor = System.Drawing.Color.Transparent;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "Series_1";
            series2.YValuesPerPoint = 4;
            this.chartOrders.Series.Add(series2);
            this.chartOrders.Size = new System.Drawing.Size(420, 220);
            this.chartOrders.TabIndex = 2;
            this.chartOrders.Text = "chart1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "Orders Breakdown";
            // 
            // lblTitleDashboard
            // 
            this.lblTitleDashboard.AutoSize = true;
            this.lblTitleDashboard.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleDashboard.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitleDashboard.Location = new System.Drawing.Point(20, 20);
            this.lblTitleDashboard.Name = "lblTitleDashboard";
            this.lblTitleDashboard.Size = new System.Drawing.Size(157, 37);
            this.lblTitleDashboard.TabIndex = 6;
            this.lblTitleDashboard.Text = "Dashboard";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 510);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Recent Orders";
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AllowUserToDeleteRows = false;
            this.dgvRecentOrders.AllowUserToResizeRows = false;
            this.dgvRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentOrders.ColumnHeadersHeight = 35;
            this.dgvRecentOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrderID,
            this.ColCustomer,
            this.colStatus,
            this.ColAmount,
            this.ColOrderDate});
            this.dgvRecentOrders.EnableHeadersVisualStyles = false;
            this.dgvRecentOrders.GridColor = System.Drawing.Color.Silver;
            this.dgvRecentOrders.Location = new System.Drawing.Point(20, 550);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentOrders.RowHeadersVisible = false;
            this.dgvRecentOrders.RowHeadersWidth = 32;
            this.dgvRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentOrders.Size = new System.Drawing.Size(1010, 150);
            this.dgvRecentOrders.TabIndex = 5;
            // 
            // ColOrderID
            // 
            this.ColOrderID.DataPropertyName = "OrderId";
            this.ColOrderID.HeaderText = "Order ID";
            this.ColOrderID.Name = "ColOrderID";
            this.ColOrderID.ReadOnly = true;
            // 
            // ColCustomer
            // 
            this.ColCustomer.DataPropertyName = "fullname";
            this.ColCustomer.HeaderText = "Customer";
            this.ColCustomer.Name = "ColCustomer";
            this.ColCustomer.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // ColAmount
            // 
            this.ColAmount.DataPropertyName = "TotalAmount";
            this.ColAmount.HeaderText = "Amount";
            this.ColAmount.Name = "ColAmount";
            this.ColAmount.ReadOnly = true;
            // 
            // ColOrderDate
            // 
            this.ColOrderDate.DataPropertyName = "orderDate";
            this.ColOrderDate.HeaderText = "Oder Date";
            this.ColOrderDate.Name = "ColOrderDate";
            this.ColOrderDate.ReadOnly = true;
            // 
            // UC_AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblTitleDashboard);
            this.Controls.Add(this.dgvRecentOrders);
            this.Controls.Add(this.panelOrderBreakDown);
            this.Controls.Add(this.panelSales);
            this.Controls.Add(this.panelCard4);
            this.Controls.Add(this.panelCard2);
            this.Controls.Add(this.panelCard3);
            this.Controls.Add(this.panelBooks);
            this.Name = "UC_AdminDashboard";
            this.Size = new System.Drawing.Size(1050, 728);
            this.Load += new System.EventHandler(this.UC_AdminDashboard_Load);
            this.panelBooks.ResumeLayout(false);
            this.panelBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelCard3.ResumeLayout(false);
            this.panelCard3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panelCard2.ResumeLayout(false);
            this.panelCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelCard4.ResumeLayout(false);
            this.panelCard4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panelSales.ResumeLayout(false);
            this.panelSales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).EndInit();
            this.panelOrderBreakDown.ResumeLayout(false);
            this.panelOrderBreakDown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBooks;
        private System.Windows.Forms.Panel panelCard3;
        private System.Windows.Forms.Panel panelCard2;
        private System.Windows.Forms.Panel panelCard4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Label lblBooks;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label lblCustomers;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Panel panelSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSales;
        private System.Windows.Forms.Label labelTitleSales;
        private System.Windows.Forms.Panel panelOrderBreakDown;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOrders;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTitleDashboard;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrderDate;
    }
}
