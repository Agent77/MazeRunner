using MazeLib;
using MazeRunner.Models;
using MazeRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool alreadyOut = false;
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

                    if (close  == -1 || close == 2)
                    {
                        LoserWindow lw = new LoserWindow();
                        lw.ShowDialog();
                        //MainMenu_Click(lw, null);
                        BackToMain();

                    }
                  if(close == -2)
                    {
                        OtherPlayerQuit();
                    }
                });
                

            };
            myVM.VM_Maze = myVM.MyModel.MazeString();
        }

        private void BackToMain()
        {
            alreadyOut = true;
            myVM.MyModel.Disconnect();
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void OtherPlayerQuit()
        {
            string message = "Other Play quit the game!";
            string caption = "Game Ended";
            MessageBoxButton buttuon = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show(message, caption, buttuon);
            if (result == MessageBoxResult.OK)
            {                
                BackToMain();
            }
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard(false);
            OpponentBoard.DrawBoard(true);

        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            int check;
            Key k = e.Key;
            check=Board.MovePlayer(k);
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
            if (check == 3)
            {
                FinishWindow fw = new FinishWindow();
                fw.ShowDialog();
                myVM.MyModel.CloseGame();
                fw.sPlayer.Stop();
                BackToMain();
                fw.Close();
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
                myVM.MyModel.QuitGame();
                BackToMain();
                
                /* if (OpponentBoard.FinishedGame)
                {
                    LoserWindow lw = (LoserWindow)sender;
                    lw.Close();
                }
               else if (Board.FinishedGame)
                {
                    myVM.MyModel.CloseGame();

                }
                else if (!Board.FinishedGame)
                {
                    myVM.MyModel.QuitGame();
                }
                else if(!OpponentBoard.FinishedGame)
                {
                    myVM.MyModel.QuitGame();
                }*/
                
               
               
                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (alreadyOut==false)
            {
                myVM.MyModel.QuitGame();
                BackToMain();
            }
        }
    }
}

