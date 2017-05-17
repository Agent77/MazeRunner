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
            VM_Rows = Int32.Parse(row);
            VM_Cols = Int32.Parse(col);
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
