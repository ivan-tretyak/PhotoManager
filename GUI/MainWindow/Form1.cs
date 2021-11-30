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
            helper.showData(this.AlbumList, this.comboBox1);
        }

        private void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper.ShowPhotoFromAlbum(this.ImageListForAlbum, AlbumList.SelectedItem.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper.showPhotoFromChooseYearCurrentAlbum("0001", "Common", this.ImageListForAlbum);
        }
    }
}
