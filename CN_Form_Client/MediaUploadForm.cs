using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CN_Form_Client
{
    public partial class MediaUploadForm : Form
    {
        public MediaUploadForm()
        {
            InitializeComponent();
        }

        async Task<int> loadMedia(string fileName)
        {
            int cafeId = 0;
            Request req1 = new Request();
            string[] parseCafes = req1.getCafes(Form1.token);
            List<Cafe> cafes = JsonConvert.DeserializeObject<List<Cafe>>(parseCafes[1]);
            for (int i = 0; i < cafes.Count; i++)
            {
                if (cafes[i].owner == Form1.login)
                {
                    cafeId = cafes[i].id;

                }
                //cafeListBox.Items.Add((cafes[i].id) + " " + cafes[i].name + " (" + cafes[i].city + ")");
            }

            Request req = new Request();
            string[] parseSendMedia = req.sendCafeMedia(Form1.token, cafeId, fileName);
            label2.Text = parseSendMedia[0];
            return 1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool f = false;
            f = File.Exists(textBox1.Text);
            label2.Text = f.ToString();
            if (f = true && (textBox1.Text.Contains("jpg")| textBox1.Text.Contains("mov")))
            {
                await loadMedia(textBox1.Text);
            }
            else
            {
                label2.Text = "Incorrect Data";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            createCafeForm crcafeForm = new createCafeForm();
            crcafeForm.ControlBox = true;
            crcafeForm.Activate();
            crcafeForm.Show();
            this.Hide();
        }
    }
}
