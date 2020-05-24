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
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        async Task<int> Reg()
        {
            Request req = new Request();
            if (logBox.Text != "" & passBox.Text != "")
            {
                string[] registerAnswer = req.registerUser(logBox.Text, passBox.Text);
                label1.Text = registerAnswer[0] + registerAnswer[1];
                if (registerAnswer[0].Contains("204"))
                {
                    Form1.F1.Show();
                    this.Hide();
                }
            }
            label1.Text = "Такой пользователь уже есть";
            return 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void regB_Click(object sender, EventArgs e)
        {
            Form1.F1.Show();
            this.Hide();
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
            logBox.Text = Form1.login;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Reg();
        }

       
    }
}
