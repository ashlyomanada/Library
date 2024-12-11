namespace Library
{
    partial class librarianform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(librarianform));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dashboardBtn = new System.Windows.Forms.Button();
            this.requestedBtn = new System.Windows.Forms.Button();
            this.borrowedBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dashboard1 = new Library.dashboard();
            this.librarian_borrowed1 = new Library.librarian_borrowed();
            this.requestedBooks1 = new Library.requestedBooks();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Library.Properties.Resources.bg;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 35);
            this.panel1.TabIndex = 4;
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
            this.panel2.Controls.Add(this.dashboardBtn);
            this.panel2.Controls.Add(this.requestedBtn);
            this.panel2.Controls.Add(this.borrowedBtn);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 565);
            this.panel2.TabIndex = 5;
            // 
            // dashboardBtn
            // 
            this.dashboardBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dashboardBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dashboardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboardBtn.ForeColor = System.Drawing.Color.White;
            this.dashboardBtn.Location = new System.Drawing.Point(28, 142);
            this.dashboardBtn.Name = "dashboardBtn";
            this.dashboardBtn.Size = new System.Drawing.Size(162, 48);
            this.dashboardBtn.TabIndex = 7;
            this.dashboardBtn.Text = "Dashboard";
            this.dashboardBtn.UseVisualStyleBackColor = true;
            this.dashboardBtn.Click += new System.EventHandler(this.dashboardBtn_Click);
            // 
            // requestedBtn
            // 
            this.requestedBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.requestedBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.requestedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.requestedBtn.ForeColor = System.Drawing.Color.White;
            this.requestedBtn.Location = new System.Drawing.Point(28, 281);
            this.requestedBtn.Name = "requestedBtn";
            this.requestedBtn.Size = new System.Drawing.Size(162, 48);
            this.requestedBtn.TabIndex = 6;
            this.requestedBtn.Text = "Requested Books";
            this.requestedBtn.UseVisualStyleBackColor = true;
            this.requestedBtn.Click += new System.EventHandler(this.requestedBtn_Click);
            // 
            // borrowedBtn
            // 
            this.borrowedBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.borrowedBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.borrowedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.borrowedBtn.ForeColor = System.Drawing.Color.White;
            this.borrowedBtn.Location = new System.Drawing.Point(27, 210);
            this.borrowedBtn.Name = "borrowedBtn";
            this.borrowedBtn.Size = new System.Drawing.Size(162, 48);
            this.borrowedBtn.TabIndex = 2;
            this.borrowedBtn.Text = "Borrowed Books";
            this.borrowedBtn.UseVisualStyleBackColor = true;
            this.borrowedBtn.Click += new System.EventHandler(this.borrowedBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Welcome, Librarian!";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dashboard1);
            this.panel3.Controls.Add(this.librarian_borrowed1);
            this.panel3.Controls.Add(this.requestedBooks1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(220, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(880, 565);
            this.panel3.TabIndex = 6;
            // 
            // dashboard1
            // 
            this.dashboard1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.dashboard1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dashboard1.BackgroundImage")));
            this.dashboard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dashboard1.ForeColor = System.Drawing.Color.White;
            this.dashboard1.Location = new System.Drawing.Point(-5, 0);
            this.dashboard1.Name = "dashboard1";
            this.dashboard1.Size = new System.Drawing.Size(885, 565);
            this.dashboard1.TabIndex = 2;
            this.dashboard1.Load += new System.EventHandler(this.dashboard1_Load);
            // 
            // librarian_borrowed1
            // 
            this.librarian_borrowed1.BackColor = System.Drawing.SystemColors.Control;
            this.librarian_borrowed1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("librarian_borrowed1.BackgroundImage")));
            this.librarian_borrowed1.Location = new System.Drawing.Point(0, 0);
            this.librarian_borrowed1.Name = "librarian_borrowed1";
            this.librarian_borrowed1.Size = new System.Drawing.Size(880, 616);
            this.librarian_borrowed1.TabIndex = 1;
            // 
            // requestedBooks1
            // 
            this.requestedBooks1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("requestedBooks1.BackgroundImage")));
            this.requestedBooks1.Location = new System.Drawing.Point(0, 0);
            this.requestedBooks1.Name = "requestedBooks1";
            this.requestedBooks1.Size = new System.Drawing.Size(880, 565);
            this.requestedBooks1.TabIndex = 0;
            // 
            // librarianform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "librarianform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "librarianform";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button borrowedBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button requestedBtn;
        private requestedBooks requestedBooks1;
        private librarian_borrowed librarian_borrowed1;
        private dashboard dashboard1;
        private System.Windows.Forms.Button dashboardBtn;
    }
}