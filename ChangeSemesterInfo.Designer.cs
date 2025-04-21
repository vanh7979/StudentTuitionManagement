namespace NewProject
{
    partial class ChangeSemesterInfo
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
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtMaHK = new System.Windows.Forms.TextBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.lblSemester = new System.Windows.Forms.Label();
            this.txtHocKi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Location = new System.Drawing.Point(240, 365);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(261, 22);
            this.dtpEnd.TabIndex = 42;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblEnd.Location = new System.Drawing.Point(35, 363);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(84, 25);
            this.lblEnd.TabIndex = 41;
            this.lblEnd.Text = "Kết thúc";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblStart.Location = new System.Drawing.Point(36, 302);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(100, 25);
            this.lblStart.TabIndex = 40;
            this.lblStart.Text = "Bắt đầu từ";
            // 
            // dtpStart
            // 
            this.dtpStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Location = new System.Drawing.Point(240, 305);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(261, 22);
            this.dtpStart.TabIndex = 39;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtYear.Location = new System.Drawing.Point(240, 237);
            this.txtYear.Name = "txtYear";
            this.txtYear.ReadOnly = true;
            this.txtYear.Size = new System.Drawing.Size(261, 30);
            this.txtYear.TabIndex = 36;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExit.Location = new System.Drawing.Point(445, 458);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 36);
            this.btnExit.TabIndex = 35;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnChange.Location = new System.Drawing.Point(68, 458);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(221, 36);
            this.btnChange.TabIndex = 34;
            this.btnChange.Text = "Thay đổi thông tin";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtMaHK
            // 
            this.txtMaHK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaHK.Location = new System.Drawing.Point(240, 112);
            this.txtMaHK.Name = "txtMaHK";
            this.txtMaHK.ReadOnly = true;
            this.txtMaHK.Size = new System.Drawing.Size(261, 30);
            this.txtMaHK.TabIndex = 33;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblClass.Location = new System.Drawing.Point(36, 242);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(90, 25);
            this.lblClass.TabIndex = 32;
            this.lblClass.Text = "Năm học";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblName.Location = new System.Drawing.Point(36, 175);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 25);
            this.lblName.TabIndex = 31;
            this.lblName.Text = "Học kì";
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaSV.Location = new System.Drawing.Point(36, 115);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(132, 25);
            this.lblMaSV.TabIndex = 30;
            this.lblMaSV.Text = "Mã học kì mới";
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSemester.Location = new System.Drawing.Point(300, 47);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(145, 32);
            this.lblSemester.TabIndex = 29;
            this.lblSemester.Text = "Học kì mới";
            // 
            // txtHocKi
            // 
            this.txtHocKi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtHocKi.Location = new System.Drawing.Point(240, 172);
            this.txtHocKi.Name = "txtHocKi";
            this.txtHocKi.ReadOnly = true;
            this.txtHocKi.Size = new System.Drawing.Size(261, 30);
            this.txtHocKi.TabIndex = 43;
            // 
            // ChangeSemesterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 554);
            this.Controls.Add(this.txtHocKi);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtMaHK);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblMaSV);
            this.Controls.Add(this.lblSemester);
            this.Name = "ChangeSemesterInfo";
            this.Text = "ChangeSemesterInfo";
            this.Load += new System.EventHandler(this.ChangeSemesterInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtMaHK;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.TextBox txtHocKi;
    }
}