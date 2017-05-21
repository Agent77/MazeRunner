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
    /// Interaction logic for GameInfoControl.xaml
    /// </summary>
    public partial class GameInfoControl : UserControl
    {
        public GameInfoControl()
        {
            InitializeComponent(); 
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
          
        }



        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(GameInfoControl), new PropertyMetadata(0));



        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(GameInfoControl), new PropertyMetadata(0));

        /*private string gameName;
        public string GameName
        {
            get { return (string)GetValue(GameNameProperty); }
            set { SetValue(GameNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameNameProperty =
            DependencyProperty.Register("GameName", typeof(string), typeof(MazeBoard), new PropertyMetadata(SetName));

        private static void SetName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard m = (MazeBoard)d;
            m.GameName = (string)e.NewValue;

        }*/


    }
}
