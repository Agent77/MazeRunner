using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MazeRunner.Models
{
    /// <summary>
    /// Multi player maze model
    /// </summary>
    public class MultiMazeModel : MazeModel
    {

        /// <summary>
        /// Event handler for multi maze model
        /// </summary>
        /// <param name="o">na</param>
        /// <param name="e">na</param>
        public  delegate void EventHandler(object o, Key e);

        /// <summary>
        /// Event for when opponent moved
        /// </summary>
        public event EventHandler OpponentMoved;
        /// <summary>
        /// Function to call when event is called
        /// </summary>
        /// <param name="e">direction opponent moved</param>
        protected virtual void OnOpponentMoved(Key e)
        {
            if (OpponentMoved != null)
                OpponentMoved(this, e);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MultiMazeModel() : base()
        {
        }
        /// <summary>
        /// sends to server their move
        /// </summary>
        /// <param name="direction">direction they moved</param>
        public void MovePlayer(string direction)
        {
            string s = "play ";
            s += direction;
            TcpMessenger.Write(s);

           
        }
        /// <summary>
        /// Starts or joins the game, and begins the
        /// task of receiving the opponents moves
        /// </summary>
        /// <param name="action"> start or join</param>
        public void StartGame(string action)
        {
            string s;
            if (action == "join")
            {
                s = "join ";
                s += Name;
            }
            else
            {
                s = "start ";
                s += Name + " " + Rows + " " + Cols;
            }
            
            TcpMessenger.Write(s);
            string maze = TcpMessenger.Read();
            MyMaze = Maze.FromJSON(maze);
            InitialPos = MyMaze.InitialPos;
            GoalPos = MyMaze.GoalPos;
            if (action == "join")
            {
                Rows = MyMaze.Rows;
                Cols = MyMaze.Cols;
            }

            new Task(() =>
            {
                while (true)
                {
                    string pos = TcpMessenger.Read();


                    if (pos.Contains("Left"))
                    {
                        OnOpponentMoved(Key.Left);
                    }
                    else if (pos.Contains("Right"))
                    {
                        OnOpponentMoved(Key.Right);
                    }
                    else if (pos.Contains("Up"))
                    {
                        OnOpponentMoved(Key.Up);
                    }
                    else if (pos.Contains("Down"))
                    {
                        OnOpponentMoved(Key.Down);
                    }
                    else if (pos.Contains("close"))
                    {
                        OnOpponentMoved(Key.Delete);
                    }
                    else if (pos.Contains("quit"))
                    {
                        OnOpponentMoved(Key.Back);
                    }

                }

            }).Start();
        }
        
        /// <summary>
        /// Gets a list of the games the player can join
        /// </summary>
        /// <returns>list of games</returns>
        public ObservableCollection<string> GetListOfGames()
        {
            string request = "list";
            TcpMessenger.Write(request);
            string response = TcpMessenger.Read();
            response =  response.Replace("\n", "");
            response = response.Replace("[", "");
            response = response.Replace("]", "");
            response = response.Replace("\"", "");
            string[] games = response.Split(',');
            games = games.Take(games.Count() - 1).ToArray();
            List<string> list = games.ToList<string>();
            gameList = new ObservableCollection<string>(list);
            return gameList;
        }
        /// <summary>
        /// List of games in server
        /// </summary>
        private ObservableCollection<string> gameList;
        public ObservableCollection<string> GameList
        {
            get
            {
                return gameList;
            }
            set
            {
                gameList = value;
            }
        }

    }
}
