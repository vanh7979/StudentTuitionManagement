using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace NewProject
{
    public partial class FormInvoice : Form
    {
        private string maSV;
        private string tenKiHoc;
        private string strKN = "Data Source=PTRANVANH;Initial Catalog=NHOM7_LTUD;Integrated Security=True";

        public FormInvoice(string maSV, string tenKiHoc)
        {
            InitializeComponent();
            this.maSV = maSV;
            this.tenKiHoc = tenKiHoc;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                string hocPhiID = LayHocPhiID();

                if (string.IsNullOrEmpty(hocPhiID))
                {
                    MessageBox.Show("Không tìm thấy học phí tương ứng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReportDocument rpt = new ReportDocument();
                string reportPath = Path.Combine(Application.StartupPath, "HOADON.rpt");
                rpt.Load(reportPath);

                rpt.SetDatabaseLogon("", "", "PTRANVANH", "NHOM7_LTUD");

                foreach (Table table in rpt.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo.ServerName = "PTRANVANH";
                    logonInfo.ConnectionInfo.DatabaseName = "NHOM7_LTUD";
                    logonInfo.ConnectionInfo.IntegratedSecurity = true;
                    table.ApplyLogOnInfo(logonInfo);
                }

                // 🟢 Phải đặt dấu nháy đơn cho chuỗi NVARCHAR
                rpt.RecordSelectionFormula = $"{{v_HoaDon_ChiTiet.HocPhiID}} = '{hocPhiID}'";

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string LayHocPhiID()
        {
            string hocPhiID = null;
            using (SqlConnection conn = new SqlConnection(strKN))
            {
                conn.Open();
                string query = @"
                    SELECT TOP 1 hp.HocPhiID
                    FROM HocPhi hp
                    JOIN KiHoc kh ON kh.KiHocID = hp.KiHocID
                    WHERE hp.MaSV = @MaSV AND kh.TenKiHoc = @TenKiHoc
                    ORDER BY hp.HocPhiID DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", maSV);
                    cmd.Parameters.AddWithValue("@TenKiHoc", tenKiHoc);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        hocPhiID = result.ToString(); 
                    }
                }
            }
            return hocPhiID;
        }
    }
}
