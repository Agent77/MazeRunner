using MazeRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MazeRunner.ViewModels
{
    public class SingleViewModel : MazeViewModel
    {

        public SingleViewModel(IMazeModel model) : base(model)
        {
            //MyModel = model as SingleMazeModel;
            string ip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            string row = ConfigurationManager.AppSettings["rows"];
            string col = ConfigurationManager.AppSettings["cols"];
            rows = Int32.Parse(row);
            cols = Int32.Parse(col);
        }


        public string VM_PlayerLocation
        {
            get
            {
                return MyModel.PlayerLocation;
            }
            set
            {
                //MyModel.MovePlayer(value ie new position);
            }
        }
        //private string name;
        public string VM_Name
        {
            get
            {
                return MyModel.Name;
            }

            set
            {
                //name = value;
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

                Console.WriteLine("BINDING WORKS: {0}", MyModel.Cols);
            }
        }

        private int rows;
       

        public void ExecuteCommand(string s)
        {
            // MyModel.SendRequest('string' from user);
            //Other commands...Add controller to model
        }

        public void InitializeCommands()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void SetModel(IMazeModel m)
        {
            throw new NotImplementedException();
        }

    }

}
