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

namespace MazeRunner
{
    /// <summary>
    /// This is the main window of the game
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When clicking on single option a single window is created and main window closes itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleClick(object sender, RoutedEventArgs e)
        {
            SingleStartWindow n = new SingleStartWindow();
            n.Show();
            this.Close();

        }

        /// <summary>
        /// When clicking on multi option a multi window is created and main window closes itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiClick(object sender, RoutedEventArgs e)
        {
            MultiStartWindow mw = new MultiStartWindow();
            mw.Show();
            this.Close();

        }

        /// <summary>
        /// When clicking on settings option a settings window is created
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }
    }
}
