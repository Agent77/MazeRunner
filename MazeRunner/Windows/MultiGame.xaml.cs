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
            myVM = m as MultiViewModel;
            this.DataContext = myVM;
            MultiMazeModel tempModel = myVM.MyModel as MultiMazeModel;
            tempModel.OpponentMoved += delegate (Object sender, Key e)
            {
                OpponentBoard.MovePlayer(e);
            };
             myVM.VM_Maze = myVM.MyModel.MazeString();


            
        }



        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard();
            OpponentBoard.DrawBoard();

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
    }
}

