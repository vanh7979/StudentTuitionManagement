namespace NewProject
{
    partial class ChangeStudentInfo
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.cbxKhoa = new System.Windows.Forms.ComboBox();
            this.cbxLop = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.lblSV = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExit.Location = new System.Drawing.Point(459, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 36);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnChange.Location = new System.Drawing.Point(99, 356);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(238, 36);
            this.btnChange.TabIndex = 20;
            this.btnChange.Text = "Thay đổi thông tin";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // cbxKhoa
            // 
            this.cbxKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxKhoa.FormattingEnabled = true;
            this.cbxKhoa.Location = new System.Drawing.Point(245, 280);
            this.cbxKhoa.Name = "cbxKhoa";
            this.cbxKhoa.Size = new System.Drawing.Size(415, 33);
            this.cbxKhoa.TabIndex = 19;
            // 
            // cbxLop
            // 
            this.cbxLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxLop.FormattingEnabled = true;
            this.cbxLop.Location = new System.Drawing.Point(245, 222);
            this.cbxLop.Name = "cbxLop";
            this.cbxLop.Size = new System.Drawing.Size(415, 33);
            this.cbxLop.TabIndex = 18;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtName.Location = new System.Drawing.Point(245, 155);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(415, 30);
            this.txtName.TabIndex = 17;
            // 
            // txtMaSV
            // 
            this.txtMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaSV.Location = new System.Drawing.Point(245, 95);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.ReadOnly = true;
            this.txtMaSV.Size = new System.Drawing.Size(415, 30);
            this.txtMaSV.TabIndex = 16;
            // 
            // lblKhoa
            // 
            this.lblKhoa.AutoSize = true;
            this.lblKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKhoa.Location = new System.Drawing.Point(41, 283);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(105, 25);
            this.lblKhoa.TabIndex = 15;
            this.lblKhoa.Text = "Khoa/Viện";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblClass.Location = new System.Drawing.Point(41, 225);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(174, 25);
            this.lblClass.TabIndex = 14;
            this.lblClass.Text = "Lớp chuyên ngành";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblName.Location = new System.Drawing.Point(41, 158);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(95, 25);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Họ và tên";
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaSV.Location = new System.Drawing.Point(41, 98);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(126, 25);
            this.lblMaSV.TabIndex = 12;
            this.lblMaSV.Text = "Mã Sinh viên";
            // 
            // lblSV
            // 
            this.lblSV.AutoSize = true;
            this.lblSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSV.Location = new System.Drawing.Point(203, 34);
            this.lblSV.Name = "lblSV";
            this.lblSV.Size = new System.Drawing.Size(384, 32);
            this.lblSV.TabIndex = 11;
            this.lblSV.Text = "Chỉnh sửa thông tin Sinh viên";
            // 
            // ChangeStudentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.cbxKhoa);
            this.Controls.Add(this.cbxLop);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMaSV);
            this.Controls.Add(this.lblKhoa);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblMaSV);
            this.Controls.Add(this.lblSV);
            this.Name = "ChangeStudentInfo";
            this.Text = "ChangeStudentInfo";
            this.Load += new System.EventHandler(this.ChangeStudentInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.ComboBox cbxKhoa;
        private System.Windows.Forms.ComboBox cbxLop;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.Label lblSV;
    }
}