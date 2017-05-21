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
    /// Interaction logic for FinishWindow.xaml
    /// </summary>
    public partial class FinishWindow : Window
    {
        public FinishWindow()
        {
            InitializeComponent();
            this.Loaded += win_loaded;
        }

        public SoundPlayer sPlayer = new SoundPlayer(MazeRunner.Properties.Resources.frozen);

        private void win_loaded(object sender, RoutedEventArgs e)
        {
            /*MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"\Sounds\frozen.mp3", UriKind.RelativeOrAbsolute));
            player.Play();*/

            
            sPlayer.Play();

            /*SoundPlayer player = new SoundPlayer(@"D:\Visual Projects\MazeRunner\MazeRunner\Sounds\frozen.wav");
            player.Play();*/
        }

    }
}
