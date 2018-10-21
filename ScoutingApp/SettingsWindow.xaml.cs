using System.Windows;
using ScoutingApp;

namespace ScoutingApp
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            IpBox.Text = Settings.Ip;
            PortBox.Text = Settings.Port.ToString();
            StartUpLoad.IsChecked = Settings.LoadAtStartUp;
            LogIn.Visibility = Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
            LogOut.Visibility = !Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LogIn_OnClick(object sender, RoutedEventArgs e)
        {
            new LogIn(false).ShowDialog();
            LogIn.Visibility = Visibility.Collapsed;
            LogOut.Visibility = Visibility.Visible;
        }

        private void LogOut_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Password = "";
            LogIn.Visibility = Visibility.Visible;
            LogOut.Visibility = Visibility.Collapsed;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e) => Close();


        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Ip = IpBox.Text;
            Settings.Port = int.Parse(PortBox.Text);
            if (StartUpLoad.IsChecked != null) Settings.LoadAtStartUp = (bool) StartUpLoad.IsChecked;
            Close();
        }

        private void RestoreSettings_OnClick(object sender, RoutedEventArgs e)
        {
            Settings.Ip = "127.0.0.1";
            Settings.LoadAtStartUp = true;
            Settings.Port = 3388;
            IpBox.Text = Settings.Ip;
            PortBox.Text = Settings.Port.ToString();
            StartUpLoad.IsChecked = Settings.LoadAtStartUp;
        }
    }
}