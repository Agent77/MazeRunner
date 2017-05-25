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
        /// <summary>
        /// ViewModel member
        /// </summary>
        private MazeViewModel myVM;
        /// <summary>
        /// constructor
        /// </summary>
        public SingleGame()
        {
            InitializeComponent();
            KeyDown += Board_KeyDown;
        }
        /// <summary>
        /// Sets the view model for the window
        /// </summary>
        /// <param name="m"></param>
        public void SetVM(MazeViewModel m)
        {
            myVM = m;
            this.DataContext = myVM;
            myVM.VM_Maze = myVM.MyModel.MazeString();


        }
        /// <summary>
        /// When a button is click
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">none</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        /// <summary>
        /// When the User COntrol is loaded,
        /// this function draws the maze
        /// </summary>
        /// <param name="sender">Window</param>
        /// <param name="e">none</param>
        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard(false);
        }
        /// <summary>
        /// Used when a keyboard key is pressed,
        /// and moves the player accordingly
        /// </summary>
        /// <param name="sender">keyboard</param>
        /// <param name="e">key pressed</param>
        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            int check;
            Key k = e.Key;
            //Moves player
            check=Board.MovePlayer(k);
            //Player arrived at goal
            if (check == 3)
            {
                FinishWindow fw = new FinishWindow();
                fw.ShowDialog();
                fw.sPlayer.Stop();
                BackToMain();
            }
        }
        /// <summary>
        /// Disconnects from server
        /// and returns to main menu
        /// </summary>
        private void BackToMain()
        {
            myVM.MyModel.Disconnect();
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
        /// <summary>
        /// Returns to main menu
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">none</param>
        private void Main_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to return to main menu?";
            string caption = "Confirmation";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, 
                caption, buttuon);
            if (result == MessageBoxResult.OK)
            {
                BackToMain();
            }
           
        }
        /// <summary>
        /// Restarts the game
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">none</param>
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to restart game?";
            string caption = "Confirmation";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption,
                buttuon);
            if (result==MessageBoxResult.OK) {
                Board.RestartGame();
            }
        }
        /// <summary>
        /// Sends a request for a solution to the 
        /// maze, and the user control is then
        /// asked to animate it on the maze
        /// itself
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">none</param>
        private void Solve_Click(object sender, RoutedEventArgs e)
        {
           string solution = myVM.MyModel.SolveMaze();
           Board.SolveMaze(solution);
        }
    }
}
