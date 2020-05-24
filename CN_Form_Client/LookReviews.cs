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
    public partial class LookReviews : Form
    {
        public LookReviews()
        {
            InitializeComponent();
        }

        async Task<int> getrevLst(int id)
        {
            Request req = new Request();
            string[] getRevAnswer = req.getRevCafe(Form1.token, id);
            List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(getRevAnswer[1]);
            for (int i = 0; i < reviews.Count; i++)
            {
                revListBox.Items.Add(reviews[i].owner + ": " + reviews[i].description + "\n  Stars = " + reviews[i].stars);
            }
            return 1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool f = true;
            revListBox.Items.Clear();
            int id = 10;
            try
            {
                id = int.Parse(textBox1.Text);
            }catch(Exception ee)
            {
                label2.Text = "Incorrect Id";
                f = false;
            }

            Request req = new Request();
            string[] parseCafes = req.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            int c = cafes.Count;
            if ((id<1) | (id > c))
            {
                label2.Text = "Incorrect Id";
                f = false;
            }
            if (f)
            {
                await getrevLst(id);
                label3.Text = (cafes[id-1].id) + " " + cafes[id - 1].name + " (" + cafes[id - 1].city + ")";
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
