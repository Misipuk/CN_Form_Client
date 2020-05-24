using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CN_Form_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Form1 F1;
        public static string login = "mylog";

        private void button1_Click(object sender, EventArgs e)
        {
            LookCafes cafeForm = new LookCafes();
            cafeForm.ControlBox = true;
            cafeForm.Activate();
            cafeForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegForm regForm = new RegForm();
            regForm.ControlBox = true;
            regForm.Activate();
            regForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            F1 = this;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
