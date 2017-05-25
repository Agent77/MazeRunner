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
    /// <summary>
    /// Class to connect and communicate with server
    /// </summary>
    public class ClientCommunicator
    {

        /// <summary>
        /// Tcp connection member
        /// </summary>
        private TcpClient tcpMessenger;
        
        /// <summary>
        /// Writer
        /// </summary>
        private StreamWriter writer;
        /// <summary>
        /// reader
        /// </summary>
        private StreamReader reader;

        /// <summary>
        /// Constructor for client communicator
        /// </summary>
        public ClientCommunicator()
        {
        }

        /// <summary>
        /// Connect method
        /// </summary>
        /// <param name="ip"> ip address from App. config</param>
        /// <param name="port"> port from app config</param>
        /// <returns>Success or failur</returns>
        public int Connect(string ip, string port)
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                Int32.Parse("5555"));
            tcpMessenger = new TcpClient();
            try
            {
                tcpMessenger.Connect(ep);

            } catch(Exception e)
            {
                return -1;
            }
          
            NetworkStream ns = tcpMessenger.GetStream();
            writer = new StreamWriter(ns);
            reader = new StreamReader(ns);
            writer.AutoFlush = true;
            return 1;
        }

        /// <summary>
        /// Writes to server
        /// </summary>
        /// <param name="command"> from to send to server</param>
        public void Write(string command)
        {
            writer.WriteLine(command);

        }

        /// <summary>
        /// Reads from server
        /// </summary>
        /// <returns> string read</returns>
        public string Read()
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
        } 


        /// <summary>
        /// Disconnects from server
        /// </summary>
        public void Disconnect() {
            tcpMessenger.Close();
        }
    }

}
