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
    /// Interaction logic for SkinIniEditorFonts.xaml
    /// </summary>
    public partial class SkinIniEditorFonts : UserControl
    {
        MainWindow mw;
        SkinIniEditor skinIniEditor;
        string skinName;

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child != null && child is T)
                    yield return (T)child;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public SkinIniEditorFonts(MainWindow recievedWindow, string skinName)
        {
            mw = recievedWindow;
            skinIniEditor = new SkinIniEditor();
            this.skinName = skinName;

            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            foreach (var tb in FindVisualChildren<TextBox>(mw))
            {
                string value = skinIniEditor.GetIniData(skinName, "Fonts", tb.Name, true);
                tb.Text = value;
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Name.Contains("Overlap"))
            {
                if (int.TryParse(tb.Text, out _))
                {
                    tb.Background = Brushes.Transparent;
                    skinIniEditor.SkinIniChanged(skinName, "Fonts", tb.Name, tb.Text, false);
                }
                else
                {
                    tb.Background = Brushes.Red;
                }
            }
            else
            {
                skinIniEditor.SkinIniChanged(skinName, "Fonts", tb.Name, tb.Text, false);
            }
        }
    }
}
