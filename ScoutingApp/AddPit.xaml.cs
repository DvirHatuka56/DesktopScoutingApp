using System.Windows;
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

            if (!DataLists.AddPit(pit))
            {
                MsgLabel.ShowMessage("Can't send game to server.\nPlease check your internet connection.", true);
            }
            else
            {
                MsgLabel.ShowMessage("Game has successfully saved!", false);
            }
        }

        private bool IsAutoDescriptionEmpty()
        {
            if (AutoDiscription == null) return true;
            if (AutoDiscription != null && AutoDiscription.Document.Blocks.Count == 0)
                return true;
            var startPointer =
                AutoDiscription.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            var endPointer =
                AutoDiscription.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer != null && endPointer != null && startPointer.CompareTo(endPointer) == 0;
        }

        private string AutoDescriptionContent()
        {
            return new TextRange(AutoDiscription.Document.ContentStart, AutoDiscription.Document.ContentEnd).Text;
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
            AutoDiscription.Background = IsAutoDescriptionEmpty() ? Brushes.OrangeRed : Brushes.White;
            SubmitButton.IsEnabled = ValidInput();
        }
    }
}