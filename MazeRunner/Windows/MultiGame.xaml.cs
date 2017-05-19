﻿using MazeRunner.ViewModels;
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
        }
        public void SetVM(MazeViewModel m)
        {
            myVM = m as MultiViewModel;
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            Board.DrawBoard();
            OpponentBoard.DrawBoard();
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            switch (k)
            {
                case Key.Left:
                    myVM.MovePlayer("LEFT");
                    break;
                case Key.Right:
                    myVM.MovePlayer("RIGHT");
                    break;
                case Key.Up:
                    myVM.MovePlayer("UP");
                    break;
                case Key.Down:
                    myVM.MovePlayer("DOWN");
                    break;
            }
        }
    }
}

