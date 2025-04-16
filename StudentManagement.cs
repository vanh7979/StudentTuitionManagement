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
            txtSearch.Focus();
            
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
            if (dvgThongTinSV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSV = dvgThongTinSV.CurrentRow.Cells["MaSV"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa sinh viên mã " + maSV + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string query = $"DELETE FROM SinhVien WHERE MaSV = '{maSV}'";
                    dp.ThucThi(query);

                    MessageBox.Show("✅ Đã xóa sinh viên và các dữ liệu liên quan (nếu có)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StudentManagement_Load(sender, e); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi xóa sinh viên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dvgThongTinSV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa!");
                return;
            }

            
            string maSV = dvgThongTinSV.CurrentRow.Cells["MaSV"].Value.ToString();

            
            string query = $"SELECT * FROM SinhVien WHERE MaSV = '{maSV}'";
            dp.Doc_DL(query, reader =>
            {
                if (reader.Read())
                {
                    ChangeStudentInfo changeStudentForm = new ChangeStudentInfo(this, maSV);
                    changeStudentForm.Show();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.StudentManagement_Load(sender, e);
        }
    }
}

