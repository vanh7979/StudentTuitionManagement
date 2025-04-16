using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewProject;

namespace ProjectStudentTuitionManagement
{
    public partial class StatisticReport : UserControl
    {
        public StatisticReport()
        {
            InitializeComponent();
        }

        private void StatisticReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormBaoCaoSV form = new FormBaoCaoSV();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormBaoCaoHP form = new FormBaoCaoHP();
            form.ShowDialog();
        }
    }
}
