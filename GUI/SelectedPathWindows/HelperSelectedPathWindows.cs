using System.Windows.Forms;
using IndexingModule;
using ORMDatabaseModule;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;

namespace PhotoManager
{
    public class HelperSelectedPathWindows
    {
        public Button AddRow(TableLayoutPanel panel)
        {
            panel.RowCount++;

            var textbox = new TextBox();
            var label = new Label();
            var button = new Button();

            //label.TabIndex = 0;
            label.Text = "Path:";
            label.AutoSize = true;

            textbox.Size = new System.Drawing.Size(588, 23);
            textbox.Name = $"textBox{panel.RowCount}";
            //textbox.TabIndex = 1;

            button.Size = new System.Drawing.Size(139, 23);
            //button.TabIndex = 2;
            button.Text = "Choose";
            button.UseVisualStyleBackColor = true;
            button.Name = $"{panel.RowCount}";

            panel.Controls.Add(textbox, 1, panel.RowCount);
            panel.Controls.Add(label, 0, panel.RowCount);
            panel.Controls.Add(button, 2, panel.RowCount);
            panel.RowStyles.Add(new RowStyle());
            panel.Size = new System.Drawing.Size(776, 31 * panel.RowCount);

            return button;
        }

        public void RemoveRow(TableLayoutPanel panel)
        {
            int row = panel.RowCount;
            if (row != 1)
            {
                for (int i = 0; i < panel.ColumnCount; i++)
                {
                    Control c = panel.GetControlFromPosition(i, row);
                    panel.Controls.Remove(c);
                    c.Dispose();
                }
                panel.RowStyles.RemoveAt(row - 1);
                panel.RowCount--;

                panel.Size = new System.Drawing.Size(776, 31 * panel.RowCount);
            }
        }
        public bool SelectFolderPath(object sender, TableLayoutPanel panel)
        {
            var s = new FolderBrowserDialog();
            var result = s.ShowDialog();
            if ((Control)sender != null && result == DialogResult.OK)
            {
                int row = panel.GetPositionFromControl((Control)sender).Row;
                int column = panel.GetPositionFromControl((Control)sender).Column;
                panel.GetControlFromPosition(column - 1, row).Text = s.SelectedPath;
                return true;
            }
            return false;
        }

        public void Scanning(TableLayoutPanel panel)
        {
            //Create first album
            var album = new Album();
            album.Name = "Common";
            album.DateCreation = DateTime.Now.ToString();

            //Save album to database
            using (var db = new DatabaseContext())
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

            Indexing indexing = new();
            for (int i = 0; i < panel.RowCount; i++)
            {
                int c = 0;
                var res = indexing.IndexingDirectory(panel.Controls[i * 3].Text);
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

                    //Create albumcontext
                    AlbumContext albumContext = new();

                    //Associate album with photo
                    albumContext.Album = albums;
                    albumContext.Photo = p;

                    using (var db = new DatabaseContext())
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
        }
    }
}
