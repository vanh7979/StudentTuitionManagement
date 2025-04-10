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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void ShowControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            ShowControl(new StudentManagement());
        }

        private void btnSemester_Click(object sender, EventArgs e)
        {
            ShowControl(new SemesterManagement());
        }

        private void btnTuition_Click(object sender, EventArgs e)
        {
            ShowControl(new TuitionManagement());
        }

        private void btnStatisticReport_Click(object sender, EventArgs e)
        {
            ShowControl(new StatisticReport());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
