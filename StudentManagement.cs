using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewProject;

namespace ProjectStudentTuitionManagement
{
    public partial class StudentManagement : UserControl
    {
        public StudentManagement()
        {
            InitializeComponent();
        }
        DataProvider dp = new DataProvider();
        private void StudentManagement_Load(object sender, EventArgs e)
        {
            LoadThongTinSinhVien();
        }

        public void LoadThongTinSinhVien()
        {
            string query = $"SELECT * FROM SinhVien";
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang(query);
            dvgThongTinSV.DataSource = dt;
            dvgThongTinSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgThongTinSV.Columns["MaSV"].HeaderText = "Mã Sinh viên";
            dvgThongTinSV.Columns["FullName"].HeaderText = "Họ và tên";
            dvgThongTinSV.Columns["Lop"].HeaderText = "Lớp chuyên ngành";
            dvgThongTinSV.Columns["Khoa"].HeaderText = "Khoa/Viện";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm(this);
            addStudentForm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập mã sinh viên")
            {
                MessageBox.Show("Hãy nhập mã sinh viên vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa sinh viên mã " + txtSearch.Text + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string query = $"DELETE FROM SinhVien where MaSV = '" + txtSearch.Text + "'";
                    dp.ThucThi(query);
                    this.StudentManagement_Load(sender, e);
                }    
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập mã sinh viên";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập mã sinh viên")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM SinhVien WHERE MaSV = '" + txtSearch.Text + "'";
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang(query);
            if (dt.Rows.Count > 0)
            {
                dvgThongTinSV.DataSource = dt;
                dvgThongTinSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dvgThongTinSV.Columns["MaSV"].HeaderText = "Mã Sinh viên";
                dvgThongTinSV.Columns["FullName"].HeaderText = "Họ và tên";
                dvgThongTinSV.Columns["Lop"].HeaderText = "Lớp chuyên ngành";
                dvgThongTinSV.Columns["Khoa"].HeaderText = "Khoa/Viện";
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.StudentManagement_Load(sender, e);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập mã sinh viên")
            {
                MessageBox.Show("Hãy nhập mã sinh viên vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT * FROM SinhVien WHERE MaSV ='" + txtSearch.Text + "'";
                dp.Doc_DL(query, reader =>
                {
                    if (reader.Read())
                    {
                        ChangeStudentInfo changeTuitionInfo = new ChangeStudentInfo(this, txtSearch.Text);
                        changeTuitionInfo.Show();
                    }
                    else
                    {
                        MessageBox.Show("Hãy kiểm tra lại mã sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                });
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.StudentManagement_Load(sender, e);
        }
    }
}

