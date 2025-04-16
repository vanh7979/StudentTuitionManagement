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
                string query = $"SELECT * FROM HocPhi where HocPhiID = '" + txtMaHK.Text +"'";
                DataTable dt = new DataTable();
                dt = dp.Lay_DLbang(query);
                if (dt.Rows.Count == 0)
                {
                    DialogResult = MessageBox.Show("Xác nhận thêm học phí mới cho sinh viên mã " + txtMaSV.Text + "?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (DialogResult == DialogResult.OK)
                    {
                        SqlParameter[] HocKi = new SqlParameter[]
    {
                        new SqlParameter("@HocPhiID", txtMaHK.Text),
                        new SqlParameter("@MaSV", txtMaSV.Text),
                        new SqlParameter("@KiHocID", cbxHocki.Text),
                        new SqlParameter("@SoTien", int.Parse(txtHP.Text)),
                        new SqlParameter("@HanDong", dtpEnd.Value)
    };
                        bool result_HK = dp.ExecuteNonQuery("sp_ThemHocPhi", HocKi);
                        if (result_HK)
                            MessageBox.Show("Xóa thành công!");
                        else
                            MessageBox.Show("Xảy ra lỗi, kiểm tra lại mã học phí");
                        parentForm.LoadThongTin();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Mã học phí đã tồn tại!");
                }
            }
        }
    }
}
