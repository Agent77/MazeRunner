using MazeRunner.Models;
using MazeRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeRunner.Windows
{
    /// <summary>
    /// Interaction logic for MultiStartWindow.xaml
    /// </summary>
    public partial class MultiStartWindow : Window
    {
        /// <summary>
        /// ViewModel member
        /// </summary>
        private MultiViewModel myVM;
        /// <summary>
        /// constructor
        /// </summary>
        public MultiStartWindow()
        {
            InitializeComponent();
            MultiMazeModel m = new MultiMazeModel();
            int success = m.Connect();
            if (success < 0)
            {
                ConnectionFailedWindow c = new ConnectionFailedWindow();
                c.Show();
            }
            if (success >= 0)
            {
                m.GetListOfGames();
                myVM = new MultiViewModel(m);
                DataContext = myVM;
                GameInfo.btnStart.Click += BtnStart_Click;
                this.Show();
            }
           
        }
        /// <summary>
        /// Joins the game selected in the comboBox
        /// </summary>
        /// <param name="sender">This window</param>
        /// <param name="e">sd</param>
        private void JoinClicked(object sender, RoutedEventArgs e)
        {
            MultiMazeModel m = myVM.MyModel as MultiMazeModel;
            int success = m.Connect();
            if (success < 0)
            {
                ConnectionFailedWindow c = new ConnectionFailedWindow();
                c.Show();
            }
            if (success >= 0)
            {
                GameInfo.btnStart.Click += BtnStart_Click;
                this.Show();
                m.GetListOfGames();
                m.StartGame("join");
                MultiGame mg = new MultiGame();
                mg.SetVM(myVM);
                mg.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Starts the game created by the player
        /// </summary>
        /// <param name="sender">window</param>
        /// <param name="e">none</param>
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            waitingWindow ww = new waitingWindow();
            ww.Show();
            MultiMazeModel m = myVM.MyModel as MultiMazeModel;
            m.StartGame("start");
            MultiGame mg = new MultiGame();
            mg.SetVM(myVM);
            myVM.VM_GameList = null;
            ww.Close();
            mg.Show();
            this.Close();
        }
        /// <summary>
        /// When the combo box is loaded
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">none</param>
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = myVM.VM_GameList;
        }
        /// <summary>
        /// When a selection is changed
        /// </summary>
        /// <param name="sender">ComboBox</param>
        /// <param name="e">none</param>
        private void ComboBox_SelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
        }
    }
}
