using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ScoutingClassesLib.Classes;
using Client;

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
                var resp = new ResponseMessage();
                while (!resp.Message.Equals("End"))
                {
                    resp = ResponseMessage.ToResponse(client.ReceiveMessage());
                    client.SendMessage("...");
                    if (resp.Message.Equals("End")) break;
                    update.Add(Game.Deserialize(resp.Content));
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
                var resp = new ResponseMessage();
                while (!resp.Message.Equals("End"))
                {
                    resp = ResponseMessage.ToResponse(client.ReceiveMessage());
                    client.SendMessage("...");
                    if (resp.Message.Equals("End")) break;
                    update.Add(Pit.Deserialize(resp.Content));
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