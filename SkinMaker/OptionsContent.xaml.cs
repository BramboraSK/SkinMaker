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
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSaved())
            {
                var mb = MessageBox.Show("Options were not saved. Would you like to save them now?", "", MessageBoxButton.YesNoCancel);

                switch (mb)
                {
                    case MessageBoxResult.Yes:
                        OptionsLoader.options.SkinsFolderPath = SkinsFolderPath.Text;
                        OptionsLoader.options.ImageEditorPath = ImageEditorPath.Text;
                        OptionsLoader.Save();
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

            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) SkinsFolderPath.Text = dialog.FileName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OptionsLoader.options.SkinsFolderPath = SkinsFolderPath.Text;
            OptionsLoader.options.ImageEditorPath = ImageEditorPath.Text;
            OptionsLoader.Save();
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
            if (OptionsLoader.options.SkinsFolderPath == SkinsFolderPath.Text && OptionsLoader.options.ImageEditorPath == ImageEditorPath.Text)
            {
                return true;
            }
            
            return false;
        }
    }
}
