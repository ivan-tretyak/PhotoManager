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
using IndexingModule;
using ORMDatabaseModule;
using System;
using System.Linq;
using PhotoManager.GUI.SelectedPathWindows;

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
            label.Text = StringOfSelectedPath.PathLabelText;
            label.AutoSize = true;

            textbox.Size = new System.Drawing.Size(588, 23);
            textbox.Name = $"textBox{panel.RowCount}";
            //textbox.TabIndex = 1;

            button.Size = new System.Drawing.Size(139, 23);
            //button.TabIndex = 2;
            button.Text = StringOfSelectedPath.ChooseButtonText;
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

        public void Scanning(TableLayoutPanel panel, ProgressBar bar, Label label)
        {
            //Create first album
            var album = new Album();
            album.Name = StringOfSelectedPath.AlbumName;
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

            label.Text = $"0/{panel.RowCount}";

            Indexing indexing = new();
            for (int i = 0; i < panel.RowCount; i++)
            {
                label.Text = $"{i}/{panel.RowCount}";
                int c = 0;
                var res = indexing.IndexingDirectory(panel.Controls[i * 3].Text);
                bar.Maximum = res.Count;
                bar.Value = 0;
                int count = 0;
                foreach (Image image in res)
                {
                    //create photo
                    Photo p = new();

                    ORMDatabaseModule.MetaData m = new();

                    //Adding data
                    p.Path = image.path;
                    m.DateCreation = image.metaData.DateCreation.ToString();
                    m.Flash = image.metaData.Flash;
                    m.Latitude = image.metaData.Latitude;
                    m.Longitude = image.metaData.Longitude;
                    m.FocusLength = (float)image.metaData.FocalLength;
                    m.Orientation = image.metaData.Orientation;
                    m.Model = image.metaData.Model;
                    m.Manufacturer = image.metaData.Manufacturer;
                    p.MetaData = m;

                    //Create albumcontext for common album
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
                    count++;
                    bar.Value = count;
                }
            }
        }
    }
}
