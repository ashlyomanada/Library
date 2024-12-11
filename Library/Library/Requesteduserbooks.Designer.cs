namespace Library
{
    partial class RequestedUserBooks
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewRequestedUserBooks = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequestedUserBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panel2.Controls.Add(this.dataGridViewRequestedUserBooks);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(38, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(807, 512);
            this.panel2.TabIndex = 6;
            // 
            // dataGridViewRequestedUserBooks
            // 
            this.dataGridViewRequestedUserBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRequestedUserBooks.BackgroundColor = System.Drawing.Color.Ivory;
            this.dataGridViewRequestedUserBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequestedUserBooks.Location = new System.Drawing.Point(23, 46);
            this.dataGridViewRequestedUserBooks.Name = "dataGridViewRequestedUserBooks";
            this.dataGridViewRequestedUserBooks.RowHeadersWidth = 51;
            this.dataGridViewRequestedUserBooks.RowTemplate.Height = 24;
            this.dataGridViewRequestedUserBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRequestedUserBooks.Size = new System.Drawing.Size(758, 446);
            this.dataGridViewRequestedUserBooks.TabIndex = 3;
            this.dataGridViewRequestedUserBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBooks_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your Requested Books";
            // 
            // RequestedUserBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Library.Properties.Resources.bg;
            this.Controls.Add(this.panel2);
            this.Name = "RequestedUserBooks";
            this.Size = new System.Drawing.Size(880, 565);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequestedUserBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewRequestedUserBooks;
        private System.Windows.Forms.Label label1;
    }
}
