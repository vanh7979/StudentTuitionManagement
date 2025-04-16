using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectStudentTuitionManagement;

namespace NewProject
{
    public partial class AddStudentForm : Form
    {
        private StudentManagement parentForm;
        public AddStudentForm(StudentManagement Form)
        {
            InitializeComponent();
            parentForm = Form;
        }
        DataProvider dp = new DataProvider();
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            LoadNewID();
            KHOAVIEN();
            LOP();
        }
        private void LoadNewID()
        {
            string query = "SELECT TOP 1 MaSV FROM SinhVien ORDER BY MaSV DESC";
            int newId = 11220000;
            dp.Doc_DL(query, reader =>
            {
                if (reader.Read())
                {
                    string lastId = reader["MaSV"].ToString();
                    if (int.TryParse(lastId, out int idSo))
                    {
                        newId = idSo + 1;
                    }
                }
            });
            txtMaSV.Text = newId.ToString(); 
        }

        private void KHOAVIEN()
        {
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang("Select * from KhoaVien");
            cbxKhoa.DataSource = dt;
            cbxKhoa.DisplayMember = "MAKHOA";
        }
        private void LOP()
        {
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang("Select * from Lop");
            cbxLop.DataSource = dt;
            cbxLop.DisplayMember = "MALOP";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Xác nhận thêm sinh viên mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (DialogResult == DialogResult.OK)
            {
                SqlParameter[] SV = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", txtMaSV.Text),
                    new SqlParameter("@FullName", txtName.Text),
                    new SqlParameter("@Lop", cbxLop.Text),
                    new SqlParameter("@Khoa", cbxKhoa.Text)
                };
                bool result_SV = dp.ExecuteNonQuery("sp_ThemSinhVien", SV);
                string userID = "user" + txtMaSV.Text.Substring(txtMaSV.Text.Length - 2);
                string pass = "pass" + txtMaSV.Text.Substring(txtMaSV.Text.Length - 2);
                SqlParameter[] user = new SqlParameter[]
                {
                    new SqlParameter("@Username", userID),
                    new SqlParameter("@Password", pass),
                    new SqlParameter("@FullName", txtName.Text),
                    new SqlParameter("@MaSV", txtMaSV.Text)
                };
                bool result_User = dp.ExecuteNonQuery("sp_ThemUsers", user);
                if (result_SV && result_User)
                    MessageBox.Show("Thêm thành công!");
                else
                    MessageBox.Show("Xảy ra lỗi, kiểm tra lại thông tin");
                parentForm.LoadThongTinSinhVien();
                this.Close();
            }
        }
    }
}
