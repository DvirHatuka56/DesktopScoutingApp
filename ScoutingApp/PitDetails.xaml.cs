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
	/// Interaction logic for PitDetails.xaml
	/// </summary>
	public partial class PitDetails : Page
	{
		public PitDetails()
		{
			InitializeComponent();
		}
		private void ResetFields()
		{
			TeamNumber.Content = "Team Number: ";
			Chassis.Content = "Chassis: ";
			AutoDiscription.Content = "Autonomous Description: ";
			AutonomousOptions.Content = "Autonomous Options: ";
			ClimbAble.Content = "";
			MiddleAuto.Content = "";
		}

		public void ShowDetails(Pit pit)
		{
			ResetFields();
			TeamNumber.Content += pit.TeamNumber.ToString();
			Chassis.Content += pit.Chassis;
			AutoDiscription.Content += pit.AutonomousDescription;
			AutonomousOptions.Content += pit.AutonomousOptions.ToString();
			ClimbAble.Content += pit.Climb ? "The robot can climb" : "The robot can not climb";
			MiddleAuto.Content += pit.MiddleAutonomous ? "Has middle autonomous" : "Doesn't has middle autonomous";
		}
	}
}
