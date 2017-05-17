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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeRunner.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        //private MazeViewModel myVM;

        public MazeBoard()
        {
            InitializeComponent();
        }


        public void UserControl_Loaded(object o, RoutedEventArgs e)
        {
            //loop
            int x = 0;
            int y = 0;
            int diff = 4;
            for (x = 0; x < Cols; x += 4)
            {
                for (y = 0; y < Rows; y += 4)
                {
                    Path p = new Path();
                    RectangleGeometry r = new RectangleGeometry(new Rect(x, y, 10, 10));
                    SolidColorBrush b = GetColour(x, y);
                    p.Fill = b;
                    p.Data = r;
                    Board.Children.Add(p);

                }
            }
        }


        private SolidColorBrush GetColour(int r, int c)
        {
            if (MazeString[r][c] == '0')
                return Brushes.Aqua;
            return Brushes.Black;

        }



        private string[] mstring;
        public string[] MazeString
        {
            get { return (string[])GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string[]), typeof(MazeBoard), new PropertyMetadata(SetString));

        private static void SetString(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard m = (MazeBoard)d;
            m.mstring = (string[])e.NewValue;
        }

        public void DrawBoard()
        {

            //Create maze or do it in UserControl_Loaded
        }
        public string PlayerPosition
        {
            get
            {
                return (string)GetValue(PlayerPositionProperty);
            }
            set
            {
                SetValue(PlayerPositionProperty, value);
            }
        }

        public static readonly DependencyProperty PlayerPositionProperty =
            DependencyProperty.Register("PlayerPosition", typeof(string), typeof(MazeBoard), new PropertyMetadata(ReDrawBoard));

        private static void ReDrawBoard(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Redraw board with new position of player
        }

        public int Rows {
            get
            {
                return (int)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
                
            }
        }

        public static readonly DependencyProperty RowsProperty = 
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata());

     
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));





    }
}
