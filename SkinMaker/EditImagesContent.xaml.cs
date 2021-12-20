using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Diagnostics;

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
            string imgSource = Path.Join(OptionsLoader.options.SkinsFolderPath, skinName, ImageList.SelectedItem.ToString());
            string[] imgDim = EditImages.GetImageDim(imgSource);

            Preview.Source = new BitmapImage(new Uri(imgSource));

            PreviewWidth.Content = imgDim[0];
            PreviewHeight.Content = imgDim[1];
        }

        private void EditButon_Click(object sender, RoutedEventArgs e)
        {
            if(ImageList.SelectedItem != null && File.Exists(OptionsLoader.options.ImageEditorPath))
            {
                Process.Start(OptionsLoader.options.ImageEditorPath, $"\"{Path.Join(OptionsLoader.options.SkinsFolderPath, skinName, ImageList.SelectedItem.ToString())}\"");
            }
        }
    }
}
