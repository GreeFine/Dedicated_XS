﻿using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Fonction
    {
        
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
                case "/StartServer":
                    Console.Write("Ip : ");
                    string ip = Console.ReadLine();
                    Console.Write("Port : ");
                    int port = Convert.ToInt32(Console.ReadLine());
                    new Server(ip,port);
                    break;
                case "/StartServer Auto":
                    ip = "127.0.0.1";
                    port = 47950;
                    new Server(ip, port);
                    break;
                case "/SendCommand":
                    Server.Commhandle();
                    break;
                case "/FileSend" :
                    string path = Console.ReadLine();
                    Server.FileSending(path);
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
        //static void ConnectConfig()
        //{
        //    Console.WriteLine("Config of Connection ....");
        //    Console.WriteLine("Enter ip :");
        //    string ip = Console.ReadLine();
        //    Console.WriteLine("Enter port :");
        //    string port = Console.ReadLine();
        //    Console.WriteLine("Connecting " + ip + ":" + port);

        //    //Start Connection
        //    TcpClient client = new TcpClient();

        //    // Connect using a timeout (5 seconds)
        //    IAsyncResult result = client.BeginConnect(IPAddress.Parse(ip), int.Parse(port), null, null);
        //    bool success = result.AsyncWaitHandle.WaitOne(5000, true);
       
        //    if (!success)
        //    {
        //        client.Close();
        //        Console.WriteLine("Connection time out");
        //    } else 
        //    {
        //        Console.WriteLine("Connected");
        //        ServerCommand( client);
        //    }
        //}

        //static void ConnectAuto()
        //{
        //    Console.WriteLine("Auto Connection ....");
        //    //Start Connection
        //    TcpClient client = new TcpClient();
        //    // Connect using a timeout (5 seconds)
        //    IAsyncResult result = client.BeginConnect(IPAddress.Parse("192.168.0.10"), int.Parse("47950"), null, null);
        //    bool success = result.AsyncWaitHandle.WaitOne(5000, true);
        //    if (!success)
        //    {
        //        client.Close();
        //        Console.WriteLine("Connection time out");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Connected");
        //        ServerCommand(client);
        //    }
        //}
        
