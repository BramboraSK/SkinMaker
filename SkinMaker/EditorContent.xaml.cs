using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro EditorContent.xaml
    /// </summary>
    public partial class EditorContent : UserControl
    {
        private ObservableCollection<OsuStdMenuContent> _osuStdContent = new();
        MainWindow mw;
        string skinName;
        public EditorContent(MainWindow recievedWindow, string skinName)
        {
            this.skinName = skinName;
            mw = recievedWindow;

            InitializeComponent();
            FillOsuStdContentMenu();

            Editing.Text = $"Editing: {skinName}";
            editorControl.Content = new EditImagesContent(skinName);
        }
        public ObservableCollection<OsuStdMenuContent> OsuStdContent
        {
            get { return _osuStdContent; }
            set { _osuStdContent = value; }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new MainContent(mw);
        }

        private void FillOsuStdContentMenu()
        {
            AddFileLoader.LoadMenuContentFile();

            foreach (string file in AddFileLoader.content.OsuStdFiles)
            {
                OsuStdContent.Add(new OsuStdMenuContent { Title = $"{file}.png" });
            }
        }
    }

    public class OsuStdMenuContent
    {
        public string Title { get; set; }
    }


}
