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
    public partial class AddTuitionForm : Form
    {
        private TuitionManagement parentForm;
        public AddTuitionForm(TuitionManagement form)
        {
            InitializeComponent();
            parentForm = form;
        }
        DataProvider dp = new DataProvider();

        private void AddTuitionForm_Load(object sender, EventArgs e)
        {
            txtMaSV.Focus();
            HOCKI();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void HOCKI()
        {
            DataTable dt = new DataTable();
            dt = dp.Lay_DLbang("Select * from KiHoc");
            cbxHocki.DataSource = dt;
            cbxHocki.DisplayMember = "KiHocID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM SinhVien WHERE MaSV ='" + txtMaSV.Text + "'";
            dp.Doc_DL(query, reader =>
            {
                if (!reader.Read())
                {
                    txtName.Text = "";
                    txtLop.Text = "";
                    MessageBox.Show("Hãy kiểm tra lại mã sinh viên", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    txtName.Text = reader["FullName"].ToString();
                    txtLop.Text = reader["Lop"].ToString();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtHP.Text != "")
            {
                string hocPhiID = txtMaHK.Text.Trim();
                string checkQuery = $"SELECT COUNT(*) FROM HocPhi WHERE HocPhiID = '{hocPhiID}'";
                int count = Convert.ToInt32(dp.Lay_GiaTriDon(checkQuery));
                string tenKiHoc = cbxHocki.Text; 

                
                MessageBox.Show($"⚠️ Học phí kỳ học {tenKiHoc} cho sinh viên đã tồn tại!\nVui lòng chọn kỳ học khác hoặc chỉnh sửa bản ghi cũ.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (count > 0)
                {
                    MessageBox.Show($"⚠️ Học phí kỳ học {tenKiHoc} cho sinh viên đã tồn tại!\nVui lòng chọn kỳ học khác hoặc chỉnh sửa bản ghi cũ.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ngayHanDong = dtpEnd.Value.ToString("yyyy-MM-dd");
                string strLuu = "Insert into HocPhi (HocPhiID, MaSV, KiHocID, SoTien, HanDong) values ('" + txtMaHK.Text + "','" + txtMaSV.Text + "','" + cbxHocki.Text + "'," + int.Parse(txtHP.Text) + ",'" + ngayHanDong + "')";
                DialogResult = MessageBox.Show("Xác nhận thêm học phí mới cho sinh viên mã " + txtMaSV.Text +"?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (DialogResult == DialogResult.OK)
                {
                    dp.ThucThi(strLuu);
                    parentForm.LoadThongTin();
                    this.Close();
                }
            }
        }

        private void txtMaHK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
