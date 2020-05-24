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
    public partial class MyReviewsForm : Form
    {
        public MyReviewsForm()
        {
            InitializeComponent();
        }

        async Task<int> getrevLst()
        {
            Request req1 = new Request();
            string[] parseCafes = req1.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            for (int i = 0; i < cafes.Count; i++)
            {
                await workCafeReview(cafes[i].id, cafes[i].name);
            }
            return 1;
        }

        async Task<int> workCafeReview(int id, string cafeName)
        {

            Request req = new Request();
            string[] getRevAnswer = req.getRevCafe(Form1.token, id);
            if (getRevAnswer[0].Contains("404"))
            {
                return 1;
            }
            List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(getRevAnswer[1]);
            for (int i = 0; i < reviews.Count; i++)
            {
                if (reviews[i].owner == Form1.login)
                {
                    revListBox.Items.Add("About " + cafeName + ": " + reviews[i].description + "\n  Stars = " + reviews[i].stars);
                }
                
            }
            return 1;
        }

        private async void MyReviewsForm_Load(object sender, EventArgs e)
        {
            await getrevLst();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LookCafes cafeForm = new LookCafes();
            cafeForm.ControlBox = true;
            cafeForm.Activate();
            cafeForm.Show();
            this.Hide();
        }
    }
}
