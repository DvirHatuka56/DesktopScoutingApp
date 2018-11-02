using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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

        public static (List<Game>, string) LoadGames(string lastUpdate)
        {
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
                var resps = client.ReceiveMessage().Split('|');
                new DebugWindow($"{lastUpdate}{Environment.NewLine}" +
                                $"{resps.Length}{Environment.NewLine}" + 
                                $"{Indent(resps[0])}{Environment.NewLine}" + 
                                $"{Indent(resps[1])}").ShowDialog();
                foreach (var resp in resps)
                {
                    if (string.IsNullOrEmpty(resp)) continue;
                    var response = ResponseMessage.ToResponse(resp);
                    if (response.Message != "")
                    {
                        update.Add(Game.Deserialize(response.Content));
                    }
                }

                return (update, DateTime.Now.ToString("HH:mm:ss tt"));
            }
            catch (SocketException e)
            {
                new DebugWindow("Socket Exception: " + e).ShowDialog();
                return (null, lastUpdate);
            }
            catch (Exception e)
            {
                new DebugWindow("Exception: " + e).ShowDialog();
                return (null, lastUpdate);
            }
        }

        private static string Indent(string str)
        {
            return str.Substring(0, str.Length / 2) + Environment.NewLine + str.Substring(str.Length / 2);
        }
        
        public static (List<Pit>, string) LoadPits(string lastUpdate)
        {
            try
            {
                RequestMessage uploadRequestMessage = new RequestMessage
                {
                    Username = Settings.Username,
                    Password = Settings.Password,
                    Request = "Load",
                    Type = "Pit",
                    Content = lastUpdate,
                    Time = DateTime.Now.ToString("HH:mm:ss tt")
                };
                ClientSocket client = new ClientSocket(Settings.Ip, Settings.Port);
                client.SendMessage(uploadRequestMessage.ToString());
                
                var update = new List<Pit>();
                var response = new ResponseMessage();
                while (response.Message != "End")
                {
                    response = ResponseMessage.ToResponse(client.ReceiveMessage());
                    update.Add(Pit.Deserialize(response.Content));
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