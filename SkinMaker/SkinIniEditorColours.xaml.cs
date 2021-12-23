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
using System.Drawing;


namespace SkinMaker
{
    /// <summary>
    /// Interaction logic for SkinIniEditorColours.xaml
    /// </summary>
    public partial class SkinIniEditorColours : UserControl
    {
        MainWindow mw;
        SkinIniEditor skinIniEditor;
        string skinName;
        TextBox lastSelected;

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

        public SkinIniEditorColours(MainWindow recievedWindow, string skinName)
        {
            mw = recievedWindow;
            this.skinName = skinName;
            skinIniEditor = new SkinIniEditor();

            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            InitializeValues();

            lastSelected = null;

            RGBPicker.R = RGBPicker.B = RGBPicker.G = 0;
        }

        private void TextBox_UpdateSelected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lastSelected = (TextBox)sender;
            }
            catch
            {
                return;
            }

            string value = skinIniEditor.GetIniData(skinName, "Colours", lastSelected.Name, true);

            if (value != null)
            {
                UpdateRGBPicker(value);
            }

            UpdateRectangle(lastSelected.Name);
            ColorPickerLabel.Content = $"Editing: {lastSelected.Name}";
        }

        private void UpdateRGBPicker(string value)
        {
            string[] RGBField = value.Split(',');

                RGBPicker.R = Byte.Parse(RGBField[0]);
                RGBPicker.G = Byte.Parse(RGBField[1]);
                RGBPicker.B = Byte.Parse(RGBField[2]);
        }

        private bool IsValidString(string[] input)
        {
            Byte smrad;

            if (input.Length == 3)
            {
                foreach (string item in input)
                {
                    if (item == "" || !Byte.TryParse(item, out smrad))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (lastSelected != null && IsValidString(lastSelected.Text.Split(',')))
            {
                skinIniEditor.SkinIniChanged(skinName, "Colours", lastSelected.Name, lastSelected.Text);

                if (lastSelected.Text != $"{RGBPicker.R},{RGBPicker.G},{RGBPicker.B}")
                {
                    Debug.WriteLine(lastSelected.Text);
                    UpdateRGBPicker(lastSelected.Text);
                }
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (lastSelected == null)
            {
                return;
            }

            lastSelected.Text = $"{RGBPicker.R},{RGBPicker.G},{RGBPicker.B}";
            UpdateRectangle(lastSelected.Name);
        }


        private void UpdateRectangle(string name)
        {
            object obj = FindName($"{name}Frame");
            
            if (obj is Frame)
            {
                Frame wantedFrame = obj as Frame;
                wantedFrame.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, RGBPicker.R, RGBPicker.G, RGBPicker.B));
            }
        }

        private void HideTextBoxLabel(string tbName)
        {
            object obj = FindName($"{tbName}Label");

            if (obj is Label)
            {
                Label wantedLabel = obj as Label;
                wantedLabel.Visibility = Visibility.Hidden;
            }
        }

        private void InitializeValues()
        {
            foreach (var tb in FindVisualChildren<TextBox>(mw))
            {
                string value = skinIniEditor.GetIniData(skinName, "Colours", tb.Name, true);

                if (value == null && tb.Name != "Combo1")
                {
                    tb.Visibility = Visibility.Hidden;
                    HideTextBoxLabel(tb.Name);
                }
                else
                {
                    value = value ?? skinIniEditor.GetIniData(skinName, "Colours", tb.Name, false);
                    tb.Text = value;
                    lastSelected = (TextBox)tb;
                    UpdateRGBPicker(value);
                }

            }
        }
    }
}
