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
    public partial class AddSemesterForm : Form
    {
        private SemesterManagement parentForm;
        public AddSemesterForm(SemesterManagement form)
        {
            InitializeComponent();
            parentForm = form;
        }
        DataProvider dp = new DataProvider();
        private void AddSemesterForm_Load(object sender, EventArgs e)
        {
            LoadHocKi();
        }

        private void LoadHocKi()
        {
            cbxHocki.Items.Add("Chọn học kì");
            cbxHocki.Items.Add("Học kì 1");
            cbxHocki.Items.Add("Học kì 2");

            cbxHocki.SelectedIndex = 0;    
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xác nhận thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (cbxHocki.Text == "Chọn học kì" || txtYear.Text == "" || int.TryParse(txtYear.Text, out int number) == false)
            {
                txtMaHK.Text = "Kiểm tra lại năm học hoặc học kì";
            }
            else 
            {
                if (cbxHocki.Text != "Chọn học kì" && txtYear.Text != "")
                {
                    if (cbxHocki.Text == "Học kì 1")
                    {
                        txtMaHK.Text = "1-" + txtYear.Text;
                        dtpStart.Value = DateTime.Parse((int.Parse(txtYear.Text) - 1).ToString() + "-09-06");
                        dtpEnd.Value = DateTime.Parse((int.Parse(txtYear.Text)).ToString() + "-02-06");
                    }
                    else if (cbxHocki.Text == "Học kì 2")
                    {
                        txtMaHK.Text = "2-" + txtYear.Text;
                        dtpStart.Value = DateTime.Parse((int.Parse(txtYear.Text)).ToString() + "-03-01");
                        dtpEnd.Value = DateTime.Parse((int.Parse(txtYear.Text)).ToString() + "-06-20");
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaHK.Text != "")
            {

                string TenHocKi;
                if (cbxHocki.Text == "Học kì 1")
                {
                    TenHocKi = "HK1 " + txtYear.Text;
                }
                else
                {
                    TenHocKi = "HK2 " + txtYear.Text;
                }
                string query = $"SELECT * FROM KiHoc where TenKiHoc = '" + TenHocKi + "'";
                DataTable dt = new DataTable();
                dt = dp.Lay_DLbang(query);
                if (dt.Rows.Count == 0)
                {
                    DialogResult dr = MessageBox.Show("Xác nhận thêm kì học mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        SqlParameter[] HK = new SqlParameter[]
                        {
                            new SqlParameter("@KiHocID", txtMaHK.Text),
                            new SqlParameter("@TenKiHoc", TenHocKi),
                            new SqlParameter("@NamHoc", txtYear.Text),
                            new SqlParameter("@TGBatDau", dtpStart.Value),
                            new SqlParameter("TGKetThuc", dtpEnd.Value)
                        };
                        bool result_HK = dp.ExecuteNonQuery("sp_ThemKiHoc", HK);
                        if (result_HK)
                            MessageBox.Show("Thêm thành công!");
                        else
                            MessageBox.Show("Xảy ra lỗi, kiểm tra lại thông tin");
                        parentForm.LoadThongTin();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Học kì đã tồn tại.");
                }
            }    
        }
    }

}
