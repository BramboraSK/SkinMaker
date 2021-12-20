using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;
using System.Drawing.Imaging;

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
            FillOsuStdListbox();

            Editing.Text = $"Editing: {skinName}";
            editorControl.Content = new EditImagesContent(skinName);
        }

        private void Convert2x_Click(object sender, RoutedEventArgs e)
        {
            foreach (string filename in Directory.EnumerateFiles(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName)))
            {
                if (filename.Contains("@2x"))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(filename);
                    Bitmap bm = new(new Bitmap(img), new System.Drawing.Size(img.Width > 1 ? img.Width / 2 : 1, img.Height > 1 ? img.Height / 2 : 1));
                    img.Dispose();

                    bm.Save(filename.Replace("@2x", ""), filename.EndsWith(".png") ? ImageFormat.Png : ImageFormat.Jpeg);
                }
            }

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

        private void FillOsuStdListbox()
        {
            ListBoxItem item = null;
            foreach (AddFileLoader.OsuStdFilesContent file in AddFileLoader.content.OsuStdFiles)
            {
                item = new ListBoxItem();
                item.Content = file.Name;
                item.MouseEnter += new MouseEventHandler(item_MouseEnter);
                osuStdListbox.Items.Add(item);
            }
        }
         
        private void item_MouseEnter(object sender, MouseEventArgs e)
        {
            DescBox.Text = AddFileLoader.GetFileDesc(((ListBoxItem)sender).Content.ToString());
            MenuDescPopup.IsOpen = true;
        }

        private void osuStdListbox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MenuDescPopup.IsOpen = false;
        }
    }

    public class OsuStdMenuContent
    {
        public string Title { get; set; }
    }


}
