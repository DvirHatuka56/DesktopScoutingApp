using System.Windows;
using System.Windows.Controls;

namespace ScoutingApp
{
    public partial class LogIn : Window
    {
        private readonly bool _switchWindow;
        
        public LogIn()
        {
            InitializeComponent();
            _switchWindow = true;
        }

        public LogIn(bool switchWindow)
        {
            InitializeComponent();
            _switchWindow = switchWindow;
        }
        
        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Password = PasswordBox.Password;
            if (!Connections.ValidPassword())
            {
                MsgLabel.Visibility = Visibility.Visible;
                MsgLabel.ShowMessage("Incorrect Password", true);
                return;
            }
            Settings.SaveSettings();
            if (_switchWindow)
            {
                SwitchToMain();
            }
            if (Settings.LoadAtStartUp) DataLists.RefreshLists();
            Close();
        }

        private void SwitchToMain()
        {
            new MainWindow().Show();
            Close();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            MsgLabel.Visibility = Visibility.Collapsed;
            Settings.Password = PasswordBox.Password;
        }

        private void Username_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Username = Username.Text;
        }
    }
}
