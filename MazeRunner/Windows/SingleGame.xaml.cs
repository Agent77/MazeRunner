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
            Board.DrawBoard();

        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            switch (k)
            {

                case Key.Left:
                    // myVM.VM_PlayerLocation = 
                    break;



            }
        }
    }
}
