using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner.Models
{
    public abstract class MazeModel : IMazeModel
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private int cols;
        private int rows;
        private Maze maze;
        public Maze MyMaze
        {
            get
            {
                return maze;
                //TODO return MyMaze in string form
            }
            set
            {
                maze = value;
                NotifyPropertyChanged("MyMaze");
            }
        }

        public int Cols
        {
            get
            {
                return cols;
            }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");

            }
        }
        public int rowsTry { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private Position initialPos;
        public Position InitialPos
        {
            get
            {
                return initialPos;
            }
            set
            {
                initialPos = value;
                NotifyPropertyChanged("InitialPos");
            }
        }


        private Position goalPos;
        public Position GoalPos
        {
            get
            {
                return goalPos;
            }
            set
            {
                goalPos = value;
                NotifyPropertyChanged("GoalPos");
            }
        }

        private Position playerLocation;
        public Position PlayerLocation
        {
            get
            {
                return playerLocation;
            }
            set
            {
                playerLocation = value;
                NotifyPropertyChanged("PlayerLocation");
            }
        }

        



        public string ServerIp { get; set; }
        public string Port { get; set; }
        public ClientCommunicator TcpMessenger { get; set; }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public MazeModel()
        {
             TcpMessenger = new ClientCommunicator();
             TcpMessenger.Connect(ServerIp, Port);

        }

        public void SendMaze(string action)
        {
            string s = action;
            s += " " + Name + " " + Rows + " " + Cols;
            TcpMessenger.Write(s);

            //new Task(() =>
            //{
                string maze = TcpMessenger.read();
                MyMaze = Maze.FromJSON(maze);
                InitialPos = MyMaze.InitialPos;
                GoalPos = MyMaze.GoalPos;


            //}).Start();
        }


        public void SetName(string s)
        {
            Name = s;
        }
        public string GetMaze()
        {
            //RETURN STRING OF MAZE? or array?
            return "01001";
        }


        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            string s = "close";
            s += " "+ Name;
            TcpMessenger.Write(s);
            TcpMessenger.read();
            TcpMessenger.disconnect();
        }

        public string[] MazeString()
        {
            string[] wholeString = new string[Rows];
            
            int row = 0;
            int col = 0;
            int place = 0;
            for(row = 0; row < Rows; row++)
            {
                string mazeString = null;
                for (col = 0; col < Cols; col++)
                {
                    if (InitialPos.Row == row && InitialPos.Col == col)
                    {
                        mazeString += '*';
                    }
                    else if (GoalPos.Row == row && GoalPos.Col == col)
                    {
                        mazeString += '#';
                    }
                    else
                    {
                        if (MyMaze[row, col] == 0)
                            mazeString += '0';
                        else
                            mazeString += '1';
                    }
                }
                wholeString[place] = mazeString;
                place++;
            }
            return wholeString;
        }


        
        public void SetRows(int r)
        {
            rows = r;
        }

        public void SetCols(int c)
        {
            cols = c;
        }

        public string SolveMaze()
        {
            string algorithm = ConfigurationManager.AppSettings["algorithm"];
            string s = "solve";
            s += " " + Name + " " + "1";
            TcpMessenger.Write(s);
            string solution = TcpMessenger.read();
            string[] sol1 = solution.Split(',');
            string[] sol2 = sol1[1].Split(':');
            string[] sol3 = sol2[1].Split('"');
            return sol3[1];
        }
     
    }
}
