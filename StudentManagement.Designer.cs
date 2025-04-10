using System.Drawing;
using System.Windows.Forms;

namespace ProjectStudentTuitionManagement
{
    partial class StudentManagement
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
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            MaSV = new DataGridViewTextBoxColumn();
            HoTen = new DataGridViewTextBoxColumn();
            Lop = new DataGridViewTextBoxColumn();
            Khoa = new DataGridViewTextBoxColumn();
            button1 = new Button();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(190, 40);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(480, 40);
            textBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { MaSV, HoTen, Lop, Khoa });
            dataGridView1.Location = new Point(25, 120);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.Size = new Size(900, 450);
            dataGridView1.TabIndex = 2;
            // 
            // MaSV
            // 
            MaSV.FillWeight = 50.6066475F;
            MaSV.HeaderText = "Mã sinh viên";
            MaSV.MinimumWidth = 6;
            MaSV.Name = "MaSV";
            MaSV.Width = 150;
            // 
            // HoTen
            // 
            HoTen.FillWeight = 99.29669F;
            HoTen.HeaderText = "Họ và tên";
            HoTen.MinimumWidth = 6;
            HoTen.Name = "HoTen";
            HoTen.Width = 294;
            // 
            // Lop
            // 
            Lop.FillWeight = 68.45799F;
            Lop.HeaderText = "Lớp";
            Lop.MinimumWidth = 6;
            Lop.Name = "Lop";
            Lop.Width = 203;
            // 
            // Khoa
            // 
            Khoa.FillWeight = 67.73493F;
            Khoa.HeaderText = "Khoa";
            Khoa.MinimumWidth = 6;
            Khoa.Name = "Khoa";
            Khoa.Width = 201;
            // 
            // button1
            // 
            button1.Location = new Point(25, 590);
            button1.Name = "button1";
            button1.Size = new Size(200, 40);
            button1.TabIndex = 3;
            button1.Text = "Thêm sinh viên";
            button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(690, 40);
            button4.Name = "button4";
            button4.Size = new Size(120, 40);
            button4.TabIndex = 6;
            button4.Text = "Tìm kiếm";
            button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(250, 590);
            button2.Name = "button2";
            button2.Size = new Size(200, 40);
            button2.TabIndex = 7;
            button2.Text = "Xóa sinh viên";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(475, 590);
            button3.Name = "button3";
            button3.Size = new Size(200, 40);
            button3.TabIndex = 8;
            button3.Text = "Chỉnh sửa thông tin";
            button3.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(700, 590);
            button5.Name = "button5";
            button5.Size = new Size(200, 40);
            button5.TabIndex = 9;
            button5.Text = "Toàn bộ danh sách";
            button5.UseVisualStyleBackColor = true;
            // 
            // StudentManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Name = "StudentManagement";
            Size = new Size(950, 650);
            Load += StudentManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn MaSV;
        private DataGridViewTextBoxColumn HoTen;
        private DataGridViewTextBoxColumn Lop;
        private DataGridViewTextBoxColumn Khoa;
        private Button button1;
        private Button button4;
        private Button button2;
        private Button button3;
        private Button button5;
    }
}
