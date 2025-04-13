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
            string strLuu = "Insert into SinhVien values ('" + txtMaSV.Text + "',N'" + txtName.Text + "','" + cbxLop.Text + "','" + cbxKhoa.Text + "')";
            string strTK = "Insert into Users (Username, Password, FullName, Role, MaSV) values ('user" + txtMaSV.Text.Substring(txtMaSV.Text.Length - 2) + "','pass" + txtMaSV.Text.Substring(txtMaSV.Text.Length - 2) + "','" + txtName.Text + "','user','" + txtMaSV.Text +"')";
            DialogResult = MessageBox.Show("Xác nhận thêm sinh viên mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (DialogResult == DialogResult.OK)
            {
                dp.ThucThi(strLuu);
                dp.ThucThi(strTK);
                parentForm.LoadThongTinSinhVien();

                this.Close();
            }
        }
    }
}
