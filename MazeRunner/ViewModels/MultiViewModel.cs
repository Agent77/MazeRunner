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
    /// <summary>
    /// Extends MazeViewModel, for multi player games
    /// </summary>
    public class MultiViewModel : MazeViewModel
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public MultiViewModel(IMazeModel model) : base(model)
        {
            MyModel = (MultiMazeModel)model;
            
            MyModel.PropertyChanged += delegate (Object sender, 
                PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
        }
        /// <summary>
        /// Opponent Position on maze
        /// </summary>
        private Position oppPos;
        /// <summary>
        /// Opponent position property
        /// </summary>
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
        /// <summary>
        /// List of games the player is able to join
        /// </summary>
        private ObservableCollection<string> gameList;
        /// <summary>
        /// gameList property
        /// </summary>
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
        /// <summary>
        /// Moves the player on the maze
        /// in a certain direction
        /// </summary>
        /// <param name="direction"> which way to move</param>
        public void MovePlayer(string direction)
        {
            MultiMazeModel m = MyModel as MultiMazeModel;
             m.MovePlayer(direction);
        }
    }
}
