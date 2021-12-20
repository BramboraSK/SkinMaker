using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Drawing.Imaging;

namespace SkinMaker
{
    /// <summary>
    /// Interakční logika pro EditorContent.xaml
    /// </summary>
    public partial class EditorContent : UserControl
    {
        MainWindow mw;
        string skinName;
        public EditorContent(MainWindow recievedWindow, string skinName)
        {
            this.skinName = skinName;
            mw = recievedWindow;

            InitializeComponent();

            Editing.Text = $"Editing: {skinName}";
            editorControl.Content = new EditImagesContent(skinName);
        }

        private void Convert2x_Click(object sender, RoutedEventArgs e)
        {
            foreach(string filename in Directory.EnumerateFiles(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName)))
            {
                if(filename.Contains("@2x"))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(filename);
                    new Bitmap(new Bitmap(img), new System.Drawing.Size(img.Width / 2, img.Height / 2)).Save(filename.Replace("@2x", ""), filename.EndsWith(".png") ? ImageFormat.Png : ImageFormat.Jpeg);
                }
            }

            editorControl.Content = new EditImagesContent(skinName);
        }
    }
}
