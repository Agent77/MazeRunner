using MazeRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MazeLib;
using System.ComponentModel;

namespace MazeRunner.ViewModels
{
    public class SingleViewModel : MazeViewModel
    {
       // public SingleMazeModel MyModel;


        public SingleViewModel(IMazeModel model) : base(model)
        {
            MyModel = model as SingleMazeModel;
            MyModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
           
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
