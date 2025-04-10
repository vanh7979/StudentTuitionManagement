using System.Drawing;
using System.Windows.Forms;

namespace ProjectStudentTuitionManagement
{
    partial class SemesterManagement
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
            button5 = new Button();
            button3 = new Button();
            button2 = new Button();
            button4 = new Button();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            IDKiHoc = new DataGridViewTextBoxColumn();
            TenKiHoc = new DataGridViewTextBoxColumn();
            NamHoc = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button5
            // 
            button5.Location = new Point(700, 580);
            button5.Name = "button5";
            button5.Size = new Size(200, 40);
            button5.TabIndex = 16;
            button5.Text = "Toàn bộ kì học";
            button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(475, 580);
            button3.Name = "button3";
            button3.Size = new Size(200, 40);
            button3.TabIndex = 15;
            button3.Text = "Chỉnh sửa kì học";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(250, 580);
            button2.Name = "button2";
            button2.Size = new Size(200, 40);
            button2.TabIndex = 14;
            button2.Text = "Xóa kì học";
            button2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(690, 40);
            button4.Name = "button4";
            button4.Size = new Size(120, 40);
            button4.TabIndex = 13;
            button4.Text = "Tìm kiếm";
            button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(25, 580);
            button1.Name = "button1";
            button1.Size = new Size(200, 40);
            button1.TabIndex = 12;
            button1.Text = "Thêm kì học";
            button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { IDKiHoc, TenKiHoc, NamHoc });
            dataGridView1.Location = new Point(25, 110);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.Size = new Size(900, 450);
            dataGridView1.TabIndex = 11;
            // 
            // IDKiHoc
            // 
            IDKiHoc.HeaderText = "ID kì học";
            IDKiHoc.MinimumWidth = 6;
            IDKiHoc.Name = "IDKiHoc";
            IDKiHoc.Width = 283;
            // 
            // TenKiHoc
            // 
            TenKiHoc.HeaderText = "Tên kì học";
            TenKiHoc.MinimumWidth = 6;
            TenKiHoc.Name = "TenKiHoc";
            TenKiHoc.Width = 282;
            // 
            // NamHoc
            // 
            NamHoc.HeaderText = "Năm học";
            NamHoc.MinimumWidth = 6;
            NamHoc.Name = "NamHoc";
            NamHoc.Width = 283;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(190, 40);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            
            textBox1.Size = new Size(480, 40);
            textBox1.TabIndex = 10;
            // 
            // SemesterManagement
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
            Name = "SemesterManagement";
            Size = new Size(950, 650);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button5;
        private Button button3;
        private Button button2;
        private Button button4;
        private Button button1;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn IDKiHoc;
        private DataGridViewTextBoxColumn TenKiHoc;
        private DataGridViewTextBoxColumn NamHoc;
    }
}
