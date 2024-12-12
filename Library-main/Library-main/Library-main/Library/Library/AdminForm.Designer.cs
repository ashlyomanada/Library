namespace Library
{
    partial class adminForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.locationsBtn = new System.Windows.Forms.Button();
            this.booksBtn = new System.Windows.Forms.Button();
            this.dashboardBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dashboard1 = new Library.dashboard();
            this.locationForm1 = new Library.locationForm();
            this.addBooks1 = new Library.addBooks();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Library.Properties.Resources.bg;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 35);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Library.Properties.Resources.bell;
            this.pictureBox1.Location = new System.Drawing.Point(964, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1072, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Library.Properties.Resources.bg;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.locationsBtn);
            this.panel2.Controls.Add(this.booksBtn);
            this.panel2.Controls.Add(this.dashboardBtn);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 565);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // locationsBtn
            // 
            this.locationsBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.locationsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.locationsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.locationsBtn.ForeColor = System.Drawing.Color.White;
            this.locationsBtn.Location = new System.Drawing.Point(27, 303);
            this.locationsBtn.Name = "locationsBtn";
            this.locationsBtn.Size = new System.Drawing.Size(162, 48);
            this.locationsBtn.TabIndex = 3;
            this.locationsBtn.Text = "Locations";
            this.locationsBtn.UseVisualStyleBackColor = true;
            this.locationsBtn.Click += new System.EventHandler(this.locationsBtn_Click);
            // 
            // booksBtn
            // 
            this.booksBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.booksBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.booksBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.booksBtn.ForeColor = System.Drawing.Color.White;
            this.booksBtn.Location = new System.Drawing.Point(27, 226);
            this.booksBtn.Name = "booksBtn";
            this.booksBtn.Size = new System.Drawing.Size(162, 48);
            this.booksBtn.TabIndex = 2;
            this.booksBtn.Text = "Books";
            this.booksBtn.UseVisualStyleBackColor = true;
            this.booksBtn.Click += new System.EventHandler(this.booksBtn_Click);
            // 
            // dashboardBtn
            // 
            this.dashboardBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.dashboardBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.dashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboardBtn.ForeColor = System.Drawing.Color.White;
            this.dashboardBtn.Location = new System.Drawing.Point(27, 145);
            this.dashboardBtn.Name = "dashboardBtn";
            this.dashboardBtn.Size = new System.Drawing.Size(162, 48);
            this.dashboardBtn.TabIndex = 1;
            this.dashboardBtn.Text = "Dashboard";
            this.dashboardBtn.UseVisualStyleBackColor = true;
            this.dashboardBtn.Click += new System.EventHandler(this.dashboardBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Welcome, Admin!";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dashboard1);
            this.panel3.Controls.Add(this.locationForm1);
            this.panel3.Controls.Add(this.addBooks1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(220, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(880, 565);
            this.panel3.TabIndex = 2;
            // 
            // dashboard1
            // 
            this.dashboard1.BackColor = System.Drawing.Color.Transparent;
            this.dashboard1.BackgroundImage = global::Library.Properties.Resources.bg;
            this.dashboard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dashboard1.ForeColor = System.Drawing.Color.White;
            this.dashboard1.Location = new System.Drawing.Point(-3, -2);
            this.dashboard1.Name = "dashboard1";
            this.dashboard1.Size = new System.Drawing.Size(883, 563);
            this.dashboard1.TabIndex = 2;
            this.dashboard1.Load += new System.EventHandler(this.dashboard1_Load);
            // 
            // locationForm1
            // 
            this.locationForm1.BackColor = System.Drawing.Color.Transparent;
            this.locationForm1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("locationForm1.BackgroundImage")));
            this.locationForm1.Location = new System.Drawing.Point(0, 0);
            this.locationForm1.Name = "locationForm1";
            this.locationForm1.Size = new System.Drawing.Size(880, 565);
            this.locationForm1.TabIndex = 1;
            // 
            // addBooks1
            // 
            this.addBooks1.BackColor = System.Drawing.Color.Transparent;
            this.addBooks1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("addBooks1.BackgroundImage")));
            this.addBooks1.Location = new System.Drawing.Point(-3, -7);
            this.addBooks1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addBooks1.Name = "addBooks1";
            this.addBooks1.Size = new System.Drawing.Size(880, 565);
            this.addBooks1.TabIndex = 0;
            // 
            // adminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "adminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminForm";
            this.Load += new System.EventHandler(this.adminForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dashboardBtn;
        private System.Windows.Forms.Button locationsBtn;
        private System.Windows.Forms.Button booksBtn;
        private System.Windows.Forms.Panel panel3;
        private addBooks addBooks1;
        private locationForm locationForm1;
        private dashboard dashboard1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}