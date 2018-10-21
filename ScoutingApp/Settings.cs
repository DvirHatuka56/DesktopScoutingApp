using System;
using System.Configuration;
using System.Net.Sockets;
using Client;

namespace ScoutingApp
{
    public static class Settings
    {        
        /// <summary>
        /// Password to access the server
        /// </summary>
        public static string Password { get; set; }
        /// <summary>
        /// Username to access the server
        /// </summary>
        public static string Username { get; set; }
        /// <summary>
        /// Ip of the server
        /// </summary>
        public static string Ip { get; set; }
        /// <summary>
        /// Port on the server
        /// </summary>
        public static int Port { get; set; }
        /// <summary>
        /// Default IP of the server
        /// </summary>
        public static string DefaultIp { get; private set; }
        /// <summary>
        /// Default port on the server
        /// </summary>
        public static int DefaultPort { get; private set; }
        /// <summary>
        /// Loading the Games and Pits from the server at start up
        /// </summary>
        public static bool LoadAtStartUp { get; set; }
        
        /// <summary>
        /// Loads the settings of the application
        /// </summary>
        public static void LoadSetting()
        {
            Ip = ConfigurationManager.AppSettings["ip"];
            DefaultIp = Ip;
            Port = int.Parse(ConfigurationManager.AppSettings["port"]);
            DefaultPort = Port;
            Username = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
            LoadAtStartUp = Convert.ToBoolean(ConfigurationManager.AppSettings["loadContent"]);
        }
        
        /// <summary>
        /// Save the last password the user entered
        /// </summary>
        public static void SaveSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;
            settings["ip"].Value = Ip;
            settings["port"].Value = Port.ToString();
            settings["username"].Value = Username;
            settings["password"].Value = Password;
            settings["loadContent"].Value = LoadAtStartUp.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
    }
}