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
using System.Net.Sockets;
using System.Text.RegularExpressions;


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
        public static string token = "";

        async Task<int> Login()
        {
            Request req = new Request();
            string[] parseAnswer = req.loginUser(loginBox.Text, passBox.Text);
            label3.Text = parseAnswer[0] + " " + parseAnswer[1];
            if (parseAnswer[0].Contains("OK")){
                login = loginBox.Text;
                token = parseAnswer[1];
                LookCafes cafeForm = new LookCafes();
                cafeForm.ControlBox = true;
                cafeForm.Activate();
                cafeForm.Show();
                this.Hide();
            }
            //Console.WriteLine(parseAnswer[0]);
            //Console.WriteLine('\n');
            //Console.WriteLine(parseAnswer[1]);
            return 1;
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            if(loginBox.Text!="" & passBox.Text != "")
            {
                await Login();
            }
            
            //LookCafes cafeForm = new LookCafes();
            //cafeForm.ControlBox = true;
            //cafeForm.Activate();
            //cafeForm.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegForm regForm = new RegForm();
            regForm.ControlBox = true;
            regForm.Activate();
            regForm.Show();
            this.Hide();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            F1 = this;
            //var login = await Login(new string[1]);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
