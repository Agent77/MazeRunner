﻿using System;
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
            //gameList = GetListOfGames();
        }
        public void MovePlayer(string direction)
        {
            //base.MovePlayer(direction);
            string s = "play ";
            s += direction;
            TcpMessenger.Write(s);

           
        }

        public void Join(string action)
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

        public void BeginMoves(string action)
        {
            string s = action;
            if (action == "start")
            {

                s += " " + Name + " " + Rows + " " + Cols;
            }
            else
            {
                s += " " + Name;
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
                    else if (pos.Contains("close"))
                    {
                        OnOpponentMoved(Key.Delete);
                    }
                    else if (pos.Contains("quite"))
                    {
                        OnOpponentMoved(Key.Back);
                    }
                }
            }).Start();
        }

    }
}
