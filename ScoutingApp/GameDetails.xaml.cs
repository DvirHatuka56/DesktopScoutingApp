using System.Windows.Controls;
using ScoutingClassesLib.Classes;

namespace ScoutingApp
{
    public partial class GameDetails : Page
    {
        public GameDetails()
        {
            InitializeComponent();
        }

        private void ResetFields()
        {
            TeamNumber.Content = "Team Number: ";
            GameNumber.Content = "Game Number: ";
            AutoDiscription.Content = "Autonomous Description: ";
            AutoSuccses.Content = "Autonomous Success: ";
            Stability.Content = "Stability: ";
            Missions.Content = "Missions: ";
            MissionsSuccses.Content = "Missions Success: ";
            ClimbAble.Content = "";
            MiddleAuto.Content = "";
        }

        public void ShowDetails(Game game)
        {
            ResetFields();
            TeamNumber.Content += game.TeamNumber.ToString();
            GameNumber.Content += game.GameNumber.ToString();
            AutoDiscription.Content += game.AutonomousDescription;
            AutoSuccses.Content += game.AutonomousSuccess.ToString();
            Stability.Content += game.Stability.ToString();
            Missions.Content += game.Missions;
            MissionsSuccses.Content += game.MissionSuccess.ToString();
            ClimbAble.Content += game.Climb ? "The robot climbed" : "The robot did not climb";
            MiddleAuto.Content += game.MiddleAutonomous ? "Did middle autonomous" : "Did side autonomous";
        }
    }
}
