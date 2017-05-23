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
        private MultiViewModel myVM;
        public MultiStartWindow()
        {
            InitializeComponent();
            MultiMazeModel m = new MultiMazeModel();
            myVM = new MultiViewModel(m);
            DataContext = myVM;
            GameInfo.btnStart.Click += BtnStart_Click;

        }

        private void JoinClicked(object sender, RoutedEventArgs e)
        {
            
           
            MultiMazeModel m = myVM.MyModel as MultiMazeModel;
            m.Join();
            MultiGame mg = new MultiGame();
            mg.SetVM(myVM);

            /* mg.Board.Rows = mg.OpponentBoard.Rows;
             mg.Board.Cols = mg.OpponentBoard.Cols;*/
            mg.Show();
            this.Close();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            waitingWindow ww = new waitingWindow();
            ww.Show();
            myVM.MyModel.SendMaze("start");
           // ww.Close();
            MultiMazeModel m = myVM.MyModel as MultiMazeModel;
            m.BeginMoves();
            

            MultiGame mg = new MultiGame();
            mg.SetVM(myVM);
            myVM.VM_GameList = null;

            ww.Close();
            mg.Show();

            this.Close();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {


            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = myVM.VM_GameList;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;




        }
    }
}
