using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PhotoManager
{
    public partial class MainWindow : Form
    {
        HelperMainWindow helper;
        public MainWindow()
        {
            helper = new();
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            helper.showData(this.AlbumList);
            //this.ImageListForAlbum.CheckBoxes = true;
        }

        private void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper.ShowPhotoFromAlbum(this.ImageListForAlbum, AlbumList.SelectedItem.ToString());
            this.label2.Visible = false;
            this.moveAlbumBox.Visible = false;
            this.moveButton.Visible = false;
            this.CopyButton.Visible = false;
            helper.addYears(AlbumList.SelectedItem.ToString(), comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AlbumList.SelectedItem != null)
            {
                var mYear = this.comboBox1.SelectedItem;
                string year = helper.normalizeYear(mYear.ToString());
                helper.ShowPhotoFromAlbum(year, this.AlbumList.SelectedItem.ToString(), this.ImageListForAlbum);
            }
            this.label2.Visible = false;
            this.RemoveButton.Visible = false;
            this.moveAlbumBox.Visible = false;
            this.moveButton.Visible = false;
            this.CopyButton.Visible = false;
        }

        private void CreateAlbumButton_Click(object sender, EventArgs e)
        {
            var s = new PhotoManager.GUI.AlbumCreator.Form1();
            s.ShowDialog();
            helper.showData(this.AlbumList);
        }

        private void ImageListForAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImageListForAlbum.SelectedItems.Count != 0)
            {
                this.label2.Visible = true;
                this.moveAlbumBox.Visible = true;
                this.RemoveButton.Visible = true;
                this.moveAlbumBox.Items.Clear();
                foreach(var item in this.AlbumList.Items)
                {
                    this.moveAlbumBox.Items.Add(item.ToString());
                }
            }
            else
            {
                this.label2.Visible = false;
                this.moveAlbumBox.Visible = false;
            }
        }

        private void moveAlbumBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (moveAlbumBox.SelectedItem != null)
            {
                moveButton.Visible = true;
                CopyButton.Visible = true;
                this.RemoveButton.Visible = true;
            }
            else
            {
                moveButton.Visible = false;
                CopyButton.Visible = false;
                this.RemoveButton.Visible = true;
            }
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            var newAlbum = this.moveAlbumBox.SelectedItem;
            var oldAlbum = this.AlbumList.SelectedItem;
            foreach (int index in this.ImageListForAlbum.SelectedIndices)
            {
                helper.MoveToAnotherAlbum(newAlbum.ToString(), oldAlbum.ToString(), this.ImageListForAlbum.Items[index].Text.ToString());
            }
            this.label2.Visible = false;
            this.moveAlbumBox.Visible = false;
            this.moveButton.Visible = false;
            this.CopyButton.Visible = false;
            this.RemoveButton.Visible = false;
            helper.ShowPhotoFromAlbum(this.ImageListForAlbum, oldAlbum.ToString());
            helper.addYears(AlbumList.SelectedItem.ToString(), comboBox1);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var newAlbum = this.moveAlbumBox.SelectedItem;
            foreach(int index in this.ImageListForAlbum.SelectedIndices)
            {
                helper.CopyToAnotherAlbum(newAlbum.ToString(), this.ImageListForAlbum.Items[index].Text.ToString());
            }
            this.label2.Visible = false;
            this.moveAlbumBox.Visible = false;
            this.moveButton.Visible = false;
            this.CopyButton.Visible = false;
            this.RemoveButton.Visible = false;
            helper.ShowPhotoFromAlbum(this.ImageListForAlbum, this.AlbumList.SelectedItem.ToString());
            helper.addYears(AlbumList.SelectedItem.ToString(), comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var album = this.AlbumList.SelectedItem.ToString();
            foreach(int index in this.ImageListForAlbum.SelectedIndices)
            {
                var path = this.ImageListForAlbum.Items[index].Text.ToString();
                helper.Remove(album, path);
            }
            helper.ShowPhotoFromAlbum(this.ImageListForAlbum, this.AlbumList.SelectedItem.ToString());
            helper.addYears(AlbumList.SelectedItem.ToString(), comboBox1);
        }
    }
}
