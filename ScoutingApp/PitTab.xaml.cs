using ScoutingClassesLib.Classes;
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
			CustomListViewColumn column = (CustomListViewColumn)sender;
			int idx = CustomListView.ItemList.Children.IndexOf(column);
			Pit pit = DataLists.GetPits()[idx];

			PitDetailsExpander.IsExpanded = true;
			PitDetails.ShowDetails(pit);
		}
	}
}
