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

        /// <summary>
        /// Represents columns of maze
        /// </summary>
        private int cols;
        /// <summary>
        /// Represents rows of maze
        /// </summary>
        private int rows;
        /// <summary>
        /// Represents the maze
        /// </summary>
        private Maze maze;
        public Maze MyMaze
        {
            get
            {
                return maze;
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
        

        /// <summary>
        /// Represents the name of the maze
        /// </summary>
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

        /// <summary>
        /// Represents initial position of maze
        /// </summary>
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

        /// <summary>
        /// Represents goal position of maze
        /// </summary>
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

        /// <summary>
        /// Represents player's position in maze
        /// </summary>
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

        /// <summary>
        /// Represents ip of server
        /// </summary>
        public string ServerIp { get; set; }
        
        /// <summary>
        /// Represents port of server
        /// </summary>
        public string Port { get; set; }
        public ClientCommunicator TcpMessenger { get; set; }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MazeModel()
        {

        }

        /// <summary>
        /// Method that gets generate command, send it to server and create maze in client
        /// </summary>
        /// <param name="action"></param>
        public void SendMaze(string action)
        {
            string s = action;
            s += " " + Name + " " + Rows + " " + Cols;
            TcpMessenger.Write(s);
            string maze = TcpMessenger.read();
            MyMaze = Maze.FromJSON(maze);
            InitialPos = MyMaze.InitialPos;
            GoalPos = MyMaze.GoalPos;
        }


        public void SetName(string s)
        {
            Name = s;
        }

        /// <summary>
        /// Ctreate connection to server
        /// </summary>
        /// <returns></returns>
        public int Connect()
        {
            TcpMessenger = new ClientCommunicator();
            int success = TcpMessenger.Connect(ServerIp, Port);
            return success;
        }

        /// <summary>
        /// Disconnecting from server
        /// </summary>
        public void Disconnect()
        {
            TcpMessenger.disconnect();
        }

        /// <summary>
        /// Sending server message that current game id closed
        /// </summary>
        public void CloseGame()
        {
            string s = "close";
            s += " " + Name;
            TcpMessenger.Write(s);
        }

        /// <summary>
        /// Sending server message that player exited game
        /// </summary>
        public void QuitGame()
        {
            string s = "quit";
            s += " " + Name;
            TcpMessenger.Write(s);
        }

        /// <summary>
        /// Parsing the maze object to a 2 dimensional string array
        /// </summary>
        /// <returns>Maze string representation</returns>
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

        /// <summary>
        /// Join coammand sent to server
        /// </summary>
        public void Join()
        {
            string s = "join ";
            s += Name;
            TcpMessenger.Write(s);
            string maze = TcpMessenger.read();
            MyMaze = Maze.FromJSON(maze);
            InitialPos = MyMaze.InitialPos;
            GoalPos = MyMaze.GoalPos;
            Rows = MyMaze.Rows;
            Cols = MyMaze.Cols;
        }
        
        public void SetRows(int r)
        {
            rows = r;
        }

        public void SetCols(int c)
        {
            cols = c;
        }

        /// <summary>
        /// Sending to maze request for solving maze
        /// </summary>
        /// <returns>Solution string</returns>
        public string SolveMaze()
        {
            string algorithm = ConfigurationManager.AppSettings["algorithm"];
            string s = "solve";
            if(algorithm == "BFS")
            {
                s += " " + Name + " " + "0";

            } else
            {
                s += " " + Name + " " + "1";
            }
            TcpMessenger.Write(s);
            string solution = TcpMessenger.read();
            string[] sol1 = solution.Split(',');
            string[] sol2 = sol1[1].Split(':');
            string[] sol3 = sol2[1].Split('"');
            return sol3[1];
        }

        
    }
}
