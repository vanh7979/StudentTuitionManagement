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
    public partial class TuitionManagement : UserControl
    {
        public TuitionManagement()
        {
            InitializeComponent();
        }
        DataProvider dp = new DataProvider();
        public void LoadThongTin()
        {
            string query = $"SELECT * FROM HocPhi";
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang(query);
            dvgThongTin.DataSource = dt;
            dvgThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvgThongTin.Columns["HocPhiID"].HeaderText = "Mã Học Phí";
            dvgThongTin.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            dvgThongTin.Columns["KiHocID"].HeaderText = "Kì học";
            dvgThongTin.Columns["SoTien"].HeaderText = "Số tiền";
            dvgThongTin.Columns["HanDong"].HeaderText = "Hạn đóng";
            dvgThongTin.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void TuitionManagement_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học hoặc mã sinh viên")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập kì học hoặc mã sinh viên";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string queryMaHK = $"SELECT * FROM HocPhi WHERE KiHocID = '" + txtSearch.Text + "'";
            DataTable dtMaHK = new DataTable();
            dtMaHK = dp.Lay_DLbang(queryMaHK);
            if (dtMaHK.Rows.Count > 0)
            {
                dvgThongTin.DataSource = dtMaHK;
                dvgThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dvgThongTin.Columns["HocPhiID"].HeaderText = "Mã Học Phí";
                dvgThongTin.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
                dvgThongTin.Columns["KiHocID"].HeaderText = "Kì học";
                dvgThongTin.Columns["SoTien"].HeaderText = "Số tiền";
                dvgThongTin.Columns["HanDong"].HeaderText = "Hạn đóng";
                dvgThongTin.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            else
            {
                string querySV = $"SELECT * FROM HocPhi WHERE MaSV = '" + txtSearch.Text + "'";
                DataTable dtSV = new DataTable();
                dtSV = dp.Lay_DLbang(querySV);
                if (dtSV.Rows.Count > 0)
                {
                    dvgThongTin.DataSource = dtSV;
                    dvgThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dvgThongTin.Columns["HocPhiID"].HeaderText = "Mã Học Phí";
                    dvgThongTin.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
                    dvgThongTin.Columns["KiHocID"].HeaderText = "Kì học";
                    dvgThongTin.Columns["SoTien"].HeaderText = "Số tiền";
                    dvgThongTin.Columns["HanDong"].HeaderText = "Hạn đóng";
                    dvgThongTin.Columns["TrangThai"].HeaderText = "Trạng Thái";
                }
                else
                {
                    string queryMHP = $"SELECT * FROM HocPhi WHERE HocPhiID = '" + txtSearch.Text + "'";
                    DataTable dtHP = new DataTable();
                    dtHP = dp.Lay_DLbang(querySV);
                    if (dtHP.Rows.Count > 0)
                    {
                        dvgThongTin.DataSource = dtHP;
                        dvgThongTin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dvgThongTin.Columns["HocPhiID"].HeaderText = "Mã Học Phí";
                        dvgThongTin.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
                        dvgThongTin.Columns["KiHocID"].HeaderText = "Kì học";
                        dvgThongTin.Columns["SoTien"].HeaderText = "Số tiền";
                        dvgThongTin.Columns["HanDong"].HeaderText = "Hạn đóng";
                        dvgThongTin.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin, hãy kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.TuitionManagement_Load(sender, e);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTuitionForm addTuitionForm = new AddTuitionForm(this);
            addTuitionForm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học hoặc mã sinh viên")
            {
                MessageBox.Show("Hãy nhập thông tin tìm kiếm vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa học phí cho sinh viên không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    string query = $"DELETE FROM HocPhi where HocPhiID = '" + txtSearch.Text + "'";
                    dp.ThucThi(query);
                    this.TuitionManagement_Load(sender, e);
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.TuitionManagement_Load(sender,e);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập kì học hoặc mã sinh viên")
            {
                MessageBox.Show("Hãy nhập mã học phí vào ô tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "SELECT * FROM HocPhi WHERE HocPhiID ='" + txtSearch.Text + "'";
                dp.Doc_DL(query, reader =>
                {
                    if (reader.Read())
                    {
                        ChangeTuitionInfo changeTuitionInfo = new ChangeTuitionInfo(this, txtSearch.Text);
                        changeTuitionInfo.Show();
                    }
                    else
                    {
                        MessageBox.Show("Hãy kiểm tra lại mã học phí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                });
            }
        }
    }
}
