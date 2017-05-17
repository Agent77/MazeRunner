using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner.Models
{

    public class ClientCommunicator
    {
        private TcpClient tcpMessenger;
        private int x;
        private StreamWriter writer;
        private StreamReader reader;
        public ClientCommunicator()
        {
        }

        public void Connect(string ip, string port)
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse("5555"));
            tcpMessenger = new TcpClient();
            tcpMessenger.Connect(ep);
            NetworkStream ns = tcpMessenger.GetStream();
            writer = new StreamWriter(ns);
            reader = new StreamReader(ns);
            writer.AutoFlush = true;
        }

        public void Write(string command)
        {
            writer.WriteLine(command);

        }
        public string read()
        {
            return reader.ReadLine();
        } // blocking call

        void disconnect() { }
    }

}
