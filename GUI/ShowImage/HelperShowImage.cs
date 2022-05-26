﻿//PhotoManager. Program to organize your photo to album.
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

using Microsoft.Win32;
using ORMDatabaseModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhotoManager.GUI.ShowImage
{
    public static class HelperShowImage
    {
        public static void resizer(MyPanel panel, PictureBox pictureBox)
        {
            int x = panel.Size.Width;
            int y = panel.Size.Height;
            x -= 10;
            y -= 10;
            Size size = new();
            size.Width = x;
            size.Height = y;
            pictureBox.Size = size;
        }

        public static void ShowImage(PictureBox pictureBox, PictureBox org, string path, Form form)
        {
            path = GetFullPathForImage(path);
            pictureBox.Image = new Bitmap(path);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            org.Load(path);

            using (DatabaseContext db = new())
            {
                var photo = db.Photos
                    .Where(p => p.Path == Path.GetFileName(path))
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();

                pictureBox.Image.RotateFlip(OrientationToFlipType(metadata.Orientation));
            }
            form.Text = path;
        }

        public static string GetFullPathForImage(string path)
        {
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey registry = currentUser.OpenSubKey("appPhotoOrginizer");
            var pathToSearchKey = registry.GetValue("FolderSync");
            var pathToSearch = pathToSearchKey.ToString();

            path = $"{pathToSearch}{System.IO.Path.DirectorySeparatorChar}{path}";
            return path;
        }

        public static RotateFlipType OrientationToFlipType(int orientation)
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

        public static MetaData LoadMetadata(string path)
        {
            using (var db = new DatabaseContext())
            {
                var photo = db.Photos
                    .Where(p => p.Path == path)
                    .First();

                var metadata = db.MetaDatas
                    .Where(m => m.MetadataId == photo.MetaDataId)
                    .First();
                return metadata;
            }
        }

        public static List<String> LoadKeyWords(string path)
        {
            using (var db = new DatabaseContext())
            {
                var photo = db.Photos
                    .Where(p => p.Path == Path.GetFileName(path))
                    .First();
                var keyWordsLists = db.keyWordsLists
                    .Where(kL => kL.PhotoId == photo.PhotoId)
                    .ToList();
                List<String> keyWords = new();
                foreach (var keyWordList in keyWordsLists)
                {
                    var keyWord = db.KeyWords
                        .Where(kW => kW.KeyWordsId == keyWordList.KeyWordsId)
                        .First();
                    keyWords.Add(keyWord.KeyWord);
                }
                return keyWords;
            }
        } 

        public static void UpdateMetadata(MetaData metaData)
        {
            using (var db = new DatabaseContext())
            {
                db.MetaDatas.Update(metaData);
                db.SaveChanges();
            }
        }

        public static void RotateImage(PictureBox pictureBox, MyPanel panel, TrackBar track, bool clockwised)
        {
            var image = pictureBox.Image;
            if (clockwised)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
            {
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            pictureBox.Image = image;
            pictureBox.Size = new Size(panel.Size.Width - 10, panel.Size.Height - 10);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            track.Value = 100;
        }

        public static void ResizeImage(TrackBar track, PictureBox pictureBox, PictureBox org, MyPanel panel, string path, Form form)
        {
            path = GetFullPathForImage(path);
            if (track.Value > 0)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                panel.AutoScroll = true;
                panel.HorizontalScroll.Visible = true;
                panel.VerticalScroll.Visible = true;
                var img = new Bitmap(path);
                Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * track.Value / 100), Convert.ToInt32(img.Height * track.Value / 100));
                Graphics gpu = Graphics.FromImage(bm);
                gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                pictureBox.Image.Dispose();
                pictureBox.Image = bm;
                gpu.Dispose();

                using (DatabaseContext db = new())
                {
                    var photo = db.Photos
                        .Where(p => p.Path == Path.GetFileName(path))
                        .First();

                    var metadata = db.MetaDatas
                        .Where(m => m.MetadataId == photo.MetaDataId)
                        .First();

                    pictureBox.Image.RotateFlip(OrientationToFlipType(metadata.Orientation));
                }
            }
            else
            {
                ShowImage(pictureBox, org, path, form);
                panel.AutoScroll = false;
            }
        }

        public static void ReplaceLabelTextBox(TextBox textBox, Label label, int row, EventHandler eventHandler, TableLayoutPanel tableLayoutPanel)
        {
            textBox.Text = label.Text;
            textBox.Location = new Point(103, 120);
            label.Dispose();
            tableLayoutPanel.Controls.Add(textBox, 1, row);
            textBox.DoubleClick += new EventHandler(eventHandler);
        }

        public static void AddLinkKeyWordPhoto(string[] keyWords, string path)
        {
            using (var db = new DatabaseContext())
            {
                foreach(var keyWord in keyWords)
                {
                    var tag = db.KeyWords
                        .Where(k => k.KeyWord == keyWord)
                        .First();

                    var photo = db.Photos
                        .Where(p => p.Path == Path.GetFileName(path) && p.Exist == 0)
                        .First();

                    var oldTags = db.keyWordsLists
                        .Where(kwl => kwl.PhotoId == photo.PhotoId)
                        .ToList();

                    foreach(var oldTag in oldTags)
                    {
                        db.Remove(oldTag);
                        db.SaveChanges();
                    }

                    var tagCount = db.keyWordsLists
                        .Where(kwl => kwl.KeyWordsId == tag.KeyWordsId && kwl.PhotoId == photo.PhotoId)
                        .FirstOrDefault();
                    if (tagCount is null)
                    {
                        var tagList = new KeyWordsList();
                        tagList.Photo = photo;
                        tagList.KeyWords = tag;

                        db.Add(tagList);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void AddNewKeyWords(string[] keyWords)
        {
            if (keyWords is null)
            {
                return;
            }

            using (var db = new DatabaseContext())
            {
                foreach (var KeyWord in keyWords)
                {
                    var checkKeyWord = db.KeyWords
                        .Where(k => k.KeyWord == KeyWord)
                        .Count();
                    if (checkKeyWord == 0)
                    {
                        var keyWord = new KeyWords();
                        keyWord.KeyWord = KeyWord;
                        db.Add(keyWord);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
