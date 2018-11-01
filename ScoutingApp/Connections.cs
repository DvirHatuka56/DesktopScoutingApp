using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ScoutingClassesLib.Classes;
using Client;
using Newtonsoft.Json;

namespace ScoutingApp
{
    public static class Connections
    {
        public static bool ValidPassword()
        {
            try
            {
                RequestMessage invalidRequestMessage = new RequestMessage
                {
                    Username = Settings.Username,
                    Password = Settings.Password,
                    Request = "Hello",
                    Type = "Game",
                    Time = DateTime.Now.ToString("HH:mm:ss tt")
                };
                ClientSocket client = new ClientSocket(Settings.Ip, Settings.Port);
                client.SendMessage(invalidRequestMessage.ToString());
                return ResponseMessage.ToResponse(client.ReceiveMessage()).Message.Equals("Invalid Request!");
            }
            catch (SocketException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Upload(object data, string type)
        {
            try
            {
                RequestMessage uploadRequestMessage = new RequestMessage
                {
                    Username = Settings.Username,
                    Password = Settings.Password,
                    Request = "Upload",
                    Type = type,
                    Content = type.Equals("Game") ? ((Game) data).Serialize() : ((Pit) data).Serialize(),
                    Time = DateTime.Now.ToString("HH:mm:ss tt")
                };
                ClientSocket client = new ClientSocket(Settings.Ip, Settings.Port);
                client.SendMessage(uploadRequestMessage.ToString());
                return ResponseMessage.ToResponse(client.ReceiveMessage()).Message.Equals("Fine");
            }
            catch (SocketException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<object> Load(string type)
        {
            try
            {
                RequestMessage uploadRequestMessage = new RequestMessage
                {
                    Username = Settings.Username,
                    Password = Settings.Password,
                    Request = "Load",
                    Type = type,
                    Content = "",
                    Time = DateTime.Now.ToString("HH:mm:ss tt")
                };
                ClientSocket client = new ClientSocket(Settings.Ip, Settings.Port);
                client.SendMessage(uploadRequestMessage.ToString());
                var response = ResponseMessage.ToResponse(client.ReceiveMessage());
                if (response.Message.Equals("fine"))
                {
                    return type.Equals("Game") 
                        ? Game.DeserializeList(response.Content).Cast<object>().ToList() 
                        : Pit.DeserializeList(response.Content).Cast<object>().ToList();
                }

                return null;
            }
            catch (SocketException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static (List<Game>, string) LoadGames(string lastUpdate)
        {
//            var json = "[{\"Stability\":4,\"MissionSuccess\":4,\"Missions\":\"Mission three\",\"AutonomousSuccess\":3,\"GameNumber\":52,\"TeamNumber\":3388,\"Climb\":true,\"MiddleAutonomous\":true,\"AutonomousDescription\":\"hello\"}, {\"Stability\":4,\"MissionSuccess\":4,\"Missions\":\"Mission three\",\"AutonomousSuccess\":3,\"GameNumber\":52,\"TeamNumber\":3388,\"Climb\":true,\"MiddleAutonomous\":true,\"AutonomousDescription\":\"hello\"}]";
//            return JsonConvert.DeserializeObject<List<Game>>(json);
            try
            {
                RequestMessage uploadRequestMessage = new RequestMessage
                {
                    Username = Settings.Username,
                    Password = Settings.Password,
                    Request = "Load",
                    Type = "Game",
                    Content = lastUpdate,
                    Time = DateTime.Now.ToString("HH:mm:ss tt")
                };
                ClientSocket client = new ClientSocket(Settings.Ip, Settings.Port);
                client.SendMessage(uploadRequestMessage.ToString());
                
                var update = new List<Game>();
                var response = new ResponseMessage();
                while (response.Message != "End")
                {
                    response = ResponseMessage.ToResponse(client.ReceiveMessage());
                    update.Add(Game.Deserialize(response.Content));
                }
                return (update, DateTime.Now.ToString("HH:mm:ss tt"));
            }
            catch (SocketException)
            {
                return (null, lastUpdate);
            }
            catch (Exception)
            {
                return (null, lastUpdate);
            }
        }
    }
}