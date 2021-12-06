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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CreationDateShow = new System.Windows.Forms.Label();
            this.FlashShow = new System.Windows.Forms.Label();
            this.PlacesShow = new System.Windows.Forms.Label();
            this.FocusLenghtShow = new System.Windows.Forms.Label();
            this.OrientationShow = new System.Windows.Forms.Label();
            this.ModelShow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ManufacturerShow = new System.Windows.Forms.Label();
            this.panel1 = new PhotoManager.GUI.ShowImage.MyPanel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(562, 419);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CreationDateShow, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.FlashShow, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.PlacesShow, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.FocusLenghtShow, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.OrientationShow, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ModelShow, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ManufacturerShow, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 140);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // CreationDateShow
            // 
            this.CreationDateShow.AutoSize = true;
            this.CreationDateShow.Location = new System.Drawing.Point(103, 120);
            this.CreationDateShow.Name = "CreationDateShow";
            this.CreationDateShow.Size = new System.Drawing.Size(44, 15);
            this.CreationDateShow.TabIndex = 13;
            this.CreationDateShow.Text = "label14";
            // 
            // FlashShow
            // 
            this.FlashShow.AutoSize = true;
            this.FlashShow.Location = new System.Drawing.Point(103, 100);
            this.FlashShow.Name = "FlashShow";
            this.FlashShow.Size = new System.Drawing.Size(44, 15);
            this.FlashShow.TabIndex = 12;
            this.FlashShow.Text = "label13";
            // 
            // PlacesShow
            // 
            this.PlacesShow.AutoSize = true;
            this.PlacesShow.Location = new System.Drawing.Point(103, 80);
            this.PlacesShow.Name = "PlacesShow";
            this.PlacesShow.Size = new System.Drawing.Size(44, 15);
            this.PlacesShow.TabIndex = 11;
            this.PlacesShow.Text = "label12";
            // 
            // FocusLenghtShow
            // 
            this.FocusLenghtShow.AutoSize = true;
            this.FocusLenghtShow.Location = new System.Drawing.Point(103, 60);
            this.FocusLenghtShow.Name = "FocusLenghtShow";
            this.FocusLenghtShow.Size = new System.Drawing.Size(44, 15);
            this.FocusLenghtShow.TabIndex = 10;
            this.FocusLenghtShow.Text = "label11";
            // 
            // OrientationShow
            // 
            this.OrientationShow.AutoSize = true;
            this.OrientationShow.Location = new System.Drawing.Point(103, 40);
            this.OrientationShow.Name = "OrientationShow";
            this.OrientationShow.Size = new System.Drawing.Size(44, 15);
            this.OrientationShow.TabIndex = 9;
            this.OrientationShow.Text = "label10";
            // 
            // ModelShow
            // 
            this.ModelShow.AutoSize = true;
            this.ModelShow.Location = new System.Drawing.Point(103, 20);
            this.ModelShow.Name = "ModelShow";
            this.ModelShow.Size = new System.Drawing.Size(38, 15);
            this.ModelShow.TabIndex = 8;
            this.ModelShow.Text = "label9";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manufacturer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Model";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Orientation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Focus Length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Place";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Flash";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Creation date";
            // 
            // ManufacturerShow
            // 
            this.ManufacturerShow.AutoSize = true;
            this.ManufacturerShow.Location = new System.Drawing.Point(103, 0);
            this.ManufacturerShow.Name = "ManufacturerShow";
            this.ManufacturerShow.Size = new System.Drawing.Size(38, 15);
            this.ManufacturerShow.TabIndex = 7;
            this.ManufacturerShow.Text = "label8";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(220, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 425);
            this.panel1.TabIndex = 2;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 159);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(201, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Previous";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 221);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Сounterclockwise";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(139, 221);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Clockwise";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private TrackBar trackBar1;
        private MyPanel panel1;
        private Button button1;
        private Button button2;
        private Label CreationDateShow;
        private Label FlashShow;
        private Label PlacesShow;
        private Label FocusLenghtShow;
        private Label OrientationShow;
        private Label ModelShow;
        private Label ManufacturerShow;
        private Button button3;
        private Button button4;
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