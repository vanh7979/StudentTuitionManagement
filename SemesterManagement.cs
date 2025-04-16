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
using NewProject;

namespace ProjectStudentTuitionManagement
{
    public partial class SemesterManagement : UserControl
    {
        public SemesterManagement()
        {
            InitializeComponent();
        }
        DataProvider dp = new DataProvider();
        private void SemesterManagement_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        public void LoadThongTin()
        {
            string query = $"Select * from KiHoc";
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang(query);
            dvgSemester.DataSource = dt;
            dvgSemester.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgSemester.Columns["KiHocID"].HeaderText = "Mã Kì học";
            dvgSemester.Columns["TenKiHoc"].HeaderText = "Học kì";
            dvgSemester.Columns["NamHoc"].HeaderText = "Năm Học";
            dvgSemester.Columns["TGBatDau"].HeaderText = "Thời gian bắt đầu";
            dvgSemester.Columns["TGKetThuc"].HeaderText = "Thời gian kết thúc";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSemesterForm addSemesterForm = new AddSemesterForm(this);
            addSemesterForm.Show();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SemesterManagement_Load(sender, e);
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập kì học .VD:HK1 2021";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học .VD:HK1 2021")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học .VD:HK1 2021")
            {
                MessageBox.Show("Hãy nhập mã kì học vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT * FROM KiHoc WHERE TenKiHoc = '" + txtSearch.Text + "'";
                dp.Doc_DL(query, reader =>
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không tìm thấy kì học, hãy kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa học kì mã " + txtSearch.Text + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            SqlParameter[] kihoc = new SqlParameter[]
 {
                                new SqlParameter("@KiHocID", reader["KiHocID"].ToString()),
 };
                            bool result_HK = dp.ExecuteNonQuery("sp_XoaKiHoc", kihoc);
                            if (result_HK)
                                MessageBox.Show("Xóa thành công!");
                            else
                                MessageBox.Show("Xảy ra lỗi, kiểm tra lại mã sinh viên");
                            this.SemesterManagement_Load(sender, e);
                        }
                    }
                });
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học .VD:HK1 2021")
            {
                MessageBox.Show("Hãy nhập tên học kì vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT * FROM KiHoc WHERE TenKiHoc ='" + txtSearch.Text + "'";
                dp.Doc_DL(query, reader =>
                {
                    if (reader.Read())
                    {
                        ChangeSemesterInfo changeSemesterInfo = new ChangeSemesterInfo(this, txtSearch.Text);
                        changeSemesterInfo.Show();
                    }
                    else
                    {
                        MessageBox.Show("Hãy kiểm tra lại tên học kì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                });
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = $"SELECT * FROM KiHoc WHERE TenKiHoc = '" + txtSearch.Text + "'";
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang(query);
            if (dt.Rows.Count > 0)
            {
                dvgSemester.DataSource = dt;
                dvgSemester.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dvgSemester.Columns["KiHocID"].HeaderText = "Mã Kì học";
                dvgSemester.Columns["TenKiHoc"].HeaderText = "Học kì";
                dvgSemester.Columns["NamHoc"].HeaderText = "Năm Học";
                dvgSemester.Columns["TGBatDau"].HeaderText = "Thời gian bắt đầu";
                dvgSemester.Columns["TGKetThuc"].HeaderText = "Thời gian kết thúc";
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin học kì", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SemesterManagement_Load(sender, e);
            }
        }
    }
}
