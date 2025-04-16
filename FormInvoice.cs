using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace NewProject
{
    public partial class FormInvoice : Form
    {
        public FormInvoice()
        {
            InitializeComponent();
        }
        public string hocPhiID;
        public FormInvoice(string hocPhiID)
        {
            InitializeComponent();
            this.hocPhiID = hocPhiID;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReportDocument rpt = new ReportDocument();
            string reportPath = Path.Combine(Application.StartupPath, "HOADON.rpt");
            rpt.Load(reportPath);

            rpt.SetDatabaseLogon("", "", "DESKTOP\\HAYLAMDMM", "NHOM7_LTUD"); 
            foreach (Table table in rpt.Database.Tables)
            {
                TableLogOnInfo logonInfo = table.LogOnInfo;
                logonInfo.ConnectionInfo.ServerName = "DESKTOP\\HAYLAMDMM";
                logonInfo.ConnectionInfo.DatabaseName = "NHOM7_LTUD";
                logonInfo.ConnectionInfo.IntegratedSecurity = true; 
                table.ApplyLogOnInfo(logonInfo);
            }
            string safeHocPhiID = hocPhiID.Replace("'", "''"); // escape ký tự '
            rpt.RecordSelectionFormula = $"{{v_HoaDon_ChiTiet.HocPhiID}} = '{safeHocPhiID}'";
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }

        private void FormInvoice_Load(object sender, EventArgs e)
        {

        }
    }
}
