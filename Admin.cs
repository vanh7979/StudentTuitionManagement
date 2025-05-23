﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjectStudentTuitionManagement
{
    public partial class Admin : Form
    {
        public Admin(string username)
        {
            InitializeComponent();
            label1.Text = $"Xin chào {username} ⌄";
            this.label1.MouseEnter += new EventHandler(lblAdmin_MouseEnter);
            this.label1.MouseLeave += new EventHandler(lblAdmin_MouseLeave);
        }

        private void ShowControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowControl(new StudentManagement());
        }

        private void btnSemester_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowControl(new SemesterManagement());
        }

        private void btnTuition_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowControl(new TuitionManagement());
        }

        private void btnStatisticReport_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            ShowControl(new StatisticReport());
        }

        private void lkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                Application.Restart();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pnlDropdown.Visible = !pnlDropdown.Visible;
        }
        private void HighlightButton(Button activeButton)
        {
            
            foreach (Control ctrl in panel1.Controls) 
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = SystemColors.Control; 
                    btn.Font = new Font(btn.Font, FontStyle.Regular); 
                }
            }

            
            activeButton.BackColor = Color.LightSteelBlue; 
            activeButton.Font = new Font(activeButton.Font, FontStyle.Bold); 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lblAdmin_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = Color.LightGray; 
            label1.Cursor = Cursors.Hand;       
        }

        private void lblAdmin_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = SystemColors.Control; 
        }

        private void Admin_Load_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                    "Bạn có chắc chắn muốn thoát không?",
                                    "Xác nhận thoát",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}