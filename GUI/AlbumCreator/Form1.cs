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
