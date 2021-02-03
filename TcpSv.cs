using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class TcpSv
    {
        static void Main(string[] args)
        {
            string recieveddata;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string endmarker = "<##!##>";
            ArrayList msg = new ArrayList();


            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");
            Socket client = newsock.Accept();
            IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}",
                                 newclient.Address, newclient.Port);
            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            string welcome = "Welcome to ege's server";
            sw.WriteLine(welcome);
            sw.Flush();
            while (true)
            {
                recieveddata = sr.ReadLine();
                string[] msgdata = recieveddata.Split(delimiterChars);

                foreach (string ege in msgdata)
                {

                    if (string.Equals(ege, endmarker))
                    {
                        Console.WriteLine("message end ");

                    }
                    else
                    {
                        msg.Add(ege);
                    }

                }
                string realmsg = string.Join(" ", msg.ToArray());
                Console.WriteLine(realmsg);
                sw.WriteLine(realmsg);
                sw.Flush();
                msg.Clear();
            }           
            Console.WriteLine("Disconnected from {0}", newclient.Address);
            sw.Close();
            sr.Close();
            ns.Close();
        }
     }

  }



