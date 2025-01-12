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

namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for InvalidPathControl.xaml
    /// </summary>
    public partial class InvalidPathContent : UserControl
    {
        MainWindow mw;

        public InvalidPathContent(MainWindow recievedWindow)
        {
            mw = recievedWindow;
            InitializeComponent();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new OptionsContent(mw);
        }
    }
}
