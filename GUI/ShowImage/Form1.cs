//PhotoManager. Program to organize your photo to album.
//Copyright (C) 2021 Ivan Tretyak Nickolaevich
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSONSendGet;
using ORMDatabaseModule;
using System.Text;

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
        bool change = false;
        int oldPos = 0;
        RichTextBox rich;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(List<string> paths, int i)
        {
            InitializeComponent();
            this.paths = paths;
            this.index = i;
            org = new PictureBox();
            HelperShowImage.ShowImage(this.pictureBox2, org, paths[index], this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HelperShowImage.ShowImage(pictureBox2, org, paths[index], this);
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

            this.trackBar1.Value = (int)(pictureBox2.Width * 1.0 / pictureBox2.Image.Width * 100);
        }

        private void resize(object sender, EventArgs e)
        {
            HelperShowImage.resizer(panel2, pictureBox2);
            HelperShowImage.ShowImage(pictureBox2, org, paths[index], this);
            DisplayMetadata();
            this.trackBar1.Value = 100;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            HelperShowImage.ResizeImage(trackBar1, pictureBox2, org, panel2, paths[index], this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            if (index + 1 == paths.Count - 1)
            {
                button2.Enabled = false;
            }
            if (index + 1 > 0)
            {
                button1.Enabled = true;
            }
            index++;
            HelperShowImage.ShowImage(pictureBox2, org, paths[index], this);
            DisplayMetadata();
            pictureBox2.Size = new Size(panel2.Size.Width - 10, panel2.Size.Height - 10);
            trackBar1.Value = 100;
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

            var keyWords = HelperShowImage.LoadKeyWords(paths[index]);
            if (keyWords.Count() > 0)
            {
                label10.Text = keyWords.Aggregate((a, b) => a + ", " + b);
            }
            else
            {
                label10.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            if (index - 1 == 0)
            {
                button1.Enabled = false;
            }
            if (index - 1 < paths.Count - 1)
            {
                button2.Enabled = true;
            }
            index--;
            HelperShowImage.ShowImage(pictureBox2, org, paths[index], this);
            DisplayMetadata();
            pictureBox2.Size = new Size(panel2.Size.Width - 10, panel2.Size.Height - 10);
            trackBar1.Value = 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            HelperShowImage.RotateImage(pictureBox2, panel2, trackBar1, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            HelperShowImage.RotateImage(pictureBox2, panel2, trackBar1, false);
        }

        private void CreationDateShow_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            change = true;
            date = new();
            try
            {
                date.Value = DateTime.Parse(CreationDateShow.Text.Replace('\u00A0', ' '));
            }
            catch (Exception)
            {

            }
            date.Location = new Point(103, 120);
            this.tableLayoutPanel1.Controls.Add(date, 1, 7);
            date.CloseUp += new EventHandler(dateChanged);
            date.MaxDate = DateTime.Now;
            CreationDateShow.Dispose();
        }

        private void dateChanged(object sender, EventArgs e)
        {
            change = false;
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
            if (change)
            {
                return;
            }
            change = true;
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, LatitudeShow, 4, (EventHandler)placeInputLatitude_Click, tableLayoutPanel1);
        }

        private void placeInputLatitude_Click(object sender, EventArgs e)
        {
            change = false;
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
            change = false;
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

        private void keyWordsInput(object sender, EventArgs e)
        {
            change = false;
            label10 = new();
            label10.Text = rich.Text;
            label10.Size = rich.Size;
            rich.Dispose();
            HelperShowImage.AddNewKeyWords(label10.Text.Split(", "));
            HelperShowImage.AddLinkKeyWordPhoto(label10.Text.Split(", "), paths[index]);
            label10.Click += new EventHandler(label10_Click);
            tableLayoutPanel1.Controls.Add(label10, 1, 8);
        }

        private void LongitudeShow_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            change = true;
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, LongitudeShow, 5, (EventHandler)placeInputLongitude, tableLayoutPanel1);
        }

        private void ManufacturerShow_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            change = true;
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, ManufacturerShow, 0, (EventHandler)placeInputManufacturer, tableLayoutPanel1);
        }

        private void placeInputManufacturer(object sender, EventArgs e)
        {
            change = false;
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
            if (change)
            {
                return;
            }
            change = true;
            placeInput = new();
            HelperShowImage.ReplaceLabelTextBox(placeInput, ModelShow, 1, (EventHandler)placeInputModel, tableLayoutPanel1);
        }

        private void placeInputModel(object sender, EventArgs e)
        {
            change = false;
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
            if (change)
            {
                return;
            }
            change = true;
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
            change = false;
            if (orientationBox.SelectedItem != null)
            {
                var metadata = HelperShowImage.LoadMetadata(paths[index]);
                metadata.Orientation = int.Parse(orientationBox.SelectedItem.ToString());
                HelperShowImage.UpdateMetadata(metadata);
                orientationBox.Dispose();
                OrientationShow = new();
                tableLayoutPanel1.Controls.Add(OrientationShow, 1, 2);
                OrientationShow.Click += new EventHandler(OrientationShow_Click);
                HelperShowImage.ShowImage(pictureBox2, org, paths[index], this);
                DisplayMetadata();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            PrintImage();
        }

        void PrintImage()
        {
            PrintDocument pd = new PrintDocument();
            var path = HelperShowImage.GetFullPathForImage(paths[index]);
            var bmIm = new Bitmap(path);
            if (bmIm.Width > bmIm.Height)
            {
                pd.DefaultPageSettings.Landscape = true;
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPageHor);
            }
            else
            {
                pd.DefaultPageSettings.Landscape = false;
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPageVert);
            }

            PrintPreviewDialog printPreviewDialog = new();
            printPreviewDialog.Document = pd;
            printPreviewDialog.ShowDialog();
        }

        void pd_PrintPageHor(object sender, PrintPageEventArgs e)
        {
            double cmToUnits = 100 / 2.54;
            var path = HelperShowImage.GetFullPathForImage(paths[index]);
            var bmIm = new Bitmap(path);
            e.Graphics.DrawImage(bmIm, 0, 0, (float)(29.7 * cmToUnits), (float)(21 * cmToUnits));
        }

        void pd_PrintPageVert(object sender, PrintPageEventArgs e)
        {
            double cmToUnits = 100 / 2.54;
            var path = HelperShowImage.GetFullPathForImage(paths[index]);
            var bmIm = new Bitmap(path);
            e.Graphics.DrawImage(bmIm, 0, 0, (float)(21 * cmToUnits), (float)(29.7 * cmToUnits));
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            Clipboard.SetImage(pictureBox2.Image);
            await Task.Delay(1000);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (change)
            {
                return;
            }
            change = true;
            rich = new();
            HelperShowImage.ReplaceLabelRichTextBox(rich, label10, 8, (EventHandler)keyWordsInput, tableLayoutPanel1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == paths[index])
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                pictureBox2.Image.RotateFlip(HelperShowImage.OrientationToFlipType(metadata.Orientation));
            }
            if (change)
            {
                return; 
            }
            change = true;
            MemoryStream ms = new();
            pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            string[] keywords = KeywordsForImage.GetKeywords(ms.GetBuffer());
            if (keywords is null)
            {
                change = false;
                return;
            }
            if (keywords.Length > 0)
            {
                label10.Text = keywords.Aggregate((a, b) => a + ", " + b);
            }
            HelperShowImage.AddNewKeyWords(label10.Text.Split(", "));
            HelperShowImage.AddLinkKeyWordPhoto(label10.Text.Split(", "), paths[index]);
            change = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
