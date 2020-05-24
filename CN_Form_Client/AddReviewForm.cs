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
    public partial class AddReviewForm : Form
    {
        public AddReviewForm()
        {
            InitializeComponent();
        }

        async Task<int> sendRev(int id, int stars)
        {
            Request req = new Request();
            Review r = new Review(textBox1.Text, id, stars, richTextBox1.Text);
            string[] getAddRevAnswer = req.addRevCafe(Form1.token, r);
            if (getAddRevAnswer[0].Contains("204"))
            {
                label5.Text = "Ваш отзыв добавлен";
            }
            
            return 1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool f = true;
            int id = 10;
            int star = 5;
            try
            {
                id = int.Parse(textBox1.Text);
                star = int.Parse(textBox2.Text);
            }
            catch (Exception ee)
            {
                label5.Text = "Incorrect Data";
                f = false;
            }

            Request req = new Request();
            string[] parseCafes = req.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            int c = cafes.Count;
            if ((id < 1) | (id > c))
            {
                label5.Text = "Incorrect Data";
                f = false;
            }
            if ((star<0) | (star > 5) | (richTextBox1.Text == "")){
                label5.Text = "Incorrect Data";
                f = false;
            }


            if (f)
            {
                await sendRev(id, star);
               // label3.Text = (cafes[id - 1].id) + " " + cafes[id - 1].name + " (" + cafes[id - 1].city + ")";
            }
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
