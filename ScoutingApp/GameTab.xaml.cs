using System;
using System.Windows;
using System.Windows.Controls;
using ScoutingClassesLib.Classes;

namespace ScoutingApp
{
    public partial class GameTab : Page
    {
        public GameTab()
        {
            InitializeComponent();
            CustomListView.Header1.Content = "Team Number";
            CustomListView.Header2.Content = "Game Number";
            DataLists.GameListChanged += DataListsOnGameListChanged;
        }

        private void DataListsOnGameListChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            MsgLabel.Visibility = Visibility.Visible;
            (string msg, bool err) = DataLists.UpdateGameList();
            MsgLabel.ShowMessage(msg, err);
            CustomListView.ItemList.Children.Clear();
            foreach (var game in  DataLists.GetGames())
            {
                CustomListViewColumn column = new CustomListViewColumn
                {
                    Data1 = {Content = game.TeamNumber},
                    Data2 = {Content = game.GameNumber}
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
            Game game = DataLists.GetGames()[idx];

            GameDetailsExpender.IsExpanded = true;
            GameDetails.ShowDetails(game);
        }
    }
}