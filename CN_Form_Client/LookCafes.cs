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
    public partial class LookCafes : Form
    {
        private LookCafes cafeF;
        public LookCafes()
        {
            InitializeComponent();
        }

        async Task<int> loadLst()
        {
            Request req = new Request();
            string[] parseCafes = req.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            for (int i = 0; i < cafes.Count; i++)
            {
                cafeListBox.Items.Add((cafes[i].id) + " " + cafes[i].name + " (" + cafes[i].city + ")");
            }
            return 1;
        }


        private async void LookCafes_Load(object sender, EventArgs e)
        {
            cafeF = this;
            this.Text = "CafesUA List "  + Form1.login +  " (User)";
            await loadLst();
        }

        private void button5_Click(object sender, EventArgs e)
        {        
            Form1.F1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LookReviews revForm = new LookReviews();
            revForm.ControlBox = true;
            revForm.Activate();
            revForm.Show();
            this.Hide();
        }

        private void addReviewB_Click(object sender, EventArgs e)
        {
            AddReviewForm addRevForm = new AddReviewForm();
            addRevForm.ControlBox = true;
            addRevForm.Activate();
            addRevForm.Show();
            this.Hide();
        }

        private void createCafeB_Click(object sender, EventArgs e)
        {
            createCafeForm crcafeForm = new createCafeForm();
            crcafeForm.ControlBox = true;
            crcafeForm.Activate();
            crcafeForm.Show();
            this.Hide();
        }

        private async void onFiltB_Click(object sender, EventArgs e)
        {
            cafeListBox.Items.Clear();
            Request req = new Request();
            string[] parseCafes = req.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            for (int i = 0; i < cafes.Count; i++)
            {
                if ((idBox.Text == "") | (cafes[i].id.ToString().Contains(idBox.Text)))
                {
                    if ((nameBox.Text == "") | (cafes[i].name.ToLower().Contains(nameBox.Text.ToLower())))
                    {
                        if ((cityBox.Text == "") | (cafes[i].city.ToLower().Contains(cityBox.Text.ToLower())))
                        {
                            cafeListBox.Items.Add((cafes[i].id) + " " + cafes[i].name + " (" + cafes[i].city + ")");
                        }
                    }
                }
               
            }
        }

        private async void offFiltB_Click(object sender, EventArgs e)
        {
            cafeListBox.Items.Clear();
            await loadLst();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyReviewsForm myReviewsForm = new MyReviewsForm();
            myReviewsForm.ControlBox = true;
            myReviewsForm.Activate();
            myReviewsForm.Show();
            this.Hide();
        }
    }
}
