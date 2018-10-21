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
        private static readonly List<Game> Games = new List<Game>();
        private static readonly List<Pit> Pits = new List<Pit>();

        public static event EventHandler GameListChanged;
        public static event EventHandler PitListChanged;

        public static bool AddGame(Game game)
        {
            if (!Connections.Upload(game, "Game")) return false;
            Games.Add(game);
            return true;
        }

        public static bool AddPit(Pit pit)
        {
            if (!Connections.Upload(pit, "Pit")) return false;
            Pits.Add(pit);
            return true;
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
            var update = Connections.LoadGames();
            return update.Equals(null) ? ("Update made successfully", true) : ("Error", false);
        }
        
        public static (string msg, bool err) UpdateGameList()
        {
            var update = Connections.LoadGames();
            if (update != null)
            {
                Games.Clear();
                foreach (var game in update)
                {
                    Games.Add(game);
                }
            }
            return update != null ? ("Update made successfully", false) : ("Error", true);
        }

        public static void RefreshLists()
        {
            GameListChanged?.Invoke(GetGames(), EventArgs.Empty);
            PitListChanged?.Invoke(GetPits(), EventArgs.Empty);
        }
    }
}