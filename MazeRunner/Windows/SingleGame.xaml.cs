using MazeLib;
using MazeRunner.Models;
using MazeRunner.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SingleGame.xaml
    /// </summary>
    public partial class SingleGame : Window
    {
        
        private MazeViewModel myVM;
        

        public SingleGame()
        {

            //myVM = new SingleViewModel(new SingleMazeModel());
            //this.DataContext = myVM;
            InitializeComponent();
            KeyDown += Board_KeyDown;
            

        }

        public void SetVM(MazeViewModel m)
        {
            myVM = m;
            this.DataContext = myVM;
            myVM.VM_Maze = myVM.MyModel.MazeString();


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard(false);
            

        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;
            Board.MovePlayer(k);
           
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to return to main menu?";
            string caption = "Confirmation";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttuon);
            if (result == MessageBoxResult.OK)
            {
                myVM.MyModel.Disconnect();
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
           
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to restart game?";
            string caption = "Confirmation";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttuon);
            if (result==MessageBoxResult.OK) {
                Board.RestartGame();
            }
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
           string solution = myVM.MyModel.SolveMaze();
           Board.SolveMaze(solution);

        }
    }
}
