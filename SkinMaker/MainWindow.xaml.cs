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
using System.Windows.Shapes;

namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomChromeLibrary.CustomChromeWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            new OptionsLoader().Init();

            if(Directory.Exists(OptionsLoader.options.SkinsFolderPath))
            {
                contentControl.Content = new MainContent(this);
            }
            else
            {
                contentControl.Content = new InvalidPathContent(this);
            }
        }

        private void Shutdown_MouseDown(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize_MouseDown(object sender, RoutedEventArgs e)
        {
            MainWindow.GetWindow(this).WindowState = WindowState.Minimized;
        }
    }
}
