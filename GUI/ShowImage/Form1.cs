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
using PhotoManager.GUI.ShowImage;

namespace PhotoManager.GUI.ShowImage
{
    public partial class Form1 : Form
    {
        List<string> paths;
        PictureBox org;
        int index;
        DateTimePicker date;
        TextBox placeInput;
        ComboBox orientationBox;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            HelperShowImage.ShowImage(pictureBox1, org, paths[index], this);
            DisplayMetadata();
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
            HelperShowImage.resizer(panel1, pictureBox1);
            HelperShowImage.ShowImage(pictureBox1, org, paths[index], this);
            DisplayMetadata();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            HelperShowImage.ResizeImage(trackBar1, pictureBox1, org, panel1, paths[index], this);
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
            HelperShowImage.ShowImage(pictureBox1, org, paths[index], this);
            DisplayMetadata();
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            trackBar1.Value = 0;
        }

        private void DisplayMetadata()
        {
            var metadata = HelperShowImage.LoadMetadata(paths[index]);

            if (metadata.Manufacturer == "")
            {
                ManufacturerShow.Text = ShowImageString.UnknownManufacturer;
            }
            else
            {
                ManufacturerShow.Text = metadata.Manufacturer;
            }

            if (metadata.Model == "")
            {
                ModelShow.Text = ShowImageString.UnknownModel;
            }
            else
            {
                ModelShow.Text = metadata.Model;
            }

            OrientationShow.Text = metadata.Orientation.ToString();
            FocusLenghtShow.Text = $"{metadata.FocusLength}{ShowImageString.FocalLengthUnit}";
            LatitudeShow.Text = $"{metadata.Latitude}";
            LongitudeShow.Text = $"{metadata.Longitude}";
            FlashShow.Text = metadata.Flash.ToString();
            CreationDateShow.Text = metadata.DateCreation;
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
            HelperShowImage.ShowImage(pictureBox1, org, paths[index], this);
            DisplayMetadata();
            pictureBox1.Size = new Size(panel1.Size.Width - 10, panel1.Size.Height - 10);
            trackBar1.Value = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HelperShowImage.RotateImage(pictureBox1, panel1, trackBar1, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HelperShowImage.RotateImage(pictureBox1, panel1, trackBar1, false);
        }

        private void CreationDateShow_Click(object sender, EventArgs e)
        {
            date = new();
            try
            {
                date.Value = DateTime.Parse(CreationDateShow.Text.Replace('\u00A0', ' '));
            }
            catch (Exception)
            {

            }
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
            var metadata = HelperShowImage.LoadMetadata(paths[index]);
            metadata.DateCreation = dateString;
            HelperShowImage.UpdateMetadata(metadata);
        }

        private void LatitudeShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, LatitudeShow, 4, (EventHandler)placeInputLatitude_Click, tableLayoutPanel1);
        }

        private void placeInputLatitude_Click(object sender, EventArgs e)
        {
            float latitude = float.Parse(placeInput.Text);
            LatitudeShow = new();
            LatitudeShow.Text = $"{latitude}";
            tableLayoutPanel1.Controls.Add(LatitudeShow, 1, 4);
            placeInput.Dispose();
            LatitudeShow.Click += new EventHandler(LatitudeShow_Click);
            var metadata = HelperShowImage.LoadMetadata(paths[index]);
            metadata.Latitude = latitude;
            HelperShowImage.UpdateMetadata(metadata);
        }

        private void placeInputLongitude(object sender, EventArgs e)
        {
            float longitude = float.Parse(placeInput.Text);
            LongitudeShow = new();
            LongitudeShow.Text = $"{longitude}";
            tableLayoutPanel1.Controls.Add(LongitudeShow, 1, 5);
            LongitudeShow.Click += new EventHandler(LongitudeShow_Click);
            placeInput.Dispose();
            var metadata = HelperShowImage.LoadMetadata(paths[index]);
            metadata.Longitude = longitude;
            HelperShowImage.UpdateMetadata(metadata);
        }

        private void LongitudeShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, LongitudeShow, 5, (EventHandler)placeInputLongitude, tableLayoutPanel1);
        }

        private void ManufacturerShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, ManufacturerShow, 0, (EventHandler)placeInputManufacturer, tableLayoutPanel1);
        }

        private void placeInputManufacturer(object sender, EventArgs e)
        {
            string manufacturer = placeInput.Text;
            ManufacturerShow = new();
            ManufacturerShow.Text = $"{manufacturer}";
            tableLayoutPanel1.Controls.Add(ManufacturerShow, 1, 0);
            placeInput.Dispose();
            ManufacturerShow.Click += new EventHandler(ManufacturerShow_Click);
            var metadata = HelperShowImage.LoadMetadata(paths[index]);
            metadata.Manufacturer = manufacturer;
            HelperShowImage.UpdateMetadata(metadata);
        }

        private void ModelShow_Click(object sender, EventArgs e)
        {
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, ModelShow, 1, (EventHandler)placeInputModel, tableLayoutPanel1);
        }

        private void placeInputModel(object sender, EventArgs e)
        {
            string model = placeInput.Text;
            ModelShow = new();
            ModelShow.Text = $"{model}";
            placeInput.Dispose();
            tableLayoutPanel1.Controls.Add(ModelShow, 1, 1);
            ModelShow.Click += new EventHandler(ModelShow_Click);
            var metadata = HelperShowImage.LoadMetadata(paths[index]);
            metadata.Model = model;
            HelperShowImage.UpdateMetadata(metadata);
        }

        private void OrientationShow_Click(object sender, EventArgs e)
        {
            orientationBox = new();
            for(int i = 1; i < 9; i++)
            {
                orientationBox.Items.Add(i);
            }
            tableLayoutPanel1.Controls.Add(orientationBox, 1, 2);
            orientationBox.SelectedIndexChanged += new EventHandler(OrientationBox_SelectedIndexChanged);
            OrientationShow.Dispose();
        }

        private void OrientationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (orientationBox.SelectedItem != null)
            {
                var metadata = HelperShowImage.LoadMetadata(paths[index]);
                metadata.Orientation = int.Parse(orientationBox.SelectedItem.ToString());
                HelperShowImage.UpdateMetadata(metadata);
                orientationBox.Dispose();
                OrientationShow = new();
                tableLayoutPanel1.Controls.Add(OrientationShow, 1, 2);
                OrientationShow.Click += new EventHandler(OrientationShow_Click);
                HelperShowImage.ShowImage(pictureBox1, org, paths[index], this);
                DisplayMetadata();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
