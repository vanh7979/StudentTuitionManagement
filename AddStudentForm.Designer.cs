namespace NewProject
{
    partial class AddStudentForm
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
            this.lblSV = new System.Windows.Forms.Label();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbxLop = new System.Windows.Forms.ComboBox();
            this.cbxKhoa = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSV
            // 
            this.lblSV.AutoSize = true;
            this.lblSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSV.Location = new System.Drawing.Point(267, 45);
            this.lblSV.Name = "lblSV";
            this.lblSV.Size = new System.Drawing.Size(258, 32);
            this.lblSV.TabIndex = 0;
            this.lblSV.Text = "Thông tin Sinh viên";
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaSV.Location = new System.Drawing.Point(39, 105);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(126, 25);
            this.lblMaSV.TabIndex = 1;
            this.lblMaSV.Text = "Mã Sinh viên";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblName.Location = new System.Drawing.Point(39, 165);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(95, 25);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Họ và tên";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblClass.Location = new System.Drawing.Point(39, 232);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(174, 25);
            this.lblClass.TabIndex = 3;
            this.lblClass.Text = "Lớp chuyên ngành";
            // 
            // lblKhoa
            // 
            this.lblKhoa.AutoSize = true;
            this.lblKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKhoa.Location = new System.Drawing.Point(39, 290);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(105, 25);
            this.lblKhoa.TabIndex = 4;
            this.lblKhoa.Text = "Khoa/Viện";
            // 
            // txtMaSV
            // 
            this.txtMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaSV.Location = new System.Drawing.Point(243, 102);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.ReadOnly = true;
            this.txtMaSV.Size = new System.Drawing.Size(415, 30);
            this.txtMaSV.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtName.Location = new System.Drawing.Point(243, 162);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(415, 30);
            this.txtName.TabIndex = 6;
            // 
            // cbxLop
            // 
            this.cbxLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxLop.FormattingEnabled = true;
            this.cbxLop.Location = new System.Drawing.Point(243, 229);
            this.cbxLop.Name = "cbxLop";
            this.cbxLop.Size = new System.Drawing.Size(415, 33);
            this.cbxLop.TabIndex = 7;
            // 
            // cbxKhoa
            // 
            this.cbxKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbxKhoa.FormattingEnabled = true;
            this.cbxKhoa.Location = new System.Drawing.Point(243, 287);
            this.cbxKhoa.Name = "cbxKhoa";
            this.cbxKhoa.Size = new System.Drawing.Size(415, 33);
            this.cbxKhoa.TabIndex = 8;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnAdd.Location = new System.Drawing.Point(154, 363);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(181, 36);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Thêm sinh viên";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExit.Location = new System.Drawing.Point(457, 363);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 36);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AddStudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbxKhoa);
            this.Controls.Add(this.cbxLop);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMaSV);
            this.Controls.Add(this.lblKhoa);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblMaSV);
            this.Controls.Add(this.lblSV);
            this.Name = "AddStudentForm";
            this.Text = "AddStudentForm";
            this.Load += new System.EventHandler(this.AddStudentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSV;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.TextBox txtMaSV;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbxLop;
        private System.Windows.Forms.ComboBox cbxKhoa;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
    }
}