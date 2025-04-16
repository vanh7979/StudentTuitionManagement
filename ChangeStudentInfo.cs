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
    public partial class ChangeStudentInfo : Form
    {
        private StudentManagement parentForm;

        private string MaSVien;
        public ChangeStudentInfo(StudentManagement Form, string MaSV)
        {
            InitializeComponent();
            parentForm = Form;
            MaSVien = MaSV;
        }
        DataProvider dp = new DataProvider();

        private void ChangeStudentInfo_Load(object sender, EventArgs e)
        {
            KHOAVIEN();
            LOP();
            LoadStudenInfo();
        }

        private void LoadStudenInfo()
        {
            string query = "SELECT * FROM SinhVien WHERE MaSV ='" + MaSVien + "'";
            dp.Doc_DL(query, reader =>
            {
                if (reader.Read())
                {
                    txtMaSV.Text = reader["MaSV"].ToString();
                    txtName.Text = reader["FullName"].ToString();
                    cbxLop.Text = reader["Lop"].ToString();
                    cbxKhoa.Text = reader["Khoa"].ToString();
                }
            });
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

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thay đổi thông tin sinh viên ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                SqlParameter[] SV = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", txtMaSV.Text),
                    new SqlParameter("@FullName", txtName.Text),
                    new SqlParameter("@Lop", cbxLop.Text),
                    new SqlParameter("@Khoa", cbxKhoa.Text)
                };
                bool result_SV = dp.ExecuteNonQuery("sp_SuaSinhVien", SV);
                if (result_SV)
                {
                    MessageBox.Show("Thành công");
                    parentForm.LoadThongTinSinhVien();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi.");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
