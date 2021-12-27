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
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Windows.Threading;
using menelabs.core;

namespace SkinMaker
{
    /// <summary>
    /// Interakční logika pro EditorContent.xaml
    /// </summary>
    public partial class EditorContent : UserControl
    {
        public string lastSelected;
        private ObservableCollection<OsuStdMenuContent> _osuStdContent = new();
        MainWindow mw;
        string skinName;
        FileSystemSafeWatcher watcher;
        public EditorContent(MainWindow recievedWindow, string skinName)
        {
            this.skinName = skinName;
            mw = recievedWindow;

            InitializeWatcher();
            InitializeComponent();
            FillOsuStdListbox();
            FillGameplayListbox();

            Editing.Text = $"Editing: {skinName}";
            editorControl.Content = new EditImagesContent(skinName, this, lastSelected); 
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
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() => editorControl.Content = new EditImagesContent(skinName, this, lastSelected)));

        }

        public ObservableCollection<OsuStdMenuContent> OsuStdContent
        {
            get { return _osuStdContent; }
            set { _osuStdContent = value; }
        }

        public class OsuStdMenuContent
        {
            public string Title { get; set; }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new MainContent(mw);
        }

        private void FillOsuStdListbox()
        {
            ListBoxItem item = null;
            foreach (AddFileLoader.FilesContent file in AddFileLoader.content.OsuStdFiles)
            {
                item = new ListBoxItem();
                item.Content = file.Name;
                item.MouseEnter += new MouseEventHandler(item_MouseEnter);
                item.Selected += new RoutedEventHandler(item_Selected);
                OsuStdFiles.Items.Add(item);
            }
        }

        private void FillGameplayListbox()
        {
            ListBoxItem item = null;
            foreach (AddFileLoader.FilesContent file in AddFileLoader.content.GameplayFiles)
            {
                item = new ListBoxItem();
                item.Content = file.Name;
                item.MouseEnter += new MouseEventHandler(item_MouseEnter);
                item.Selected += new RoutedEventHandler(item_Selected);
                GameplayFiles.Items.Add(item);
            }
        }

        private void item_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            AddFileLoader.CreateSelectedFile(item.Content.ToString(), skinName, ((ListBox)item.Parent).Name.ToString());
        }


        private void Remove2x_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Warning! This will DELETE all images with '@2x' in their name. This action can't be undone. Do you want to continue?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            foreach (string filename in Directory.EnumerateFiles(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName)))
            {
                if (filename.Contains("@2x"))
                {
                    FileSystem.DeleteFile(filename, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
            }
        }
            
        private void item_MouseEnter(object sender, MouseEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            DescBox.Text = AddFileLoader.GetFileDesc(item.Content.ToString(), ((ListBox)item.Parent).Name.ToString());
            MenuDescPopup.PlacementTarget = (ListBox)item.Parent;
            MenuDescPopup.IsOpen = true;

            UpdatePreviewImage(item);
        }

        private void UpdatePreviewImage(ListBoxItem item)
        {
            string imgSource = Path.Join("Templates", "Skins", "osu!Default", item.Content.ToString() + ".png");

            BitmapImage bmi = new();
            bmi.BeginInit();

            if (!File.Exists(imgSource))
            {
                bmi.UriSource = null;
                PreviewImage.Source = bmi;
                return;
            }

            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.UriSource = new Uri(imgSource, UriKind.Relative);
            bmi.EndInit();
                
            PreviewImage.Source = bmi;
        }

        private void osuStdListbox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MenuDescPopup.IsOpen = false;
        }

        private void SkinIniEditorButton_Click(object sender, RoutedEventArgs e)
        {
            mw.contentControl.Content = new SkinIniEditorContent(mw, skinName);
        }

        private void Editing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", Path.Join(OptionsLoader.options.SkinsFolderPath, skinName));
        }

        private void InitializeWatcher()
        {
            watcher = new(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName));

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
        }

        private void ExportOskButton_Click(object sender, RoutedEventArgs e)
        {
            SkinExport.ExportOsk(skinName);
        }

        private void SearchTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchTextBox.Foreground = System.Windows.Media.Brushes.White;

            if (SearchTextBox.Text.Contains("Search"))
            {
                SearchTextBox.Text = "";
            }

        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "" || SearchTextBox.Text.Contains("Search"))
            {
                SearchTextBox.Foreground = System.Windows.Media.Brushes.Gray;
                SearchTextBox.Text = "Search...";
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text != "Search...")
            {
                editorControl.Content = new EditImagesContent(skinName, this, lastSelected, SearchTextBox.Text);
            }
        }
    }
}
