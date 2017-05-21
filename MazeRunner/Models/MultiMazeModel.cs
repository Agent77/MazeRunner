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
    public class MultiMazeModel : MazeModel
    {

        
        public  delegate void EventHandler(object o, Key e);

        public event EventHandler OpponentMoved;

        protected virtual void OnOpponentMoved(Key e)
        {
            if (OpponentMoved != null)
                OpponentMoved(this, e);
        }

        public MultiMazeModel() : base()
        {
            gameList = GetListOfGames();
        }
        public void MovePlayer(string direction)
        {
            //base.MovePlayer(direction);
            string s = "play ";
            s += direction;
            TcpMessenger.Write(s);

           
        }

        public void Join()
        {
            string s = "join ";
            s += Name;
            TcpMessenger.Write(s);
            string maze = TcpMessenger.read();
            MyMaze = Maze.FromJSON(maze);
            InitialPos = MyMaze.InitialPos;
            GoalPos = MyMaze.GoalPos;
            
            new Task(() =>
            {
                while (true)
                {
                    string pos = TcpMessenger.read();
                    
                    //Position currPos = oppPos;

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
                }

            }).Start();
        }
        

        public ObservableCollection<string> GetListOfGames()
        {
            string request = "list";
            TcpMessenger.Write(request);
            string response = TcpMessenger.read();
            response =  response.Replace("\n", "");
            response = response.Replace("[", "");
            response = response.Replace("]", "");
            response = response.Replace("\"", "");
            string[] games = response.Split(',');
            
            List<string> list = games.ToList<string>();
            gameList = new ObservableCollection<string>(list);
            return gameList;
        }
        
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
                //gameList = GetListOfGames();
                //NotifyPropertyChanged("GameList");
            }
        }

        public void BeginMoves()
        {
            new Task(() =>
            {
                while (true)
                {
                    string pos = TcpMessenger.read();

                    //Position currPos = oppPos;

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
                }

            }).Start();
        }

    }
}
