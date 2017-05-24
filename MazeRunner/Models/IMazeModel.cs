﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner.Models
{
    public interface IMazeModel : INotifyPropertyChanged
    {
        Maze MyMaze { get; set; }

        int Cols { get; set; }

        int Rows { get; set; }

        string Name { get; set; }

        Position InitialPos { get; set; }

        Position GoalPos { get; set; }

        Position PlayerLocation { get; set; }
        
        ClientCommunicator TcpMessenger { get; set; }

        string ServerIp { get; set; }

        string Port { get; set; }

        
        void NotifyPropertyChanged(string propName);
        void SendMaze(string action);
        void Join();
        
        string[] MazeString();
        string SolveMaze();
        void SetRows(int r);
        void SetCols(int c);
        void CloseGame();
        void QuitGame();
        // Position MovePlayer(string direction);
        void SetName(string s);
        //MazeLib Maze;
        //RunnerPosition;

        int Connect();
        void Disconnect();
    }

}
