using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CN_Form_Client
{
    public partial class LookCafes : Form
    {
        private LookCafes cafeF;
        public LookCafes()
        {
            InitializeComponent();
        }

        private void LookCafes_Load(object sender, EventArgs e)
        {
            cafeF = this;
            this.Text = "CafesUA List "  + Form1.login +  " (User)";
        }

        private void button5_Click(object sender, EventArgs e)
        {        
            Form1.F1.Show();
            this.Hide();
        }
    }
}
