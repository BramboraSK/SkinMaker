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

            if (GetLastComboColour() > 5)
            {
                AddComboColourButton.Visibility = Visibility.Hidden;
            }

            if (GetLastComboColour() < 3)
            {
                RemoveComboColourButton.Visibility = Visibility.Hidden;
            }
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

            string value = skinIniEditor.GetIniData(skinName, "Colours", lastSelected.Name, false);

            if (value != null && IsValidString(value.Split(',')))
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
            Byte output;

            if (input.Length == 3)
            {
                foreach (string item in input)
                {
                    if (item == "" || !Byte.TryParse(item, out output))
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
                skinIniEditor.SkinIniChanged(skinName, "Colours", lastSelected.Name, lastSelected.Text, false);
                lastSelected.Background = System.Windows.Media.Brushes.Transparent;

                if (lastSelected.Text != $"{RGBPicker.R},{RGBPicker.G},{RGBPicker.B}")
                {
                    UpdateRGBPicker(lastSelected.Text);
                }
            }
            else
            {
                if (lastSelected != null)
                {
                    lastSelected.Background = System.Windows.Media.Brushes.Red;
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
            int visibleCombos = 5;

            foreach (var tb in FindVisualChildren<TextBox>(mw))
            {
                string value = skinIniEditor.GetIniData(skinName, "Colours", tb.Name, true);

                if (tb.Name.Contains("PART"))
                {
                    continue;
                }

                if (value == null && tb.Name != "Combo1" && tb.Name.Contains("Combo"))
                {
                    tb.Visibility = Visibility.Hidden;
                    HideTextBoxLabel(tb.Name);
                    visibleCombos--;
                    continue;
                }

                if (value != null)
                {
                    if (IsValidString(value.Split(',')))
                    {
                        tb.Text = value;
                        lastSelected = (TextBox)tb;
                        UpdateRGBPicker(value);
                        UpdateRectangle(lastSelected.Name);
                    }
                    else
                    {
                        value = skinIniEditor.GetIniData(skinName, "Colours", tb.Name, false);
                        tb.Text = value;
                        lastSelected = (TextBox)tb;
                        UpdateRGBPicker(value);
                        UpdateRectangle(lastSelected.Name);
                    }
                        
                }
                else
                {
                    value = skinIniEditor.GetIniData(skinName, "Colours", tb.Name, false);
                    tb.Text = value;
                    lastSelected = (TextBox)tb;
                    UpdateRGBPicker(value);
                    UpdateRectangle(lastSelected.Name);
                }
            }

            InitButtonMove(visibleCombos);
        }

        private void InitButtonMove(int i)
        {
            for (int j = i; j > 0; j--)
            {
                MoveButtons('+');
            }
        }

        private void MoveButtons(char directon)
        {
            int move = 90;

            if (directon == '-')
            {
                move = -90;
            }

            AddComboColourButton.Margin = new Thickness(AddComboColourButton.Margin.Left, AddComboColourButton.Margin.Top + move, 0, 0);
            RemoveComboColourButton.Margin = new Thickness(RemoveComboColourButton.Margin.Left, RemoveComboColourButton.Margin.Top + move, 0, 0);
        }

        private void AddComboColourButton_Click(object sender, RoutedEventArgs e)
        {
            AddComboColour();
            MoveButtons('+');
            ComboBoxVisibility($"Combo{GetLastComboColour()}", Visibility.Visible);

            lastSelected = (TextBox)FindName($"Combo{GetLastComboColour() - 1}");
            lastSelected.Text = skinIniEditor.GetIniData(skinName, "Colours", lastSelected.Name, false); 

            UpdateRGBPicker(lastSelected.Text);
            UpdateRectangle(lastSelected.Name);

            if (GetLastComboColour() > 5)
            {
                AddComboColourButton.Visibility = Visibility.Hidden;
            }

            if (GetLastComboColour() >= 3)
            {
                RemoveComboColourButton.Visibility = Visibility.Visible;
            }
        }

        private void ComboBoxVisibility(string name, Visibility visibility)
        {
            object obj = FindName($"{name}");

            if (obj is TextBox)
            {
                TextBox tb = obj as TextBox;
                tb.Visibility = visibility;
                ComboLabelVisibility(name, visibility);
            }
        }

        private void ComboLabelVisibility(string boxName, Visibility visibility)
        {
            object obj = FindName($"{boxName}Label");

            if (obj is Label)
            {
                Label tb = obj as Label;
                tb.Visibility = visibility;
                ComboFrameVisibility(boxName, visibility);
            }
        }

        private void ComboFrameVisibility(string boxName, Visibility visibility)
        {
            object obj = FindName($"{boxName}Frame");

            if (obj is Frame)
            {
                Frame tb = obj as Frame;
                tb.Visibility = visibility;
            }
        }

        private void RemoveComboColourButton_Click(object sender, RoutedEventArgs e)
        {
            MoveButtons('-');
            RemoveComboColour($"Combo{GetLastComboColour() - 1}");
            ComboBoxVisibility($"Combo{GetLastComboColour() - 1}", Visibility.Hidden);

            lastSelected = (TextBox)FindName($"Combo{GetLastComboColour() - 1}");
            UpdateRGBPicker(lastSelected.Text);
            UpdateRectangle(lastSelected.Name);

            if (GetLastComboColour() < 3)
            {
                RemoveComboColourButton.Visibility = Visibility.Hidden;
            }

            if (GetLastComboColour() <= 5)
            {
                AddComboColourButton.Visibility = Visibility.Visible;
            }
        }

        private void AddComboColour()
        {
            string comboName = $"Combo{GetLastComboColour()}";

            skinIniEditor.SkinIniChanged(skinName, "Colours", $"{comboName}", $"{skinIniEditor.GetIniData(skinName, "Colours", comboName, false)}", false);
        }

        private void RemoveComboColour(string comboName)
        {
            skinIniEditor.SkinIniChanged(skinName, "Colours", $"{comboName}", "", true);
        }

        private int GetLastComboColour()
        {
            int count = 1;
            foreach (var tb in FindVisualChildren<TextBox>(mw))
            {
                if (tb.Name.Contains("Combo") && tb.Visibility == Visibility.Visible)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
