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
               
                string reportPath = Path.Combine(Application.StartupPath, "BAOCAOHOCPHI.rpt");

                ReportDocument rpt = new ReportDocument();
                rpt.Load(reportPath);

               
                ConnectionInfo connectionInfo = new ConnectionInfo()
                {
                    ServerName = "PTRANVANH", 
                    DatabaseName = "NHOM7_LTUD",
                    IntegratedSecurity = true
                };

                
                foreach (Table table in rpt.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải báo cáo học phí:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
