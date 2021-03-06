﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ScoutingClassesLib.Classes;

namespace ScoutingApp
{
    public partial class AddPit : Page
    {
        public AddPit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pit = new Pit(int.Parse(TeamNumber.Text))
            {
                AutonomousOptions = int.Parse(AutoOptions.Text),
                Climb = ClimbAble.IsChecked != null && (bool) ClimbAble.IsChecked,
                MiddleAutonomous = MiddleAuto.IsChecked != null && (bool) MiddleAuto.IsChecked,
                Chassis = SelectedChassis(),
                AutonomousDescription = AutoDescriptionContent()
            };
            MsgLabel.Visibility = Visibility.Visible;
            if (!DataLists.AddPit(pit))
            {
                MsgLabel.ShowMessage("Can't send pit to server.\nPlease check your internet connection.", true);
            }
            else
            {
                MsgLabel.ShowMessage("Pit has successfully saved!", false);
            }
        }

        private bool IsAutoDescriptionEmpty()
        {
            if (AutoDescription == null) return true;
            if (AutoDescription != null && AutoDescription.Document.Blocks.Count == 0)
                return true;
            var startPointer =
                AutoDescription.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            var endPointer =
                AutoDescription.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer != null && endPointer != null && startPointer.CompareTo(endPointer) == 0;
        }

        private string AutoDescriptionContent()
        {
            return new TextRange(AutoDescription.Document.ContentStart, AutoDescription.Document.ContentEnd).Text;
        }

        private string SelectedChassis()
        {
            if (Tank.IsChecked != null && (bool) Tank.IsChecked)
            {
                return Tank.Content.ToString();
            }

            if (Mechanom.IsChecked != null && (bool) Mechanom.IsChecked)
            {
                return Mechanom.Content.ToString();
            }

            if (Omni.IsChecked != null && (bool) Omni.IsChecked)
            {
                return Omni.Content.ToString();
            }

            return "";
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SubmitButton == null) return;
            var textBox = (TextBox) sender;
            textBox.Background = int.TryParse(textBox.Text, out _) ? Brushes.LightGreen : Brushes.OrangeRed;
            SubmitButton.IsEnabled = ValidInput();
        }

        private bool ValidInput()
        {
            MsgLabel.Visibility = Visibility.Collapsed;
            return !IsAutoDescriptionEmpty() &&
                   !string.IsNullOrEmpty(SelectedChassis()) &&
                   int.TryParse(TeamNumber.Text, out _) &&
                   int.TryParse(AutoOptions.Text, out _);
        }

        private void Chassis_OnChecked(object sender, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = ValidInput();
        }

        private void AutoDescription_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            AutoDescription.Background = IsAutoDescriptionEmpty() ? Brushes.OrangeRed : Brushes.White;
            SubmitButton.IsEnabled = ValidInput();
        }
    }
}