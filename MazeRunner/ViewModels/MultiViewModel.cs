using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeRunner.Models;
using MazeLib;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MazeRunner.ViewModels
{
    public class MultiViewModel : MazeViewModel
    {
       // public MultiMazeModel MyModel;

        public MultiViewModel(IMazeModel model) : base(model)
        {
            MyModel = (MultiMazeModel)model;
            
            //base.SetModel(MyModel);

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
                return oppPos;
            }
            set
            {
                oppPos = value;

            }
        }

        private ObservableCollection<string> gameList;
        public ObservableCollection<string> VM_GameList
        {
            get
            {
                MultiMazeModel m = MyModel as MultiMazeModel;
                gameList = m.GameList;
                return gameList;
            }
            set
            {
                gameList = value;
            }
        }
        public void MovePlayer(string direction)
        {
            MultiMazeModel m = MyModel as MultiMazeModel;
             m.MovePlayer(direction);
        }

        public void MoveEnemy(Position p)
        {

        }
    }
}
