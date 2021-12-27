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
        EditorContent ec;
        string filter;

        public EditImagesContent(string skinName, EditorContent ec, string lastSelected, string filter = null)
        {
            this.filter = filter;
            this.ec = ec;
            this.skinName = skinName;
            InitializeComponent();
            FillImageBox();

            if (lastSelected != null && ImageList.Items.Contains(lastSelected))
            {
                ImageList.SelectedItem = lastSelected;
            }
        }

        private void FillImageBox()
        {
            foreach (string filename in Directory.GetFiles(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName)).Where(filename => filename.EndsWith(".png") || filename.EndsWith(".jpg")))
            {
                if (filter != null)
                {
                    if(filename.Contains(filter)) ImageList.Items.Add(Path.GetFileName(filename));
                }
                else
                {
                    ImageList.Items.Add(Path.GetFileName(filename));
                }
            }
        }

        private void ImageList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string imgSource = Path.Join(OptionsLoader.options.SkinsFolderPath, skinName, ImageList.SelectedItem.ToString());
            int[] imgDim = EditImages.GetImageDim(imgSource);

            BitmapImage bmi = new();
            bmi.BeginInit();
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.UriSource = new Uri(imgSource);
            bmi.EndInit();

            Preview.Source = bmi;

            double ratio;

            if (imgDim[1] < imgDim[0])
            {
                ratio = ((double)imgDim[1]) / ((double)imgDim[0]);
                Preview.MaxWidth = 180;
                Preview.MaxHeight = Preview.MaxWidth * ratio;
                PreviewWidth.Margin = new Thickness(600, PreviewWidth.Margin.Top, 0, 0);
                PreviewHeight.Margin = new Thickness(PreviewHeight.Margin.Left, 132 - (180 - Preview.MaxHeight) / 2, 0, 0);
            }
            else
            {
                ratio = ((double)imgDim[0]) / ((double)imgDim[1]);
                Preview.MaxHeight = 180;
                Preview.MaxWidth = Preview.MaxHeight * ratio;
                PreviewWidth.Margin = new Thickness(600 - (180 - Preview.MaxWidth) / 2, PreviewWidth.Margin.Top, 0, 0);
                PreviewHeight.Margin = new Thickness(PreviewHeight.Margin.Left, 132, 0, 0);
            }

            ec.lastSelected = ImageList.SelectedItem.ToString();
            PreviewWidth.Content = imgDim[0].ToString();
            PreviewHeight.Content = imgDim[1].ToString();

            PreviewBorder.MaxWidth = Preview.MaxWidth;
            PreviewBorder.MaxHeight = Preview.MaxHeight;

            DescBox.Text = AddFileLoader.GetFileDesc(Path.GetFileNameWithoutExtension(ImageList.SelectedItem.ToString()));
        }

        private void EditButon_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.SelectedItem != null && File.Exists(OptionsLoader.options.ImageEditorPath))
            {
                Process.Start(OptionsLoader.options.ImageEditorPath, $"\"{Path.Join(OptionsLoader.options.SkinsFolderPath, skinName, ImageList.SelectedItem.ToString())}\"");
            }
        }
    }
}
