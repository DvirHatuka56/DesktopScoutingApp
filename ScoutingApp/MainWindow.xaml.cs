using System.Windows;

namespace ScoutingApp
{
	/// <inheritdoc >
	///     <cref></cref>
	/// </inheritdoc>
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	// ReSharper disable once RedundantExtendsListEntry
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Username.Content = Settings.Username;
			LogIn.Visibility = Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
			LogOut.Visibility = !Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
		}

		private void RefreshLists_OnClick(object sender, RoutedEventArgs e)
		{
			DataLists.RefreshLists();
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

		private void Settings_OnClick(object sender, RoutedEventArgs e)
		{
			new SettingsWindow().ShowDialog();
		}

		private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			LogIn.Visibility = Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
			LogOut.Visibility = !Settings.Password.Equals("") ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}