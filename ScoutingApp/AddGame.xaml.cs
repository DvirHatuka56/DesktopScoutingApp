using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ScoutingClassesLib.Classes;

namespace ScoutingApp
{
    /// <inheritdoc>
    ///     <cref>hello</cref>
    /// </inheritdoc>
    /// <summary>
    /// Interaction logic for AddGame.xaml
    /// </summary>
    public partial class AddGame : Page
    {
        public AddGame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidInput())
            {
                return;
            }

            var game = new Game(int.Parse(TeamNumber.Text))
            {
                GameNumber = int.Parse(GameNumber.Text),
                AutonomousDescription = GetContentOfAutoDescription(),
                AutonomousSuccess = AutoSucsses.GetIntValue(),
                Climb = Climb.IsChecked != null && (bool) Climb.IsChecked,
                MiddleAutonomous = MiddleAuto.IsChecked != null && (bool) MiddleAuto.IsChecked,
                MissionSuccess = MissionSucssesSlider.GetIntValue(),
                Missions = GetSelectedMission(),
                Stability = StabilitySlider.GetIntValue()
            };

            if (!DataLists.AddGame(game))
            {
                MsgLabel.ShowMessage("Can't send game to server.\nPlease check your internet connection.", true);
            }
            else
            {
                MsgLabel.ShowMessage("Game has successfully saved!", false);
            }
        }

        private bool ValidInput()
        {
            bool valid = true;
            if (IsRichTextBoxEmpty(AutoDiscription))
            {
                MsgLabel.ShowMessage("Autonomous Description can't be empty", true);
                SubmitButton.IsEnabled = AllowSubmit();
                valid = false;
            }

            if (!GetSelectedMission().Equals("nothing")) return valid;
            MsgLabel.ShowMessage("One of the mission must be selected", true);
            SubmitButton.IsEnabled = AllowSubmit();
            return false;
        }

        private bool IsRichTextBoxEmpty(RichTextBox rtb)
        {
            if (rtb.Document.Blocks.Count == 0) return true;
            var startPointer = rtb.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            var endPointer = rtb.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer != null && endPointer != null && startPointer.CompareTo(endPointer) == 0;
        }


        private string GetSelectedMission()
        {
            if (M1.IsChecked != null && (bool) M1.IsChecked)
            {
                return M1.Content.ToString();
            }

            if (M2.IsChecked != null && (bool) M2.IsChecked)
            {
                return M2.Content.ToString();
            }

            if (M3.IsChecked != null && (bool) M3.IsChecked)
            {
                return M3.Content.ToString();
            }

            return "nothing";
        }

        private string GetContentOfAutoDescription()
        {
            var textRange = new TextRange(AutoDiscription.Document.ContentStart, AutoDiscription.Document.ContentEnd);
            return textRange.Text.Replace("\r\n", "");
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var box = (TextBox) sender;
            if (SubmitButton == null) return;
            SubmitButton.IsEnabled = AllowSubmit();
            box.Background = int.TryParse(box.Text, out _)
                ? new SolidColorBrush(Colors.LightGreen)
                : new SolidColorBrush(Colors.OrangeRed);
        }

        private bool AllowSubmit()
        {
            return int.TryParse(TeamNumber.Text, out _) && 
                   int.TryParse(GameNumber.Text, out _) &&
                   !IsRichTextBoxEmpty(AutoDiscription) &&
                   !GetSelectedMission().Equals("nothing");
        }
        
        private void Missions_OnGotFocus(object sender, RoutedEventArgs e)
        {
            SubmitButton.IsEnabled = AllowSubmit();
        }

        private void AutoDescription_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SubmitButton.IsEnabled = AllowSubmit();
        }
    }
}