namespace QLGVFunction2
{
    partial class LichGiangDaycs
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAbsent = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbLocation = new System.Windows.Forms.TextBox();
            this.txbStartTime = new System.Windows.Forms.TextBox();
            this.txbCourseID = new System.Windows.Forms.TextBox();
            this.dtgvListTeaching = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListTeaching)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnAbsent);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txbLocation);
            this.panel1.Controls.Add(this.txbStartTime);
            this.panel1.Controls.Add(this.txbCourseID);
            this.panel1.Controls.Add(this.dtgvListTeaching);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 537);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnAbsent
            // 
            this.btnAbsent.Location = new System.Drawing.Point(833, 453);
            this.btnAbsent.Name = "btnAbsent";
            this.btnAbsent.Size = new System.Drawing.Size(127, 47);
            this.btnAbsent.TabIndex = 7;
            this.btnAbsent.Text = "Báo vắng + dạy bù";
            this.btnAbsent.UseVisualStyleBackColor = true;
            this.btnAbsent.Click += new System.EventHandler(this.btnAbsent_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(593, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(593, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Start time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Course ID";
            // 
            // txbLocation
            // 
            this.txbLocation.Location = new System.Drawing.Point(741, 202);
            this.txbLocation.Name = "txbLocation";
            this.txbLocation.ReadOnly = true;
            this.txbLocation.Size = new System.Drawing.Size(219, 22);
            this.txbLocation.TabIndex = 3;
            // 
            // txbStartTime
            // 
            this.txbStartTime.Location = new System.Drawing.Point(741, 114);
            this.txbStartTime.Name = "txbStartTime";
            this.txbStartTime.ReadOnly = true;
            this.txbStartTime.Size = new System.Drawing.Size(219, 22);
            this.txbStartTime.TabIndex = 2;
            // 
            // txbCourseID
            // 
            this.txbCourseID.Location = new System.Drawing.Point(741, 30);
            this.txbCourseID.Name = "txbCourseID";
            this.txbCourseID.ReadOnly = true;
            this.txbCourseID.Size = new System.Drawing.Size(219, 22);
            this.txbCourseID.TabIndex = 1;
            // 
            // dtgvListTeaching
            // 
            this.dtgvListTeaching.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvListTeaching.Location = new System.Drawing.Point(3, 3);
            this.dtgvListTeaching.Name = "dtgvListTeaching";
            this.dtgvListTeaching.RowHeadersWidth = 51;
            this.dtgvListTeaching.RowTemplate.Height = 24;
            this.dtgvListTeaching.Size = new System.Drawing.Size(554, 361);
            this.dtgvListTeaching.TabIndex = 0;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(556, 420);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 8;
            this.btnExportExcel.Text = "Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // LichGiangDaycs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 548);
            this.Controls.Add(this.panel1);
            this.Name = "LichGiangDaycs";
            this.Text = "LichGiangDaycs";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvListTeaching)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtgvListTeaching;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbLocation;
        private System.Windows.Forms.TextBox txbStartTime;
        private System.Windows.Forms.TextBox txbCourseID;
        private System.Windows.Forms.Button btnAbsent;
        private System.Windows.Forms.Button btnExportExcel;
    }
}