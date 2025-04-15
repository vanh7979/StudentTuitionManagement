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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProjectStudentTuitionManagement
{
    public partial class Tuition : Form
    {
        private string maSV;
        private string tenKiHoc;
        private decimal soTien;

        public Tuition(string maSV, string tenKiHoc, string soTienText)
        {
            InitializeComponent();
            this.maSV = maSV;
            this.tenKiHoc = tenKiHoc;
            decimal.TryParse(soTienText, out this.soTien);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void Tuition_Load_1(object sender, EventArgs e)
        {
            label3.Text = $"{soTien:N0} VNĐ";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Bước 1: Kiểm tra số tiền nhập
            decimal daDong;
            if (!decimal.TryParse(textBox1.Text, out daDong) || daDong <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ và lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bước 2: Xác định ngân hàng được chọn
            string nganHang = "";
            if (radioButton1.Checked) nganHang = "MB Bank";
            else if (radioButton2.Checked) nganHang = "Techcombank";
            else if (radioButton3.Checked) nganHang = "MoMo";
            else if (radioButton4.Checked) nganHang = "BIDV";
            else
            {
                MessageBox.Show("Vui lòng chọn ngân hàng thanh toán!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bước 3: Gọi stored procedure để xử lý thanh toán
            DataProvider dp = new DataProvider();
            try
            {
                
                dp.KetNoiDl(); // mở kết nối

                SqlCommand cmd = new SqlCommand("sp_ThanhToanHocPhi", dp.cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào stored procedure
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.Parameters.AddWithValue("@TenKiHoc", tenKiHoc);
                cmd.Parameters.AddWithValue("@SoTien", daDong);
                cmd.Parameters.AddWithValue("@NganHang", nganHang);

                cmd.ExecuteNonQuery(); // thực thi

                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form hóa đơn
                FormInvoice f = new FormInvoice(maSV, tenKiHoc); // truyền vào nếu form hỗ trợ
                f.ShowDialog();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng kết nối
                
                dp.HuyKN();
            }
        }
    }
}

