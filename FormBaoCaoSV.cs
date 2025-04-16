using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace NewProject
{
    public partial class FormBaoCaoSV : Form
    {
        public FormBaoCaoSV()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                // Bước 1: Lấy dữ liệu từ VIEW thay vì stored procedure
                string connectionString = "Data Source=PTRANVANH;Initial Catalog=NHOM7_LTUD;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM v_ThongKeSinhVienDayDu", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("❗ Không có dữ liệu để hiển thị báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Bước 2: Load Crystal Report
                    ReportDocument rpt = new ReportDocument();
                    string reportPath = Path.Combine(Application.StartupPath, "BAOCAOSINHVIEN.rpt");
                    rpt.Load(reportPath);

                    // Bước 3: Thiết lập kết nối CSDL cho Crystal Report
                    rpt.SetDatabaseLogon("", "", "PTRANVANH", "NHOM7_LTUD");

                    foreach (Table table in rpt.Database.Tables)
                    {
                        TableLogOnInfo logonInfo = table.LogOnInfo;
                        logonInfo.ConnectionInfo.ServerName = "PTRANVANH";
                        logonInfo.ConnectionInfo.DatabaseName = "NHOM7_LTUD";
                        logonInfo.ConnectionInfo.IntegratedSecurity = true;
                        table.ApplyLogOnInfo(logonInfo);
                    }

                    // Bước 4: Gán dữ liệu và hiển thị
                    rpt.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải báo cáo sinh viên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
