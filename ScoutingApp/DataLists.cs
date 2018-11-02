using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using ScoutingClassesLib.Classes;
using Client;
using ScoutingApp;

namespace ScoutingApp
{
    public static class DataLists
    {
        private static string _lastUpdateGame = "";
        private static string _lastUpdatePit = "";
        private static readonly List<Game> Games = new List<Game>();
        private static readonly List<Pit> Pits = new List<Pit>();

        public static event EventHandler GameListChanged;
        public static event EventHandler PitListChanged;

        public static bool AddGame(Game game)
        {
            return Connections.Upload(game, "Game");
        }

        public static bool AddPit(Pit pit)
        {
            return Connections.Upload(pit, "Pit");
        }

        public static List<Game> GetGames()
        {
            return Games;
        }

        public static List<Pit> GetPits()
        {
            return Pits;
        }
       
        public static (string msg, bool err) UpdatePitList()
        {
            (var update, string time) = Connections.LoadPits(_lastUpdatePit);
            _lastUpdatePit = time;
            if (update == null) return ("Error", true);
            if (update.Count == 0) return ("Nothing to update...", false);
            foreach (var pit in update)
            {
                Pits.Add(pit);
            }
            return ("Update made successfully", false);
        }
        
        public static (string msg, bool err) UpdateGameList()
        {
            (var update, string time) = Connections.LoadGames(_lastUpdateGame);
            _lastUpdateGame = time;
            if (update == null) return ("Error", true);
            if (update.Count == 0) return ("Nothing to update...", false);
            foreach (var game in update)
            {
                Games.Add(game);
            }
            return ("Update made successfully", false);
        }

        public static void RefreshLists()
        {
            GameListChanged?.Invoke(GetGames(), EventArgs.Empty);
            PitListChanged?.Invoke(GetPits(), EventArgs.Empty);
        }
    }
}