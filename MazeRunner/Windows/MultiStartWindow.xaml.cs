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
    /// Interaction logic for MultiStartWindow.xaml
    /// </summary>
    public partial class MultiStartWindow : Window
    {
        private MultiViewModel myVM;
        public MultiStartWindow()
        {
            InitializeComponent();
            myVM = new MultiViewModel(new MultiMazeModel());
            DataContext = myVM;
        }

        private void JoinClicked(object sender, RoutedEventArgs e)
        {
            MultiGame mg = new MultiGame();
            mg.SetVM(myVM);
            mg.Show();
        }

       
    }
}
