using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
            txtYear.Focus();
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
                string checkQuery = $"SELECT COUNT(*) FROM KiHoc WHERE KiHocID = '{txtMaHK.Text}'";
                object result = dp.Lay_GiaTriDon(checkQuery);
                int count = Convert.ToInt32(result);

                if (count > 0)
                {
                    MessageBox.Show($"⚠️ Học kỳ '{txtMaHK.Text}' đã tồn tại!\nVui lòng chọn năm học khác hoặc học kỳ khác.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ngayBatDau = dtpStart.Value.ToString("yyyy-MM-dd");
                string ngayKetThuc = dtpEnd.Value.ToString("yyyy-MM-dd");
                string strLuu = "Insert into KiHoc values ('" + txtMaHK.Text + "','" + TenHocKi + "'," + txtYear.Text + ",'" + ngayBatDau + "','" + ngayKetThuc + "')";
                DialogResult = MessageBox.Show("Xác nhận thêm kì học mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (DialogResult == DialogResult.OK)
                {
                    dp.ThucThi(strLuu);
                    parentForm.LoadThongTin();
                    this.Close();
                }
            }    
        }
    }

}
