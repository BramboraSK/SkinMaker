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
using System.IO;

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
            LoadTemplates();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new MainContent(mw);
        }

        private void CreateSkin_Click(object sender, RoutedEventArgs e)
        {    
            if (SkinName.Text.Length > 0)
            {
                SkinCreate.CreateSkin(new Skin(SkinName.Text, SkinAuthor.Text), ChooseSkinTemplateButton.Content.ToString());
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
            ChooseSkinTemplateButton.Visibility = Visibility.Hidden;
            TemplateList.Visibility = Visibility.Visible;
        }

        private void LoadTemplates()
        {
            string[] skins = Directory.GetDirectories(OptionsLoader.options.SkinsFolderPath);

            TemplateList.Items.Add("(Empty)");

            foreach (string skin in skins)
            {
                TemplateList.Items.Add(Path.GetFileName(skin));
            }
        }

        private void SelectTemplate(object sender, RoutedEventArgs e)
        {
            ChooseSkinTemplateButton.Content = TemplateList.SelectedItem;
            ChooseSkinTemplateButton.Visibility = Visibility.Visible;
            TemplateList.Visibility = Visibility.Hidden;
        }
    }
}
