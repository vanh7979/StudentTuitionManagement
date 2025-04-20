using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace NewProject
{
    public partial class FormBaoCaoHP : Form
    {
        public FormBaoCaoHP()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                
                string connectionString = "Data Source=PTRANVANH;Initial Catalog=NHOM7_LTUD;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM v_BaoCaoHocPhi", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("❗ Không có dữ liệu để hiển thị báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ReportDocument rpt = new ReportDocument();
                    string reportPath = Path.Combine(Application.StartupPath, "BAOCAOHOCPHI.rpt");
                    rpt.Load(reportPath);
                    rpt.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải báo cáo học phí:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
