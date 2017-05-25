using MazeLib;
using MazeRunner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Threading.Tasks;

namespace MazeRunner.ViewModels
{
    /// <summary>
    /// Abstract class for ViewModels
    /// </summary>
    public abstract class MazeViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Server Ip
        /// </summary>
        public static string ServerIP { get; set; }
        /// <summary>
        /// Server Port
        /// </summary>
        public static int Port { get; set; }
        /// <summary>
        /// Type of search algorithm to use
        /// </summary>
        public static int SearchType { get; set; }
        /// <summary>
        /// Model member
        /// </summary>
        public IMazeModel MyModel;
        /// <summary>
        /// Constructor, initializes values
        /// </summary>
        /// <param name="model">model</param>
        public MazeViewModel(IMazeModel model)
        {
            MyModel = model;
            string ip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            string row = ConfigurationManager.AppSettings["rows"];
            string col = ConfigurationManager.AppSettings["cols"];
            VM_Rows = Int32.Parse(row);
            VM_Cols = Int32.Parse(col);


        }
        /// <summary>
        /// Sets the model for the ViewModel
        /// </summary>
        /// <param name="m">model</param>
       public void SetModel(IMazeModel m)
        {
            MyModel = m;
            string ip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            string row = ConfigurationManager.AppSettings["rows"];
            string col = ConfigurationManager.AppSettings["cols"];
            VM_Rows = Int32.Parse(row);
            VM_Cols = Int32.Parse(col);
        }
        /// <summary>
        /// Notifies that a property has changed
        /// </summary>
        /// <param name="propertyName"> which property has been changed</param>
        public void NotifyPropertyChanged(string propertyName)
        {
           
        }
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// ViewModel player location property
        /// </summary>
        public Position VM_PlayerLocation
        {
            get
            {
                return MyModel.PlayerLocation;
            }
            set
            {
            }
        }
        /// <summary>
        /// ViewModel goal pos property
        /// </summary>
        public Position VM_GoalPos
        {
            get
            {
                return MyModel.GoalPos;
            }
            set
            {
            }
        }
        /// <summary>
        /// Maze in 2D array format member
        /// </summary>
        private string[] maze;
        /// <summary>
        /// ViewModel maze property
        /// </summary>
        public string[] VM_Maze
        {
            get
            {
                return maze ;
            }
            set
            {
                maze = value;
            }
        }
        /// <summary>
        /// Name member for maze
        /// </summary>
        private string name;
        /// <summary>
        /// name property for maze
        /// </summary>
        public string VM_Name
        {
            get
            {
                return MyModel.Name;
            }

            set
            {
                MyModel.SetName(value);
                name = value;
            }
        }
        /// <summary>
        /// Private col num in maze
        /// </summary>
        private int cols;
        /// <summary>
        /// ViewModel num of columns
        /// </summary>
        public int VM_Cols
        {
            get
            {
                return MyModel.Cols;
            }
            set
            {
                cols = value;
                MyModel.SetCols(value);
                
            }
        }
        /// <summary>
        /// Num of rows in maze
        /// </summary>
        private int rows;
        /// <summary>
        /// ViewModel rows property
        /// </summary>
        public int VM_Rows
        {
            get
            {
                return MyModel.Rows;
            }
            set
            {
                rows = value;
                MyModel.SetRows(value);
            }
        }
        /// <summary>
        /// ViewModel initial position property
        /// </summary>
        public Position VM_InitialPos
        {
            get
            {
                return MyModel.InitialPos;
            }
            set
            {
                VM_PlayerLocation = value;
            }
        }
        /// <summary>
        /// Request model to solve the maze
        /// </summary>
        /// <returns>solution to maze</returns>
        public string SolveMaze()
        {
            return MyModel.SolveMaze();
        }
  
    }


}
