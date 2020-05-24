using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CN_Form_Client
{
    public partial class createCafeForm : Form
    {
        public createCafeForm()
        {
            InitializeComponent();
        }

        async Task<int> loadCafe()
        {
            Request req = new Request();
            string[] parseCafes = req.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            for (int i = 0; i < cafes.Count; i++)
            {
                if(cafes[i].owner == Form1.login)
                {
                    textBox1.Text = cafes[i].name;
                    textBox2.Text = cafes[i].city;
                    richTextBox1.Text = cafes[i].des;
                    label5.Text = "Данные вашего кафе введены в форму";

                }
                //cafeListBox.Items.Add((cafes[i].id) + " " + cafes[i].name + " (" + cafes[i].city + ")");
            }
            if ((textBox1.Text == "") & (textBox2.Text == "") & (richTextBox1.Text == ""))
            {
                label5.Text = "У вас еще нет кафе";
            }
            return 1;
        }

        async Task<int> putCafe()
        {
            Request req = new Request();
            string[] addCafeAnswer = req.addCafe(Form1.token, textBox1.Text, richTextBox1.Text, textBox2.Text);
            if (addCafeAnswer[0].Contains("204"))
            {
                label5.Text = "Выполнено";
            }
            if ((textBox1.Text == "") & (textBox2.Text == "") & (richTextBox1.Text == ""))
            {
                label5.Text = "У вас еще нет кафе";
            }
            return 1;
        }

        private async void createCafeForm_Load(object sender, EventArgs e)
        {
            await loadCafe();
            
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool f = true;
            if ((textBox1.Text == "") | (textBox2.Text == "") | (richTextBox1.Text == ""))
            {
                label5.Text = "Неполная информация";
                f = false;
            }
            await putCafe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LookCafes cafeForm = new LookCafes();
            cafeForm.ControlBox = true;
            cafeForm.Activate();
            cafeForm.Show();
            this.Hide();
        }
    }
}
