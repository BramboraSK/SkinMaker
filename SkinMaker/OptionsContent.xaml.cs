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
        public OptionsContent()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MainContent();
        }

        private void Shutdown_MouseDown(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize_MouseDown(object sender, RoutedEventArgs e)
        {
            MainWindow.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void BrowseSkinsFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                SkinsFolderPath.Text = dialog.FileName;
            }

        }
    }
}
