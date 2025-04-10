using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void Tuition_Load(object sender, EventArgs e)
        {
            label3.Text = $"{soTien:N0} VNĐ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            decimal daDong;
            if (!decimal.TryParse(textBox1.Text, out daDong) || daDong <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ và lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

            DataProvider dp = new DataProvider();

            // Lấy thông tin học phí của sinh viên và kỳ học
            string query = $@"
        SELECT hp.HocPhiID, hp.SoTien, kh.KiHocID
        FROM HocPhi hp
        JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
        WHERE hp.MaSV = N'{maSV}' AND kh.TenKiHoc = N'{tenKiHoc}'";

            DataTable dt = dp.Lay_DLbang(query);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy học phí cho kỳ học này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int hocPhiID = Convert.ToInt32(dt.Rows[0]["HocPhiID"]);
            int kiHocID = Convert.ToInt32(dt.Rows[0]["KiHocID"]);
            decimal tongTien = Convert.ToDecimal(dt.Rows[0]["SoTien"]);

            // Tổng số tiền đã đóng trước đó
            string queryDaDong = $"SELECT ISNULL(SUM(SoTienDaDong), 0) FROM ThanhToan WHERE HocPhiID = {hocPhiID}";
            decimal daDongTruoc = Convert.ToDecimal(dp.Lay_GiaTriDon(queryDaDong));

            // Tổng sau khi thanh toán lần này
            decimal tongDaDong = daDongTruoc + daDong;
            decimal conLai = tongTien - tongDaDong;

            // Xác định trạng thái mới
            string trangThai = "";
            if (tongDaDong == 0)
                trangThai = "Chưa đóng";
            else if (conLai <= 0)
                trangThai = "Đã đóng";
            else
                trangThai = "Còn nợ";

            // Tạo câu lệnh SQL để thực thi
            string sql = $@"
    BEGIN
        INSERT INTO ThanhToan (HocPhiID, NgayThanhToan, SoTienDaDong)
        VALUES ({hocPhiID}, GETDATE(), {daDong.ToString(System.Globalization.CultureInfo.InvariantCulture)});

        DECLARE @ThanhToanID INT;
        SET @ThanhToanID = SCOPE_IDENTITY();

        INSERT INTO HoaDon (ThanhToanID, NgayLap, SoTien)
        VALUES (@ThanhToanID, GETDATE(), {daDong.ToString(System.Globalization.CultureInfo.InvariantCulture)});

        UPDATE HocPhi
        SET TrangThai = N'{trangThai}'
        WHERE HocPhiID = {hocPhiID};";

            // Nếu còn nợ, thêm vào ThongBaoNo
            if (conLai > 0)
            {
                sql += $@"
        INSERT INTO ThongBaoNo (MaSV, KiHocID, SoTienNo, NgayThongBao)
        VALUES (N'{maSV}', {kiHocID}, {conLai.ToString(System.Globalization.CultureInfo.InvariantCulture)}, GETDATE());";
            }

            sql += "\nEND;";

            // Thực thi truy vấn
            try
            {
                dp.ThucThi(sql);
                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

