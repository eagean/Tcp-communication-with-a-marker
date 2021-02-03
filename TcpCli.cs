using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClient
{
    class TcpCli
    {
        static void Main(string[] args)
        {
            string recievedtosv;
            string input;
            string markedinput;
            string endmarker = "<##!##>";
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }
            NetworkStream stream = new NetworkStream(server);
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            recievedtosv = reader.ReadLine();
            Console.WriteLine(recievedtosv);
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                    break;
                markedinput = input +" "+ endmarker;

                writer.WriteLine(markedinput);
                writer.Flush();
               
                recievedtosv = reader.ReadLine();
                Console.WriteLine(recievedtosv);
            }
            Console.WriteLine("Disconnecting from server...");
            reader.Close();
            writer.Close();
            stream.Close();
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

    }
}
