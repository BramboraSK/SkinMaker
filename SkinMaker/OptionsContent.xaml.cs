﻿using System;
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
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new MainContent(mw);
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
            OptionsLoader.options.SkinsFolderPath = SkinsFolderPath.Text;
            OptionsLoader.Save();
        }
    }
}
