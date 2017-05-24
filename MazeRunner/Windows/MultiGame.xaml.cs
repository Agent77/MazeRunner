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
    /// Interaction logic for MultiGame.xaml
    /// </summary>
    public partial class MultiGame : Window
    {
        private MultiViewModel myVM;

        public MultiGame()
        {
            InitializeComponent();
            KeyDown += Board_KeyDown;

        }


        public void SetVM(MazeViewModel m)
        {
           // Debug d = new Debug();
           // d.Show();
            myVM = m as MultiViewModel;
            this.DataContext = myVM;
            MultiMazeModel tempModel = myVM.MyModel as MultiMazeModel;
            tempModel.OpponentMoved += delegate (Object sender, Key e)
            {

                int close = 10;
                this.Dispatcher.Invoke(() =>
                {
                  close = OpponentBoard.MovePlayer(e);

                    if (close  == -1)
                    {
                        LoserWindow lw = new LoserWindow();
                        lw.Show();
                        MainMenu_Click(lw, null);

                    }
                  if(close == -2)
                    {
                        Debug d = new Debug();
                        d.SetText("PLAYER QUIT");
                        d.Show();
                        OtherPlayerQuit();
                    }
                });
                

            };
            myVM.VM_Maze = myVM.MyModel.MazeString();
        }

        private void OtherPlayerQuit()
        {
            string message = "Other Play quit the game!";
            string caption = "Game Ended";
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

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard(false);
            OpponentBoard.DrawBoard(true);

        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;
            Board.MovePlayer(k);
            Position pos;
            MultiMazeModel tempModel = myVM.MyModel as MultiMazeModel;

            switch (k)
            {
                case Key.Left:
                    tempModel.MovePlayer("Left");
                    break;
                case Key.Right:
                    tempModel.MovePlayer("Right");
                    break;
                case Key.Up:
                    tempModel.MovePlayer("Up");
                    break;
                case Key.Down:
                    tempModel.MovePlayer("Down");
                    break;
            }


        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to return to main menu?";
            string caption = "Confirmation";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttuon);
            if (result == MessageBoxResult.OK)
            {
                if (Board.FinishedGame)
                {
                    myVM.MyModel.CloseGame();

                }
                else if (OpponentBoard.FinishedGame)
                {
                    LoserWindow lw = (LoserWindow)sender;
                    lw.Close();
                }
                else if (!Board.FinishedGame)
                {
                    Debug d = new Debug();
                    d.SetText("I QUIT");
                    d.Show();
                    myVM.MyModel.QuitGame();
                }
               

                else if (!OpponentBoard.FinishedGame)
                {

                    myVM.MyModel.QuitGame();
                }
                MainWindow m = new MainWindow();
                m.Show();
                myVM.MyModel.Disconnect();
                this.Close();
            }
        }
    }
}

