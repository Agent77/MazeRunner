using MazeRunner.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeRunner.Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SingleClick(object sender, RoutedEventArgs e)
        {
            SingleStartWindow n = new SingleStartWindow();
            n.Show();
            
        }

        private void MultiClick(object sender, RoutedEventArgs e)
        {
            MultiStartWindow mw = new MultiStartWindow();
            mw.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
