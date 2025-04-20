using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectStudentTuitionManagement;

namespace NewProject
{
    public partial class ChangeTuitionInfo : Form
    {
        private TuitionManagement parentForm;
        private string MaHP;
        public ChangeTuitionInfo(TuitionManagement form, string ma)
        {
            InitializeComponent();
            parentForm = form;
            MaHP = ma;
        }
        DataProvider dp = new DataProvider();
        private void ChangeTuitionInfo_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }
        private void LoadThongTin()
        {
            string query = "SELECT * FROM HocPhi WHERE HocPhiID ='" + MaHP + "'";
            dp.Doc_DL(query, reader =>
            {
                if (reader.Read())
                {
                    txtMaHK.Text = reader["HocPhiID"].ToString();
                    txtMaSV.Text = reader["MaSV"].ToString();
                    cbxHocki.Text = reader["KiHocID"].ToString();
                    txtHP.Text = reader["SoTien"].ToString();
                    dtpEnd.Value = DateTime.Parse(reader["HanDong"].ToString());
                }
            });
            string queryLop = "SELECT * FROM SinhVien WHERE MaSV ='" + txtMaSV.Text + "'";
            dp.Doc_DL(queryLop, new_reader =>
            {
                if (new_reader.Read())
                {
                    txtName.Text = new_reader["FullName"].ToString();
                    txtLop.Text = new_reader["Lop"].ToString();
                }
            });
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Hãy kiểm tra lại mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "SELECT SOTIEN1TIN FROM TinChi WHERE MALOP ='" + txtLop.Text + "'";
                int HP = 0;
                dp.Doc_DL(query, reader =>
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Hãy kiểm tra lại mã", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (int.TryParse(txtTin.Text, out int number))
                        {
                            HP = int.Parse(txtTin.Text) * int.Parse(reader["SOTIEN1TIN"].ToString());
                            txtMaHK.Text = txtMaSV.Text + "-" + cbxHocki.Text;
                            txtHP.Text = HP.ToString();
                            string Han = cbxHocki.Text;
                            string[] HK = Han.Split('-');
                            if (HK[0] == "1")
                            {
                                dtpEnd.Value = DateTime.Parse((int.Parse(HK[1])).ToString() + "-01-31");
                            }
                            else
                            {
                                dtpEnd.Value = DateTime.Parse((int.Parse(HK[1])).ToString() + "-05-30");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hãy kiểm tra lại số tín chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                });
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHK.Text) ||
                string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(cbxHocki.Text) ||
                string.IsNullOrWhiteSpace(txtHP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin học phí!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Xác nhận cập nhật học phí cho sinh viên " + txtMaSV.Text + "?",
                                                  "Cập nhật học phí", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                string hocPhiID = txtMaHK.Text.Trim();
                string maSV = txtMaSV.Text.Trim();
                string kiHocID = cbxHocki.Text.Trim();
                string soTien = txtHP.Text.Trim();
                string hanDong = dtpEnd.Value.ToString("yyyy-MM-dd");

                string query = $"UPDATE HocPhi SET MaSV = N'{maSV}', KiHocID = N'{kiHocID}', SoTien = {soTien}, HanDong = '{hanDong}' WHERE HocPhiID = N'{hocPhiID}'";

                dp.ThucThi(query);

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                parentForm.LoadThongTin(); // load lại danh sách
                this.Close(); // đóng form
            }
        }
    }
}
