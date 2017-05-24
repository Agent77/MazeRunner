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
        
        private StreamWriter writer;
        private StreamReader reader;
        public ClientCommunicator()
        {
        }

        public int Connect(string ip, string port)
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse("5555"));
            tcpMessenger = new TcpClient();
            tcpMessenger.Connect(ep);
            if(!tcpMessenger.Connected)
            {
                return -1;
            }
            NetworkStream ns = tcpMessenger.GetStream();
            writer = new StreamWriter(ns);
            reader = new StreamReader(ns);
            writer.AutoFlush = true;
            return 1;
        }

        public void Write(string command)
        {
            writer.WriteLine(command);

        }
        public string read()
        {
            string result = null;
            string test = reader.ReadLine();
            if (!test.Contains("#"))
                result += test;
            while (reader.Peek() > 0)
            {
                test = reader.ReadLine();
                if (!test.Contains("#"))
                    result += test;
               
            }
            return result;
        } // blocking call

        public void disconnect() {
            tcpMessenger.Close();
        }
    }

}
