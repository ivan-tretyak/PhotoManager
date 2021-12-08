using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        DateTimePicker date;
        TextBox placeInput;
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
            this.ResizeEnd += new EventHandler(this.resize);
            this.Resize += new EventHandler(this.resize);
        }

        private void resize(object sender, EventArgs e)
        {
            int x = panel1.Size.Width;
            int y = panel1.Size.Height;
            x = x - 10;
            y = y - 10;
            Size size = new();
            size.Width = x;
            size.Height = y;
            pictureBox1.Size = size;
            ShowImage();
        }

        private void ShowImage()
        {
            pictureBox1.Image = new Bitmap(this.paths[index]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            org.Load(paths[index]);
            LoadMetadata();

            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                pictureBox1.Image.RotateFlip(OrientationToFlipType(metadata.Orientation));
            }
        }

        private static RotateFlipType OrientationToFlipType(int orientation)
        {
            switch (orientation)
            {
                case 1:
                    return RotateFlipType.RotateNoneFlipNone;
                case 2:
                    return RotateFlipType.RotateNoneFlipX;
                case 3:
                    return RotateFlipType.Rotate180FlipNone;
                case 4:
                    return RotateFlipType.Rotate180FlipX;
                case 5:
                    return RotateFlipType.Rotate90FlipX;
                case 6:
                    return RotateFlipType.Rotate90FlipNone;
                case 7:
                    return RotateFlipType.Rotate270FlipX;
                case 8:
                    return RotateFlipType.Rotate270FlipNone;
                default:
                    return RotateFlipType.RotateNoneFlipNone;
            }
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
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            trackBar1.Value = 0;
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
                LatitudeShow.Text = $"{metadata.Latitude}";
                LongitudeShow.Text = $"{metadata.Longitude}";
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
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            trackBar1.Value = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            trackBar1.Value = 0;
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                int newOrientation = metadata.Orientation;

                if (newOrientation + 1 > 8)
                {
                    newOrientation = 1;
                }
                else
                {
                    newOrientation++;
                }
                metadata.Orientation = newOrientation;
                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
            LoadMetadata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            trackBar1.Value = 0;
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                int newOrientation = metadata.Orientation;

                if (newOrientation - 1 < 1)
                {
                    newOrientation = 1;
                }
                else
                {
                    newOrientation--;
                }
                metadata.Orientation = newOrientation;
                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
            LoadMetadata();
        }

        private void CreationDateShow_Click(object sender, EventArgs e)
        {
            date = new();
            date.Value = DateTime.Parse(CreationDateShow.Text.Replace('\u00A0', ' '));
            date.Location = new System.Drawing.Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(date, 1, 7);
            date.ValueChanged += new EventHandler(dateChanged);
            date.MaxDate = DateTime.Now;
            CreationDateShow.Dispose();
        }

        private void dateChanged(object sender, EventArgs e)
        {
            string dateString = date.Value.ToString();
            this.CreationDateShow = new();
            CreationDateShow.Text = dateString;
            date.Dispose();
            CreationDateShow.Location = new Point(103, 120);
            tableLayoutPanel1.Controls.Add(CreationDateShow, 1, 7);
            CreationDateShow.Click += new EventHandler(CreationDateShow_Click);
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                metadata.DateCreation = dateString;

                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
        }

        private void LatitudeShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            placeInput.Text = LatitudeShow.Text;
            placeInput.Location = new System.Drawing.Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(placeInput, 1, 4);
            placeInput.DoubleClick += new EventHandler(placeInputLatitude_Click);
            LatitudeShow.Dispose();
        }

        private void placeInputLatitude_Click(object sender, EventArgs e)
        {
            float latitude = float.Parse(placeInput.Text);
            LatitudeShow = new();
            LatitudeShow.Text = $"{latitude}";
            tableLayoutPanel1.Controls.Add(LatitudeShow, 1, 4);
            placeInput.Dispose();
            LatitudeShow.Click += new EventHandler(LatitudeShow_Click);
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                metadata.Latitude = latitude;

                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
        }

        private void placeInputLongitude(object sender, EventArgs e)
        {
            float longitude = float.Parse(placeInput.Text);
            LongitudeShow = new();
            LongitudeShow.Text = $"{longitude}";
            tableLayoutPanel1.Controls.Add(LongitudeShow, 1, 5);
            LongitudeShow.Click += new EventHandler(LongitudeShow_Click);
            placeInput.Dispose();
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                metadata.Longitude = longitude;

                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
        }

        private void LongitudeShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            placeInput.Text = LatitudeShow.Text;
            placeInput.Location = new System.Drawing.Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(placeInput, 1, 5);
            placeInput.DoubleClick += new EventHandler(placeInputLongitude);
            LatitudeShow.Dispose();
        }

        private void ManufacturerShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            placeInput.Text = ManufacturerShow.Text;
            placeInput.Location = new System.Drawing.Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(placeInput, 1, 0);
            placeInput.DoubleClick += new EventHandler(placeInputManufacturer);
            ManufacturerShow.Dispose();
        }

        private void placeInputManufacturer(object sender, EventArgs e)
        {
            string manufacturer = placeInput.Text;
            ManufacturerShow = new();
            ManufacturerShow.Text = $"{manufacturer}";
            tableLayoutPanel1.Controls.Add(ManufacturerShow, 1, 0);
            ManufacturerShow.Click += new EventHandler(ManufacturerShow_Click);
            placeInput.Dispose();
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                metadata.Manufacturer = manufacturer;

                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
        }

        private void ModelShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            placeInput.Text = ModelShow.Text;
            placeInput.Location = new System.Drawing.Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(placeInput, 1, 1);
            placeInput.DoubleClick += new EventHandler(placeInputModel);
            ModelShow.Dispose();
        }

        private void placeInputModel(object sender, EventArgs e)
        {
            string model = placeInput.Text;
            ModelShow = new();
            ModelShow.Text = $"{model}";
            tableLayoutPanel1.Controls.Add(ModelShow, 1, 1);
            ManufacturerShow.Click += new EventHandler(ModelShow_Click);
            placeInput.Dispose();
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                metadata.Model = model;

                db.MetaDatas.Update(metadata);
                db.SaveChanges();
            }
        }
    }
}
