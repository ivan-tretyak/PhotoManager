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
using System.Windows.Forms;
using ORMDatabaseModule;

namespace PhotoManager.GUI.AlbumCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    var album = new Album();
                    album.Name = this.textBox1.Text;
                    album.DateCreation = DateTime.Now.ToString();
                    db.Add(album);
                    db.SaveChanges();
                    this.Close();
                }
                catch (Exception)
                {
                    this.label2.Text = AlbumCreationMessage.AlreadyExist;
                }
            }
        }
    }
}
