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
    /// Interaction logic for SkinIniEditorManual.xaml
    /// </summary>
    public partial class SkinIniEditorManual : UserControl
    {
        MainWindow mw;
        string skinName;

        public SkinIniEditorManual(MainWindow recievedWindow, string skinName)
        {
            mw = recievedWindow;
            this.skinName = skinName;

            InitializeComponent();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            EditorTextBox.Text = File.ReadAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            File.WriteAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini", EditorTextBox.Text);
        }
    }
}
