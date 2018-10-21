using System.Windows;
using ScoutingApp;

namespace ScoutingApp
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Settings.LoadSetting();
            
            if (Connections.ValidPassword())
            {
                new MainWindow().Show();
            } // if the password is fine, the main window will appear
            else
            {
                new LogIn().Show();
            }// else, a log in page will appear
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            Settings.SaveSettings(); // saves the password at the exit
        }
    }
}