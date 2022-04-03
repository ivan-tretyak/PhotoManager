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

using System.Windows.Forms;

namespace PhotoManager.GUI.ShowImage
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LongitudeShow = new System.Windows.Forms.Label();
            this.LatitudeShow = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FocusLenghtShow = new System.Windows.Forms.Label();
            this.OrientationShow = new System.Windows.Forms.Label();
            this.ModelShow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ManufacturerShow = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CreationDateShow = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FlashShow = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            this.button5 = new System.Windows.Forms.Button();
            this.panel2 = new PhotoManager.GUI.ShowImage.MyPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.LongitudeShow, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.LatitudeShow, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.FocusLenghtShow, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.OrientationShow, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ModelShow, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ManufacturerShow, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.CreationDateShow, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.FlashShow, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // LongitudeShow
            // 
            resources.ApplyResources(this.LongitudeShow, "LongitudeShow");
            this.LongitudeShow.Name = "LongitudeShow";
            this.LongitudeShow.Click += new System.EventHandler(this.LongitudeShow_Click);
            // 
            // LatitudeShow
            // 
            resources.ApplyResources(this.LatitudeShow, "LatitudeShow");
            this.LatitudeShow.Name = "LatitudeShow";
            this.LatitudeShow.Click += new System.EventHandler(this.LatitudeShow_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // FocusLenghtShow
            // 
            resources.ApplyResources(this.FocusLenghtShow, "FocusLenghtShow");
            this.FocusLenghtShow.Name = "FocusLenghtShow";
            // 
            // OrientationShow
            // 
            resources.ApplyResources(this.OrientationShow, "OrientationShow");
            this.OrientationShow.Name = "OrientationShow";
            this.OrientationShow.Click += new System.EventHandler(this.OrientationShow_Click);
            // 
            // ModelShow
            // 
            resources.ApplyResources(this.ModelShow, "ModelShow");
            this.ModelShow.Name = "ModelShow";
            this.ModelShow.Click += new System.EventHandler(this.ModelShow_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ManufacturerShow
            // 
            resources.ApplyResources(this.ManufacturerShow, "ManufacturerShow");
            this.ManufacturerShow.Name = "ManufacturerShow";
            this.ManufacturerShow.Click += new System.EventHandler(this.ManufacturerShow_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // CreationDateShow
            // 
            resources.ApplyResources(this.CreationDateShow, "CreationDateShow");
            this.CreationDateShow.Name = "CreationDateShow";
            this.CreationDateShow.Click += new System.EventHandler(this.CreationDateShow_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // FlashShow
            // 
            resources.ApplyResources(this.FlashShow, "FlashShow");
            this.FlashShow.Name = "FlashShow";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // trackBar1
            // 
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // sqliteCommand1
            // 
            this.sqliteCommand1.CommandTimeout = 30;
            this.sqliteCommand1.Connection = null;
            this.sqliteCommand1.Transaction = null;
            this.sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Name = "panel2";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private TrackBar trackBar1;
        private Button button1;
        private Button button2;
        private Label CreationDateShow;
        private Label FlashShow;
        private Label LatitudeShow;
        private Label FocusLenghtShow;
        private Label OrientationShow;
        private Label ModelShow;
        private Label ManufacturerShow;
        private Button button3;
        private Button button4;
        private Label LongitudeShow;
        private Label label8;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Button button5;
        private MyPanel panel2;
        private PictureBox pictureBox2;
        private Button button6;
        private Label label9;
        private Label label10;
    }

    public class MyPanel : Panel
    {
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                int h = HorizontalScroll.Value;
                h += e.Delta;
                bool v = HorizontalScroll.Maximum > h;
                bool s = HorizontalScroll.Minimum < h;
                if (v && s)
                {
                    HorizontalScroll.Value = h;
                }
                else if (v && !s)
                {
                    HorizontalScroll.Value = HorizontalScroll.Minimum;
                }
                else if (!v && s)
                {
                    HorizontalScroll.Value = HorizontalScroll.Maximum;
                }
            }
            else
            {
                int h = VerticalScroll.Value;
                h += e.Delta;
                bool v = VerticalScroll.Maximum > h;
                bool s = VerticalScroll.Minimum < h;
                if (v && s)
                {
                    VerticalScroll.Value = h;
                }
                else if (v && !s)
                {
                    VerticalScroll.Value = VerticalScroll.Minimum;
                }
                else if (!v && s)
                {
                    VerticalScroll.Value = VerticalScroll.Maximum;
                }
            }
        }
    }
}