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
    /// Interakční logika pro NewSkinContent.xaml
    /// </summary>
    public partial class NewSkinContent : UserControl
    {
        MainWindow mw;

        public class Skin
        {
            public string name;
            public string author;

            public Skin(string name, string author)
            {
                this.name = name;
                this.author = author;
            }
        }

        public NewSkinContent(MainWindow recievedWindow)
        {
            mw = recievedWindow;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new MainContent(mw);
        }

        private void CreateSkin_Click(object sender, RoutedEventArgs e)
        {    
            if (SkinName.Text.Length > 0)
            {
                SkinCreate.CreateSkin(new Skin(SkinName.Text, SkinAuthor.Text));
                mw.contentControl.Content = new EditorContent(mw, SkinName.Text);
            }
            else
            {
                SkinName.Background = Brushes.DarkRed;
            }

        }

        private void SkinName_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SkinName.Background == Brushes.DarkRed)
            {
                SkinName.Background = Brushes.Transparent;
            }
        }

        private void ChooseSkinTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            TemplateList.Visibility = Visibility.Visible;
        }
    }
}
