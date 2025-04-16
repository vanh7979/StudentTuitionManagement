using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStudentTuitionManagement
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        DataProvider dp = new DataProvider();
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
        private void LoginForm_Load_1(object sender, EventArgs e)
        {
            if (NewProject.Properties.Settings.Default.RememberMe)
            {
                checkBox1.Checked = true;
                textBox1.Text = NewProject.Properties.Settings.Default.SavedUsername;
                textBox2.Text = NewProject.Properties.Settings.Default.SavedPassword;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();



            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBox1.Checked)
            {
                NewProject.Properties.Settings.Default.RememberMe = true;
                NewProject.Properties.Settings.Default.SavedUsername = textBox1.Text;
                NewProject.Properties.Settings.Default.SavedPassword = textBox2.Text;
            }
            else
            {
                NewProject.Properties.Settings.Default.RememberMe = false;
                NewProject.Properties.Settings.Default.SavedUsername = "";
                NewProject.Properties.Settings.Default.SavedPassword = "";
            }
            NewProject.Properties.Settings.Default.Save();


            string query1 = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
            string query2 = $"SELECT * FROM Admin WHERE Username = '{username}' AND Password = '{password}'";

            DataTable dta1 = dp.Lay_DLbang(query1);

            if (dta1 != null && dta1.Rows.Count > 0)
            {

                string role = dta1.Rows[0]["Role"].ToString();

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                string maSV = dta1.Rows[0]["MaSV"].ToString();
                User userForm = new User(maSV);
                userForm.ShowDialog();

                this.Close();
            }
            else
            {
                DataTable dta2 = dp.Lay_DLbang(query2);
                if (dta2 != null && dta2.Rows.Count > 0)
                {
                    string role = dta2.Rows[0]["Role"].ToString();

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Admin adminForm = new Admin();
                    adminForm.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để đặt lại mật khẩu.", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    }

    


