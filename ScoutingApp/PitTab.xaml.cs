using ScoutingClassesLib.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ScoutingApp
{
	/// <summary>
	/// Interaction logic for PitTab.xaml
	/// </summary>
	public partial class PitTab : Page
	{
		public PitTab()
		{
			InitializeComponent();
			CustomListView.Header1.Content = "Team Number";
			CustomListView.Header2.Content = "Autonomous Options";
			DataLists.PitListChanged += DataListsOnPitListChanged;
		}

		private void DataListsOnPitListChanged(object sender, EventArgs e)
		{
			UpdateList();
		}

		private void UpdateList()
		{
			MsgLabel.Visibility = Visibility.Visible;
			(string msg, bool err) = DataLists.UpdatePitList();
			MsgLabel.ShowMessage(msg, err);
			CustomListView.ItemList.Children.Clear();
			foreach (var pit in DataLists.GetPits())
			{
				CustomListViewColumn column = new CustomListViewColumn
				{
					Data1 = { Content = pit.TeamNumber },
					Data2 = { Content = pit.AutonomousOptions }
				};
				column.MouseDoubleClick += DetailsButtonOnClick;
				CustomListView.ItemList.Children.Add(column);
			}
		}

		private void DetailsButtonOnClick(object sender, RoutedEventArgs e)
		{
			MsgLabel.Visibility = Visibility.Collapsed;
			CustomListViewColumn column = (CustomListViewColumn)sender;
			int idx = CustomListView.ItemList.Children.IndexOf(column);
			Pit pit = DataLists.GetPits()[idx];

			PitDetailsExpander.IsExpanded = true;
			PitDetails.ShowDetails(pit);
		}
	}
}
