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

namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for SkinIniEditorContent.xaml
    /// </summary>
    public partial class SkinIniEditorContent : UserControl
    {
        MainWindow mw;
        string skinName;
        public SkinIniEditorContent(MainWindow recievedWindow, string skinName)
        {
            this.skinName = skinName;
            this.mw = recievedWindow;

            InitializeComponent();

            Editing.Text = $@"Editing: {skinName}\skin.ini";
            SectionController.Content = new SkinIniEditorGeneral(mw, skinName);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new EditorContent(mw, skinName);
        }

        private void GeneralButton_Click(object sender, RoutedEventArgs e)
        {
            if (SectionController.Content != new SkinIniEditorGeneral(mw, skinName))
            {
                SectionController.Content = new SkinIniEditorGeneral(mw, skinName);
            }
        }

        private void ColoursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FontButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
