using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            
        }

        

        

        private void LoginForm_Load_1(object sender, EventArgs e)
        {
            if (NewProject.Properties.Settings.Default.RememberMe)
            {
                checkBox1.Checked = true;
                textBox1.Text = NewProject.Properties.Settings.Default.SavedUsername;
                textBox2.Text = NewProject.Properties.Settings.Default.SavedPassword;
            }
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false; 
            this.MinimizeBox = false; 
            this.ControlBox = false;  
            this.StartPosition = FormStartPosition.CenterScreen;

            this.AcceptButton = button1;

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


            string query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";

            DataTable dta = dp.Lay_DLbang(query);

            if (dta != null && dta.Rows.Count > 0)
            {

                string role = dta.Rows[0]["Role"].ToString();

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();


                if (role == "admin")
                {
                    Admin adminForm = new Admin(username);
                    adminForm.ShowDialog();
                }
                else
                {
                    string maSV = dta.Rows[0]["MaSV"].ToString();
                    User userForm = new User(maSV);
                    userForm.ShowDialog();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button2_Click(object sender, EventArgs e)
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

    


