using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Fonction
    {
        public static bool ServerState;

        static void Helpmsg()
        {
            Console.WriteLine("Help message list of availiable command:");
            Console.WriteLine("1: /quit");
            Console.WriteLine("2: /StartServer");
            Console.WriteLine("4: /SendCommand");
            Console.WriteLine("3: /clear");
            Console.WriteLine("4: /? or /help");
        }

        public static void CommandExe(string Command)
        {
            string _Command = Command;
            switch (_Command)
            {
                case "/quit":
                    Environment.Exit(404);
                    break;
                case "/ServerStart":
                    Console.Write("Ip : ");
                    string ip = Console.ReadLine();
                    Console.Write("Port : ");
                    int port = Convert.ToInt32(Console.ReadLine());
                    new Server(ip,port);
                    ServerState = true;
                    break;
                case "/ServerStart Auto":
                    ip = "127.0.0.1";
                    port = 47950;
                    new Server(ip, port);
                    ServerState = true;
                    break;
                case "Auto":
                    ip = "127.0.0.1";
                    port = 47950;
                    new Server(ip, port);
                    ServerState = true;
                    Console.Write("Path : ");
                    string path = Console.ReadLine();
                    Server.FileSending(path);
                    break;
                case "/MsgStart":
                    if (ServerState)
                    {
                        Server.MsgStart();
                    } else {
                        Console.WriteLine("Error : Serveur not Started");
                    }
                    break;
                case "/FileSend" :
                    if (ServerState)
                    {
                        Console.Write("Path : ");
                        path = Console.ReadLine();
                        Server.FileSending(path);
                    } else {
                        Console.WriteLine("Error : Serveur not Started");
                    }
                    break;
                case "/clear":
                    Console.Clear();
                    break;
                case "/help":
                    Helpmsg();
                    break;
                case "/?":
                    Helpmsg();
                    break;
                default:
                    Console.WriteLine("Error bad syntax try /help or /?");
                    break;
            }
        }




    }
}

