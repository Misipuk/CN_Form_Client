using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
            if (getRevAnswer[0].Contains("404"))
            {
                label2.Text = "У данного кафе нет отзывов";
                return 1;
            }
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

        private void LookReviews_Load(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Request req = new Request();
            var fileName = @"D:\sem6_protocols\CN_Form_Client\photos";
            Console.WriteLine('\n');
            Console.WriteLine("Get cafe media download \n\n");
            string[] parseGetMedia = req.getCafeMedia(Form1.token, 1, fileName);
            Console.WriteLine(parseGetMedia[0]);
            Console.WriteLine('\n');
            //Console.WriteLine(parseGetMedia[1]);
            List<MediaFile> files = JsonConvert.DeserializeObject<List<MediaFile>>(parseGetMedia[1]);
            Console.WriteLine(files[0].type);
            var base64EncodedBytes = System.Convert.FromBase64String(files[0].bcd);
            int numBytesToRead = base64EncodedBytes.Length;
            fileName = fileName + "\\" + files[0].id + ".jpg";
            try
            {
                using (FileStream fsNew = new FileStream(fileName,
                FileMode.Create, FileAccess.Write))
                {
                    fsNew.Write(base64EncodedBytes, 0, numBytesToRead);
                }
            }
            catch (Exception ee)
            {

            }
        }
    }
}
