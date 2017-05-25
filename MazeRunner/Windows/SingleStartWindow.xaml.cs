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
    /// Interaction logic for SingleStartWindow.xaml
    /// </summary>
    public partial class SingleStartWindow : Window
    {
        private MazeViewModel myVM;
        private int x;
        public SingleStartWindow()
        {
            myVM = new SingleViewModel(new SingleMazeModel());
            this.DataContext = myVM;
            InitializeComponent();
            int success = myVM.MyModel.Connect();
            if (success < 0)
            {
                ConnectionFailedWindow c = new ConnectionFailedWindow();
                c.Show();
                //this.Close();
               // return;
            }
            if (success >= 0)
            {
                GameInfo.btnStart.Click += BtnStart_Click;
                this.Show();
            }
          
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            myVM.MyModel.SendMaze("generate");
            SingleGame sg = new SingleGame();
            sg.SetVM(myVM);
            sg.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
