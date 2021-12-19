using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SkinMaker
{
    /// <summary>
    /// Interakční logika pro MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        MainWindow mw;

        public MainContent(MainWindow recievedWindow)
        {
            mw = recievedWindow;
            InitializeComponent();
        }

        private void NewSkin_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new NewSkinContent(mw);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new OptionsContent(mw);
        }

        private void OpenSkin_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                IsFolderPicker = true,
                InitialDirectory = OptionsLoader.options.SkinsFolderPath
            };

            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) mw.contentControl.Content = new EditorContent(mw, Path.GetFileName(dialog.FileName));
        }
    }
}
