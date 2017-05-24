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
    /// <summary>
    /// Single viewmodel
    /// </summary>
    public class SingleViewModel : MazeViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        public SingleViewModel(IMazeModel model) : base(model)
        {
            MyModel = model as SingleMazeModel;
            MyModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);

            };
           
        }
        
    }

}
