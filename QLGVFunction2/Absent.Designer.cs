namespace QLGVFunction2
{
    partial class Absent
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpAbsent = new System.Windows.Forms.DateTimePicker();
            this.txbAbsentTime = new System.Windows.Forms.TextBox();
            this.dtpReschedule = new System.Windows.Forms.DateTimePicker();
            this.txbRescheduleTime = new System.Windows.Forms.TextBox();
            this.btnValid = new System.Windows.Forms.Button();
            this.pnTime = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txbCourseId = new System.Windows.Forms.TextBox();
            this.pnTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Absent date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reschedule date";
            // 
            // dtpAbsent
            // 
            this.dtpAbsent.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAbsent.Location = new System.Drawing.Point(209, 118);
            this.dtpAbsent.Name = "dtpAbsent";
            this.dtpAbsent.Size = new System.Drawing.Size(224, 22);
            this.dtpAbsent.TabIndex = 2;
            // 
            // txbAbsentTime
            // 
            this.txbAbsentTime.Location = new System.Drawing.Point(30, 3);
            this.txbAbsentTime.Name = "txbAbsentTime";
            this.txbAbsentTime.Size = new System.Drawing.Size(202, 22);
            this.txbAbsentTime.TabIndex = 3;
            // 
            // dtpReschedule
            // 
            this.dtpReschedule.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReschedule.Location = new System.Drawing.Point(209, 192);
            this.dtpReschedule.Name = "dtpReschedule";
            this.dtpReschedule.Size = new System.Drawing.Size(224, 22);
            this.dtpReschedule.TabIndex = 4;
            // 
            // txbRescheduleTime
            // 
            this.txbRescheduleTime.Location = new System.Drawing.Point(30, 74);
            this.txbRescheduleTime.Name = "txbRescheduleTime";
            this.txbRescheduleTime.Size = new System.Drawing.Size(202, 22);
            this.txbRescheduleTime.TabIndex = 5;
            // 
            // btnValid
            // 
            this.btnValid.Location = new System.Drawing.Point(616, 431);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(86, 31);
            this.btnValid.TabIndex = 6;
            this.btnValid.Text = "Xác nhận";
            this.btnValid.UseVisualStyleBackColor = true;
            this.btnValid.Click += new System.EventHandler(this.btnValid_Click);
            // 
            // pnTime
            // 
            this.pnTime.Controls.Add(this.txbAbsentTime);
            this.pnTime.Controls.Add(this.txbRescheduleTime);
            this.pnTime.Location = new System.Drawing.Point(439, 118);
            this.pnTime.Name = "pnTime";
            this.pnTime.Size = new System.Drawing.Size(329, 110);
            this.pnTime.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Mã khóa học";
            // 
            // txbCourseId
            // 
            this.txbCourseId.Location = new System.Drawing.Point(209, 38);
            this.txbCourseId.Name = "txbCourseId";
            this.txbCourseId.Size = new System.Drawing.Size(224, 22);
            this.txbCourseId.TabIndex = 9;
            // 
            // Absent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 492);
            this.Controls.Add(this.txbCourseId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnTime);
            this.Controls.Add(this.btnValid);
            this.Controls.Add(this.dtpReschedule);
            this.Controls.Add(this.dtpAbsent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Absent";
            this.Text = "Absent";
            this.pnTime.ResumeLayout(false);
            this.pnTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpAbsent;
        private System.Windows.Forms.TextBox txbAbsentTime;
        private System.Windows.Forms.DateTimePicker dtpReschedule;
        private System.Windows.Forms.TextBox txbRescheduleTime;
        private System.Windows.Forms.Button btnValid;
        private System.Windows.Forms.Panel pnTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbCourseId;
    }
}