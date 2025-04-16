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
using System.Xml.Linq;
using ProjectStudentTuitionManagement;

namespace NewProject
{
    public partial class ChangeSemesterInfo : Form
    {
        private SemesterManagement parentForm;
        private string MaHki;
        public ChangeSemesterInfo(SemesterManagement form, string MaHK)
        {
            InitializeComponent();
            parentForm = form;
            MaHki = MaHK;
        }
        DataProvider dp = new DataProvider();

        private void ChangeSemesterInfo_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void LoadInfo()
        {
            string query = "SELECT * FROM KiHoc WHERE TenKiHoc = '" + MaHki + "'";
            dp.Doc_DL(query, reader =>
            {
                if (!reader.Read()) 
                { 
                    DialogResult dr = MessageBox.Show("Không tìm thấy kì học, bạn có muốn thêm mới không ?","Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        AddSemesterForm addSemesterForm = new AddSemesterForm(parentForm);
                        addSemesterForm.Show();
                    }
                    else { this.Close(); }
                }
                else
                {
                    txtMaHK.Text = reader["KiHocID"].ToString();
                    string Mahk = reader["TenKiHoc"].ToString();
                    string[] HK = Mahk.Split(' ');
                    if (HK[0].ToString() == "HK1")
                    {
                        txtHocKi.Text = "Học kì 1";
                    }
                    else
                    {
                        txtHocKi.Text = "Học kì 2";
                    }
                    txtYear.Text = reader["NamHoc"].ToString();
                    dtpStart.Value = DateTime.Parse(reader["TGBatDau"].ToString());
                    dtpEnd.Value = DateTime.Parse(reader["TGKetThuc"].ToString());
                }
            });
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thay đổi thông tin kì học ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                SqlParameter[] HK = new SqlParameter[]
{
                    new SqlParameter("@KiHocID", txtMaHK.Text),
                    new SqlParameter("@TGBatDau", dtpStart.Value),
                    new SqlParameter("@TGKetThuc", dtpEnd.Value)
};
                bool result_HK = dp.ExecuteNonQuery("sp_SuaKiHoc", HK);
                if (result_HK)
                {
                    MessageBox.Show("Thay đổi thành công!");
                    parentForm.LoadThongTin();
                    this.Close();
                }
                else
                    MessageBox.Show("Xảy ra lỗi, kiểm tra lại mã học phí");
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
