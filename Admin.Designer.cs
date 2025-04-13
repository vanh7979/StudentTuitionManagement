using System.Drawing;
using System.Windows.Forms;

namespace ProjectStudentTuitionManagement
{
    partial class Admin
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnStatisticReport = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTuition = new System.Windows.Forms.Button();
            this.btnSemester = new System.Windows.Forms.Button();
            this.btnStudent = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lkLogout = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lkLogout);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.btnStatisticReport);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnTuition);
            this.panel1.Controls.Add(this.btnSemester);
            this.panel1.Controls.Add(this.btnStudent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 522);
            this.panel1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textBox2.Location = new System.Drawing.Point(45, 154);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(120, 33);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Admin";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStatisticReport
            // 
            this.btnStatisticReport.FlatAppearance.BorderSize = 0;
            this.btnStatisticReport.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStatisticReport.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStatisticReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatisticReport.Location = new System.Drawing.Point(0, 400);
            this.btnStatisticReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStatisticReport.Name = "btnStatisticReport";
            this.btnStatisticReport.Size = new System.Drawing.Size(200, 48);
            this.btnStatisticReport.TabIndex = 2;
            this.btnStatisticReport.Text = "Thống kê, báo cáo";
            this.btnStatisticReport.UseVisualStyleBackColor = true;
            this.btnStatisticReport.Click += new System.EventHandler(this.btnStatisticReport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NewProject.Properties.Resources.logo_neu_inkythuatso_01_09_10_41_01;
            this.pictureBox1.Location = new System.Drawing.Point(35, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 142);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnTuition
            // 
            this.btnTuition.FlatAppearance.BorderSize = 0;
            this.btnTuition.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTuition.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTuition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTuition.Location = new System.Drawing.Point(0, 344);
            this.btnTuition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTuition.Name = "btnTuition";
            this.btnTuition.Size = new System.Drawing.Size(200, 48);
            this.btnTuition.TabIndex = 1;
            this.btnTuition.Text = "Quản lý học phí";
            this.btnTuition.UseVisualStyleBackColor = true;
            this.btnTuition.Click += new System.EventHandler(this.btnTuition_Click);
            // 
            // btnSemester
            // 
            this.btnSemester.FlatAppearance.BorderSize = 0;
            this.btnSemester.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSemester.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSemester.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSemester.Location = new System.Drawing.Point(0, 288);
            this.btnSemester.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSemester.Name = "btnSemester";
            this.btnSemester.Size = new System.Drawing.Size(200, 48);
            this.btnSemester.TabIndex = 1;
            this.btnSemester.Text = "Quản lý học kì";
            this.btnSemester.UseVisualStyleBackColor = true;
            this.btnSemester.Click += new System.EventHandler(this.btnSemester_Click);
            // 
            // btnStudent
            // 
            this.btnStudent.FlatAppearance.BorderSize = 0;
            this.btnStudent.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStudent.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStudent.Location = new System.Drawing.Point(0, 232);
            this.btnStudent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(200, 48);
            this.btnStudent.TabIndex = 0;
            this.btnStudent.Text = "Quản lý sinh viên";
            this.btnStudent.UseVisualStyleBackColor = true;
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(210, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(950, 520);
            this.panelMain.TabIndex = 1;
            // 
            // lkLogout
            // 
            this.lkLogout.AutoSize = true;
            this.lkLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lkLogout.Location = new System.Drawing.Point(52, 471);
            this.lkLogout.Name = "lkLogout";
            this.lkLogout.Size = new System.Drawing.Size(101, 25);
            this.lkLogout.TabIndex = 5;
            this.lkLogout.TabStop = true;
            this.lkLogout.Text = "Đăng xuất";
            this.lkLogout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkLogout_LinkClicked);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 522);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button btnStudent;
        private Button btnTuition;
        private Button btnSemester;
        private PictureBox pictureBox1;
        private Panel panelMain;
        private Button btnStatisticReport;
        private TextBox textBox2;
        private LinkLabel lkLogout;
    }
}