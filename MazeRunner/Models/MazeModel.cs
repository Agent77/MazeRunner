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
    /// <summary>
    /// Abstract class implementing IMazeModel
    /// </summary>
    public abstract class MazeModel : IMazeModel
    {
        /// <summary>
        /// Event for INotifyPropertyChanged aspects
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// num of cols in maze
        /// </summary>
        private int cols;
        /// <summary>
        /// num of rows in maze
        /// </summary>
        private int rows;
        /// <summary>
        /// maze member
        /// </summary>
        private Maze maze;
        /// <summary>
        /// Maze property
        /// </summary>
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
        /// <summary>
        /// Cols property
        /// </summary>
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
        /// <summary>
        /// Rows property
        /// </summary>
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
        /// private member for name of maze
        /// </summary>
        private string name;
        /// <summary>
        /// public property for name of maze
        /// </summary>
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
        /// Private member of position of start
        /// </summary>
        private Position initialPos;
        /// <summary>
        /// Public property of start position
        /// </summary>
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
        /// goal position
        /// </summary>
        private Position goalPos;
        /// <summary>
        /// Property for goal position
        /// </summary>
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
        /// Player location on maze
        /// </summary>
        private Position playerLocation;
        /// <summary>
        /// Player location on maze property
        /// </summary>
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
        /// Server Ip public property
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// Port to connect to server
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// TCP connector method
        /// </summary>
        public ClientCommunicator TcpMessenger { get; set; }

        /// <summary>
        /// Function called to notify listeners a property was changed
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new 
                PropertyChangedEventArgs(propName));
        }
        /// <summary>
        /// Constructor for MazeModel
        /// </summary>
        public MazeModel()
        {

        }
        /// <summary>
        /// Sends a request to send a maze by the name
        /// given by user, and sets the member 'MyMaze'
        /// once the maze is created
        /// </summary>
        /// <param name="action"> start or generate</param>
        public void SendMaze(string action)
        {
            string s = action;
            s += " " + Name + " " + Rows + " " + Cols;
            TcpMessenger.Write(s);
                string maze = TcpMessenger.Read();
                MyMaze = Maze.FromJSON(maze);
                InitialPos = MyMaze.InitialPos;
                GoalPos = MyMaze.GoalPos;
        }

        /// <summary>
        /// Sets name of maze
        /// </summary>
        /// <param name="s">name</param>
        public void SetName(string s)
        {
            Name = s;
        }
      /// <summary>
      /// Connects to server
      /// </summary>
      /// <returns></returns>
        public int Connect()
        {
            TcpMessenger = new ClientCommunicator();
            int success = TcpMessenger.Connect(ServerIp, Port);
            return success;
        }
        /// <summary>
        /// Disconnects from server
        /// </summary>
        public void Disconnect()
        {
            TcpMessenger.Disconnect();
        }
        /// <summary>
        /// Sends to server that this player
        /// has won the game and therefore
        /// the game has ended
        /// </summary>
        public void CloseGame()
        {
            string s = "close";
            s += " " + Name;
            TcpMessenger.Write(s);
        }
        /// <summary>
        /// Notifies server that this player
        /// is quiting the game, before finishing
        /// </summary>
        public void QuitGame()
        {
            string s = "quit";
            s += " " + Name;
            TcpMessenger.Write(s);
        }
        /// <summary>
        /// Returns the maze in the form of an array of strings
        /// </summary>
        /// <returns></returns>
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
        /// Sends a request to server to join a game,
        /// and gets in return the maze sent by the server
        /// </summary>
        public void Join()
        {
            string s = "join ";
            s += Name;
            TcpMessenger.Write(s);
            string maze = TcpMessenger.Read();
            MyMaze = Maze.FromJSON(maze);
            InitialPos = MyMaze.InitialPos;
            GoalPos = MyMaze.GoalPos;
            Rows = MyMaze.Rows;
            Cols = MyMaze.Cols;
        }
        /// <summary>
        /// Sets the rows of the model
        /// </summary>
        /// <param name="r"></param>
        public void SetRows(int r)
        {
            rows = r;
        }
        /// <summary>
        /// Sets the cols of the model
        /// </summary>
        /// <param name="c"></param>
        public void SetCols(int c)
        {
            cols = c;
        }
        /// <summary>
        /// sends a request to the server to solve
        /// the maze, and receives in return the 
        /// solution
        /// </summary>
        /// <returns>solution in string form</returns>
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
            string solution = TcpMessenger.Read();
            string[] sol1 = solution.Split(',');
            string[] sol2 = sol1[1].Split(':');
            string[] sol3 = sol2[1].Split('"');
            return sol3[1];
        }

        
    }
}
