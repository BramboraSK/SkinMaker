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
using System.Diagnostics;

namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for SkinIniEditorGeneral.xaml
    /// </summary>
    public partial class SkinIniEditorGeneral : UserControl
    {
        string skinName;
        SkinIniEditor skinIniEditor;
        MainWindow mw;

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

        public SkinIniEditorGeneral(MainWindow recievedWindow, string skinName)
        {
            mw = recievedWindow;
            this.skinName = skinName;
            skinIniEditor = new SkinIniEditor();

            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            InitializeValues();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            skinIniEditor.SkinIniChanged(skinName, "General", ((CheckBox)sender).Name.ToString(), "1");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            skinIniEditor.SkinIniChanged(skinName, "General", ((CheckBox)sender).Name.ToString(), "0");
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            skinIniEditor.SkinIniChanged(skinName, "General", ((TextBox)sender).Name.ToString(), ((TextBox)sender).Text);
        }

        private void InitializeValues()
        {
            foreach (var tb in FindVisualChildren<TextBox>(mw))
            {
                tb.Text = skinIniEditor.GetIniData(skinName, "General", tb.Name.ToString());
            }

            foreach (var chb in FindVisualChildren<CheckBox>(mw))
            {
                if (skinIniEditor.GetIniData(skinName, "General", chb.Name.ToString()) == "1")
                {
                    chb.IsChecked = true;
                }
                else
                {
                    chb.IsChecked = false;
                }
            }
        }
    }
}
