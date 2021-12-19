using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for EditImagesContent.xaml
    /// </summary>
    public partial class EditImagesContent : UserControl
    {
        string skinName;

        public EditImagesContent(string skinName)
        {
            this.skinName = skinName;
            InitializeComponent();

            foreach(string filename in Directory.GetFiles(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName)).Where(filename => filename.EndsWith(".png") || filename.EndsWith(".jpg")))
            {
                ImageList.Items.Add(Path.GetFileName(filename));
            }
        }

        private void ImageList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Preview.Source = new BitmapImage(new Uri(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName, ImageList.SelectedItem.ToString())));
        }
    }
}
