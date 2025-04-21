using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjectStudentTuitionManagement
{
    public partial class User : Form
    {
        private string maSV;
        DataProvider dp = new DataProvider();
        public User(string maSV)
        {
            InitializeComponent();
            this.maSV = maSV;

            label1.MouseEnter += label1_MouseEnter;
            label1.MouseLeave += label1_MouseLeave;
            label1.Click += label1_Click;

            


        }
        private void LoadThongTinSinhVien()
        {
            string query = $"SELECT * FROM v_ThongTinSinhVien WHERE MaSV = '{maSV}'";
            DataTable dt = dp.Lay_DLbang(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                string ma = dt.Rows[0]["MaSV"].ToString();
                string tenLop = dt.Rows[0]["TENLOP"].ToString();
                string tenKhoa = dt.Rows[0]["TENKHOA"].ToString();
                string ten = dt.Rows[0]["HoTen"].ToString();

                label2.Text = ma;
                label4.Text = tenLop;
                label5.Text = tenKhoa;

                label1.Text = $"Xin chào {ten} ⌄";
            }

        }

        public void HOCPHI()
        {
            string query = $"EXEC sp_LayHocPhiTheoMaSV N'{maSV}'";
            DataTable dt = dp.Lay_DLbang(query);

            if (dt != null)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["TongTien"].HeaderText = "Tổng tiền";
                dataGridView1.Columns["SoTienConNo"].HeaderText = "Còn phải đóng";
                dataGridView1.Columns["HanDong"].HeaderText = "Hạn đóng";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";
                dataGridView1.Visible = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            HOCPHI();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một kỳ học để thanh toán!");
                return;
            }

            string tenKiHoc = dataGridView1.CurrentRow.Cells["TenKiHoc"].Value.ToString();
            string trangThai = dataGridView1.CurrentRow.Cells["TrangThai"].Value.ToString();

            if (trangThai == "Đã đóng")
            {
                MessageBox.Show("Bạn đã đóng học phí cho kỳ học này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string soTienConNo = dataGridView1.CurrentRow.Cells["SoTienConNo"].Value.ToString();


            Tuition tuitionForm = new Tuition(maSV, tenKiHoc, soTienConNo);
            tuitionForm.ShowDialog();


            HOCPHI();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string tenKiHoc = comboBox1.Text.Trim();
            string trangThai = comboBox2.Text.Trim();

            string tenKiHocParam = string.IsNullOrEmpty(tenKiHoc) ? "NULL" : $"N'{tenKiHoc.Replace("'", "''")}'";
            string trangThaiParam = string.IsNullOrEmpty(trangThai) ? "NULL" : $"N'{trangThai.Replace("'", "''")}'";

            string query = $@"
            EXEC sp_LocHocPhi 
            @MaSV = N'{maSV}', 
            @TenKiHoc = {tenKiHocParam}, 
            @TrangThai = {trangThaiParam}";

            DataTable dta = dp.Lay_DLbang(query);
            if (dta != null && dta.Rows.Count > 0)
            {
                dataGridView1.DataSource = dta;

               
                dataGridView1.Columns["SoTien"].HeaderText = "Tổng tiền";
                dataGridView1.Columns["SoTienConNo"].HeaderText = "Còn phải đóng";
                dataGridView1.Columns["HanDong"].HeaderText = "Hạn đóng";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Visible = true;
            }
            else
            {
                MessageBox.Show("Không tìm thấy học phí phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            LoadThongTinSinhVien();
            HOCPHI();
            dataGridView1.Visible = false;

            DataTable dtkihoc = dp.Lay_DLbang("SELECT DISTINCT TenKiHoc FROM KiHoc ORDER BY TenKiHoc");
            comboBox1.DataSource = dtkihoc;
            comboBox1.DisplayMember = "TenKiHoc";

            DataTable dtTrangThai = dp.Lay_DLbang("SELECT DISTINCT TrangThai FROM HocPhi ORDER BY TrangThai");
            DataTable dtTrangThaiHienThi = new DataTable();
            dtTrangThaiHienThi.Columns.Add("TrangThaiText", typeof(string));
            foreach (DataRow row in dtTrangThai.Rows)
            {
                dtTrangThaiHienThi.Rows.Add(row["TrangThai"].ToString());
            }
            comboBox2.DataSource = dtTrangThaiHienThi;
            comboBox2.DisplayMember = "TrangThaiText";

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.MediumBlue;
            label1.Cursor = Cursors.Hand; 
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
        
            label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pnlDropdown.Visible = !pnlDropdown.Visible; 
        }

        private void lkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                Application.Restart();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                    "Bạn có chắc chắn muốn thoát không?",
                                    "Xác nhận thoát",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        private void pnlDropdown_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

