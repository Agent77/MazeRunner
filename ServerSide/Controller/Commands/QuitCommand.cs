using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Controller.Commands
{
    class QuitCommand : ICommand
    {
        private MazeModel model;

        public QuitCommand(IModel model)
        {
            this.model = model as MazeModel;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string Name = args[0];

            //Gets the TCPClient that is playing against this client
            List<TcpClient> opponent = model.GetOpponents(client);
            model.EndGame(Name);
            string str;
            foreach (TcpClient O in opponent)
            {
                //Stream for opponent
                NetworkStream stream = O.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                str = "quit";
                Console.WriteLine("quit");
                writer.WriteLine(str);
                //writer.WriteLine("#");
                writer.Flush();
                //writer = new StreamWriter(stream);
                // writer.Flush();
            }
            return "";
        }
    }
}
