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
    /// Interakční logika pro EditorContent.xaml
    /// </summary>
    public partial class EditorContent : UserControl
    {
        MainWindow mw;
        string skinName;
        public EditorContent(MainWindow recievedWindow, string skinName)
        {
            this.skinName = skinName;
            mw = recievedWindow;

            InitializeComponent();

            Editing.Text = $"Editing: {skinName}";
            editorControl.Content = new EditImagesContent(skinName);
        }
    }
}
