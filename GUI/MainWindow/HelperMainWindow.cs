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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ORMDatabaseModule;
using PhotoManager.GUI.ChooseDirectoryToSync;


namespace PhotoManager
{
    public static class SmallUtils
    {
        public static string NormalizeYear(string year)
        {
            while (year.Length < 4)
            {
                year = "0" + year;
            }
            return year;
        }
    }

    public class HelperMainWindow
    {
        MainWindow myForm;
        DbHelper DBhelper;
        public HelperMainWindow(MainWindow form)
        {
            myForm = form;
            DBhelper = new();
        }

       public void showData()
        {
           if (myForm.AlbumList.Nodes.Count != 0)
            {
                myForm.AlbumList.Nodes.Clear();
            }
            try
            {
                ShowAlbums(DBhelper.GetAlbums());
                addYears();
            }
            catch (Exception)
            {
                var s = new PhotoManager.GUI.ChooseDirectoryToSync.Form1();
                s.ShowDialog();
                showData();
            }
        }

        public void ShowAlbums(List<Album> albums)
        {
            //Show albums
            foreach (var album in albums)
            {
                myForm.AlbumList.Nodes.Add(album.Name);
            }
        }

        public void addYears()
        {
            //treeView.Items.Clear();
            myForm.AlbumList.Nodes.Clear();
            ShowAlbums(DBhelper.GetAlbums());
            for (int i = 0; i < myForm.AlbumList.Nodes.Count; i++)
            {
                //Select metadata
                var metadatas = DBhelper.GetMetaDatas(myForm.AlbumList.Nodes[i].Text);

                //Get years
                List<string> years = new();

                foreach (var metadata in metadatas)
                {
                    var date = DateTime.Parse(metadata.DateCreation);
                    years.Add(date.Year.ToString());
                }

                var uniqueYears = years.Distinct();
                foreach (var year in uniqueYears)
                {
                    myForm.AlbumList.Nodes[i].Nodes.Add(year);
                }
            }
        }

        public void ShowPhotoFromAlbum(string albumName)
        {
           DisplayImage(DBhelper.GetPhotos(DBhelper.GetAlbumContexts(albumName)));
        }

        private void DisplayImage(List<Photo> photos)
        {
            int count = 0;
            //Delete LargeImageList
            if (myForm.ImageListForAlbum.LargeImageList != null)
            {
                myForm.ImageListForAlbum.LargeImageList.Dispose();
            }
            myForm.ImageListForAlbum.Items.Clear();
            //Create image list
            myForm.ImageListForAlbum.View = View.LargeIcon;
            var LargeImageList = new ImageList();
            LargeImageList.ImageSize = new Size(256, 256);
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;

            //create count
            myForm.UseWaitCursor = true;
            foreach(var photo in photos)
            {
                try
                {
                    //Create photo preview
                    //Get orientation
                    int orientation = 0;
                    using (var db = new DatabaseContext())
                    {
                        var metadata = db.MetaDatas
                            .Where(m => m.MetadataId == photo.MetaDataId)
                            .First();
                        orientation = metadata.Orientation;
                    }
                    var i = new ImagesPreview(photo.Path, orientation);
                    var thumbnail =i.thumbnail();
                    myForm.ImageListForAlbum.Items.Add(photo.Path, count);
                    count++;
                    LargeImageList.Images.Add(photo.Path, thumbnail);
                    if (photo.Exist == 1)
                    {
                        DBhelper.UpdateExist(photo, 0);
                    }
                    if (count % 10 == 0)
                    {
                        myForm.ImageListForAlbum.LargeImageList = LargeImageList;
                        myForm.ImageListForAlbum.Update();
                    }
                }
                catch (Exception)
                {
                    DBhelper.UpdateExist(photo, 1);
                }
            }
            myForm.ImageListForAlbum.Update();
            myForm.ImageListForAlbum.LargeImageList = LargeImageList;
            myForm.UseWaitCursor = false;
        }

        public void ShowPhotoFromAlbum(string year, string albumName)
        {
            DisplayImage(DBhelper.GetPhotos(DBhelper.GetAlbumContexts(albumName), year));
        }
    }
}
