using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeRunner.Models
{
    /// <summary>
    /// Interface for the Maze Models
    /// </summary>
    public interface IMazeModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Maze member of model
        /// </summary>
        Maze MyMaze { get; set; }
        /// <summary>
        /// Columns of maze
        /// </summary>
        int Cols { get; set; }
        /// <summary>
        /// Rows of maze
        /// </summary>
        int Rows { get; set; }
        /// <summary>
        /// name of maze
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// start position of player
        /// </summary>
        Position InitialPos { get; set; }
        /// <summary>
        /// goal position
        /// </summary>
        Position GoalPos { get; set; }
        /// <summary>
        /// current player location
        /// </summary>
        Position PlayerLocation { get; set; }
        /// <summary>
        /// tcp connection to server - can read and write
        /// </summary>
        ClientCommunicator TcpMessenger { get; set; }
        /// <summary>
        /// serverIp
        /// </summary>
        string ServerIp { get; set; }
        /// <summary>
        /// server port
        /// </summary>
        string Port { get; set; }
        /// <summary>
        /// Function to implement INofityPropertyChanged
        /// </summary>
        /// <param name="propName">property that was changed</param>
        void NotifyPropertyChanged(string propName);
        /// <summary>
        /// Sends request to receive maze and sets
        /// the maze member on return
        /// </summary>
        /// <param name="action"></param>
        void SendMaze(string action);
        /// <summary>
        /// Joins a specific game
        /// </summary>
        void Join();
        /// <summary>
        /// Converts the maze into a string of 1's and 0's
        /// </summary>
        /// <returns> 2D array of maze</returns>
        string[] MazeString();
        /// <summary>
        /// Requests the solution to a maze
        /// </summary>
        /// <returns>the solution to the maze</returns>
        string SolveMaze();
        /// <summary>
        /// sets the rows
        /// </summary>
        /// <param name="r"> num of rows</param>
        void SetRows(int r);
        /// <summary>
        /// Sets the cols
        /// </summary>
        /// <param name="c"> num of cols </param>
        void SetCols(int c);
        /// <summary>
        /// Sends to server that this player has won
        /// and is ending the game
        /// </summary>
        void CloseGame();
        /// <summary>
        /// sends to server that this player is quiting
        /// the game
        /// </summary>
        void QuitGame();
        /// <summary>
        /// Sets the name of the maze
        /// </summary>
        /// <param name="s"> maze name</param>
        void SetName(string s);
        /// <summary>
        /// Connects to server
        /// </summary>
        /// <returns></returns>
        int Connect();
        /// <summary>
        /// Disconnects to server
        /// </summary>
        void Disconnect();
    }

}
