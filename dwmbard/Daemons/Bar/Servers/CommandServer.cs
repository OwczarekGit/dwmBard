using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace dwmBard.Servers
{
    public class CommandServer
    {
        private static int PORT = 2137;
        private IPHostEntry host;
        private IPAddress ipAddress;
        private IPEndPoint ipEndPoint;
        private Thread worker;
        
        public CommandServer()
        {
            host = Dns.GetHostEntry("localhost");
            ipAddress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAddress, PORT);
            worker = new Thread(listen);
        }

        public void start()
        {
            worker.Start();
        }

        private void listen()
        {
            // Reference for tomorrow:
            // https://docs.microsoft.com/pl-pl/dotnet/framework/network-programming/asynchronous-server-socket-example
            try
            {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(ipEndPoint);
                listener.Listen(10);
                
                Console.WriteLine("Waiting for commands...");
                Socket handler = listener.Accept();

                /*string data = null;  
                byte[] bytes = null;  
  
                while (true)  
                {  
                    bytes = new byte[1024];  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);  
                    if (data.IndexOf("<EOF>") > -1)  
                    {  
                        break;  
                    }  
                }  
  
                Console.WriteLine("Text received : {0}", data);*/
                
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Listener down.");
        }
    }
}