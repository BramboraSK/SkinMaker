using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SkinMaker
{
    /// <summary>
    /// Interakční logika pro OptionsContent.xaml
    /// </summary>
    public partial class OptionsContent : UserControl
    {
        MainWindow mw;

        public OptionsContent(MainWindow recievedWindow)
        {
            mw = recievedWindow;
            InitializeComponent();

            SkinsFolderPath.Text = OptionsLoader.options.SkinsFolderPath;
            ImageEditorPath.Text = OptionsLoader.options.ImageEditorPath;
            OsuFolderPath.Text = OptionsLoader.options.OsuFolderPath;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSaved())
            {
                var mb = MessageBox.Show("Options were not saved. Would you like to save them now?", "", MessageBoxButton.YesNoCancel);

                switch (mb)
                {
                    case MessageBoxResult.Yes:
                        if(!SaveOptions()) return;
                        break;

                    case MessageBoxResult.No:
                        break;

                    default:
                        return;
                }
            }

            if (Directory.Exists(OptionsLoader.options.SkinsFolderPath))
            {
                mw.contentControl.Content = new MainContent(mw);
            }
            else
            {
                mw.contentControl.Content = new InvalidPathContent(mw);
            }
        }

        private void BrowseSkinsFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) SkinsFolderPath.Text = dialog.FileName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveOptions();
        }

        private bool SaveOptions()
        {
            if (CheckOsuExe() && CheckEditorExe())
            {
                OptionsLoader.options.OsuFolderPath = OsuFolderPath.Text;
                OptionsLoader.options.SkinsFolderPath = SkinsFolderPath.Text;
                OptionsLoader.options.ImageEditorPath = ImageEditorPath.Text;
                OptionsLoader.Save();
                return true;
            }
            else
            {
                if (!CheckOsuExe())
                {
                    OsuFolderPath.Background = Brushes.Red;
                }

                if (!CheckEditorExe())
                {
                    ImageEditorPath.Background = Brushes.Red;
                }

                return false;
            }
            
        }

        private bool CheckOsuExe()
        {
            return File.Exists(System.IO.Path.Join(OsuFolderPath.Text, "osu!.exe"));
        }

        private bool CheckEditorExe()
        {
            if (ImageEditorPath.Text.EndsWith(".exe"))
            {
                return true;
            }

            return false;
        }

        private void BrowseImageEditorPath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) ImageEditorPath.Text = dialog.FileName;
        }

        private bool CheckSaved()
        {
            if (OptionsLoader.options.SkinsFolderPath == SkinsFolderPath.Text && OptionsLoader.options.ImageEditorPath == ImageEditorPath.Text && OptionsLoader.options.OsuFolderPath == OsuFolderPath.Text)
            {
                return true;
            }
            
            return false;
        }

        private void BrowseOsuPath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OsuFolderPath.Text = dialog.FileName;
            }   
        }

        private void OsuFolderPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            OsuFolderPath.Background = Brushes.Transparent;
            SkinsFolderPath.Text = System.IO.Path.Join(OsuFolderPath.Text, "Skins");
        }

        private void ImageEditorPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            ImageEditorPath.Background = Brushes.Transparent;
        }
    }
}
