using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORMDatabaseModule;

namespace PhotoManager.GUI.ShowImage
{
    public partial class Form1 : Form
    {
        List<string> paths;
        PictureBox org;
        int index;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(List<string> paths, string albumName, int i)
        {
            InitializeComponent();
            this.paths = paths;
            this.index = i;
            org = new PictureBox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowImage();
            if (index == 0)
            {
                button1.Enabled = false;
            }
            if (index == paths.Count - 1)
            {
                button2.Enabled = false;
            }
        }

        private void ShowImage()
        {
            pictureBox1.Image = new Bitmap(this.paths[index]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            org.Load(paths[index]);
            LoadMetadata();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value > 0)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                panel1.AutoScroll = true;
                panel1.HorizontalScroll.Visible = true;
                panel1.VerticalScroll.Visible = true;
                var img = new Bitmap(paths[index]);
                Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * trackBar1.Value / 100), Convert.ToInt32(img.Height * trackBar1.Value / 100));
                Graphics gpu = Graphics.FromImage(bm);
                gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                pictureBox1.Image.Dispose();
                pictureBox1.Image = bm;
                gpu.Dispose();
            }
            else
            {
                ShowImage();
                panel1.AutoScroll = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index + 1 == paths.Count - 1)
            {
                button2.Enabled = false;
            }
            if (index + 1 > 0)
            {
                button1.Enabled = true;
            }
            index++;
            ShowImage();
        }

        private void LoadMetadata()
        {
            using (var db = new DatabaseContext())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                ManufacturerShow.Text = metadata.Manufacturer;
                ModelShow.Text = metadata.Model;
                OrientationShow.Text = metadata.Orientation.ToString();
                FocusLenghtShow.Text = $"{metadata.FocusLength}mm";
                PlacesShow.Text = $"{metadata.Longitude}, {metadata.Latitude}";
                FlashShow.Text = metadata.Flash.ToString();
                CreationDateShow.Text = metadata.DateCreation;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index - 1 == 0)
            {
                button1.Enabled = false;
            }
            if (index - 1 < paths.Count - 1)
            {
                button2.Enabled = true;
            }
            index--;
            ShowImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;
            trackBar1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;
            trackBar1.Value = 0;
        }
    }
}
