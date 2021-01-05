using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTPFisradLanj20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form2 planForm = null; 

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            if (planForm == null)
            {
                planForm = new Form2();
                planForm.MdiParent = this;
                planForm.FormClosed += PlanForm_FormClosed;
                planForm.Show();
            }
            else
            {
                planForm.Activate();
            }
        }

        private void PlanForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            planForm = null;
        }
    }
}
