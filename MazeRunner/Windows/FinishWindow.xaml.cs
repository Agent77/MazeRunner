using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Popup window for winner in game
    /// </summary>
    public partial class FinishWindow : Window
    {
        public FinishWindow()
        {
            InitializeComponent();
            this.Loaded += win_loaded;
        }

        /// <summary>
        /// A theme song to play when window's showed
        /// </summary>
        public SoundPlayer sPlayer = new SoundPlayer(MazeRunner.Properties.Resources.frozen);

        private void win_loaded(object sender, RoutedEventArgs e)
        {        
            sPlayer.Play();
        }

    }
}
