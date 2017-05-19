using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeRunner.Models;
using MazeLib;
using System.ComponentModel;

namespace MazeRunner.ViewModels
{
    public class MultiViewModel : MazeViewModel
    {
        MultiMazeModel MyModel;
        public MultiViewModel(IMazeModel model) : base(model)
        {
            MyModel = model as MultiMazeModel;
            MyModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
        }

        private Position oppPos;
        public Position VM_OppPos
        {
            get
            {
                return MyModel.OppPos;
            }
            set
            {
                oppPos = value;

            }
        }
        public void MovePlayer(string direction)
        {
            MyModel.MovePlayer(direction);
            VM_OppPos = MyModel.OppPos;
        }
    }
}
