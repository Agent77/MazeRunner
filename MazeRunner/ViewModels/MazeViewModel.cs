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

    public abstract class MazeViewModel : INotifyPropertyChanged
    {
        public static string ServerIP { get; set; }
        public static int Port { get; set; }
        public static int SearchType { get; set; }
        public IMazeModel MyModel;
        //all other MyMazeModel properties (except client)
        //rows, columns, name of maze (for data binding)

        public MazeViewModel(IMazeModel model)

        {
            string ip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            string row = ConfigurationManager.AppSettings["rows"];
            string col = ConfigurationManager.AppSettings["cols"];
            VM_Rows = Int32.Parse(row);
            VM_Cols = Int32.Parse(col);


        }
        public void NotifyPropertyChanged(string propertyName)
        {
            //  VM_Name = MyModel.getName();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Position VM_PlayerLocation
        {
            get
            {
                return MyModel.PlayerLocation;
            }
            set
            {
               // MyModel.MovePlayer(value);
            }
        }

        public Position VM_GoalPos
        {
            get
            {
                return MyModel.GoalPos;
            }
            set
            {
                // MyModel.MovePlayer(value);
            }
        }

        private string[] maze;
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
        private string name;
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
        private int cols;
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

        private int rows;
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

        public Position VM_InitialPos
        {
            get
            {
                return MyModel.InitialPos;
            }
            set
            {
                
            }
        }



     

        public void MovePlayer(string direction)
        {
            throw new NotImplementedException();
        }
    }


}
