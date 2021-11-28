using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndexingModule;
using ORMDatabaseModule;
using PhotoManager.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;


namespace PhotoManager
{
    public partial class SelectPathWindows : Form
    {
        private HelperSelectedPathWindows helper = new();
        public SelectPathWindows()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = helper.AddRow(this.tableLayoutPanel1);
            button.Click += new System.EventHandler(button_Click);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            helper.RemoveRow(this.tableLayoutPanel1);
        }

        private void button_Click(object sender, EventArgs e)
        {
            bool result = helper.SelectFolderPath(sender, this.tableLayoutPanel1);
            if (result)
            {
                this.scanstart.Enabled = true;
            }
        }

        private void scanstart_Click(object sender, EventArgs e)
        {
            //Create first album
            var album = new Album();
            album.Name = "Common";
            album.DateCreation = DateTime.Now.ToString();

            //Save album to database
            using(var db = new DatabaseContext())
            {
                var b = db.Database.EnsureCreated();
                db.Add(album);
                db.SaveChanges();
            }

            //Get
            var albums = new Album();
            using (var db = new DatabaseContext())
            {
                albums = db.Albums
                    .First();
            }

            //Create albumcontext
            AlbumContext albumContext = new();

            Indexing indexing = new();
            for(int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                int c = 0;
                var res = indexing.IndexingDirectory(this.tableLayoutPanel1.Controls[i * 3].Text);
                foreach (string s in res)
                {
                    //Get metadata
                    var image = new IndexingModule.Image(s);
                    //create photo
                    Photo p = new();
                    
                    ORMDatabaseModule.MetaData m = new();

                    //Adding data
                    p.Path = s;
                    m.DateCreation = image.GetDateTime().ToString();
                    m.Flash = image.GetFlash();
                    m.Latitude = (float)image.GetLatitude();
                    m.Longitude = (float)image.GetLongitude();
                    m.FocusLength = (float)image.GetFocalLength();
                    m.Orientation = image.GetOrientation();
                    m.Model = image.GetModel();
                    m.Manufacturer = image.GetManufacturer();
                    p.MetaData = m;

                    //Associate album with photo
                    albumContext.Album = albums;
                    albumContext.Photo = p;

                    using(var db = new DatabaseContext())
                    {
                        var b = db.Database.EnsureCreated();
                        //db.Add(p);
                        db.AlbumContexts.Add(albumContext);
                        db.Albums.Attach(albums);
                        //db.Photos.Attach(p);
                        db.SaveChanges();
                        c++;
                    }
                }
            }
            this.Close();
        }
    }
}
