using MazeLib;
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
        private Image player;
        public MazeBoard()
        {
            InitializeComponent();
        }


        public void UserControl_Loaded(object o, RoutedEventArgs e)
        {
            
        }


        private SolidColorBrush GetColour(int r, int c)
        {
            if (MazeString[r][c] == '0')
                return Brushes.White;
            if (MazeString[r][c] == '*')
                return Brushes.Green;
            if (MazeString[r][c] == '#')
                return Brushes.Yellow;
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

            //loop
            int x = 0;
            int y = 0;
            int diff = 300/Cols;
            int xPlace = 0;
            int yPlace = 0;
            for (x = 0; xPlace < Cols; x += diff )
            {
                for (y = 0; yPlace < Rows; y += diff)
                {
                    Path p = new Path();
                    RectangleGeometry r = new RectangleGeometry(new Rect(x, y, diff, diff));
                    SolidColorBrush b = GetColour(xPlace, yPlace);
                    p.Fill = b;
                    p.Data = r;
                    //p.Stroke = Brushes.Green;
                    //p.StrokeThickness = 1;
                    Board.Children.Add(p);
                    
                    yPlace++;

                }
                yPlace = 0;
                xPlace++;
            }
            player = new Image();
            player.Width = 10;
            player.Height = 10;
            player.Source = new BitmapImage(new Uri(@"/Images/elsa.jpg", UriKind.RelativeOrAbsolute));
            
            Canvas.SetLeft(player, InitialPos.Col);
            Canvas.SetTop(player, InitialPos.Row);
            Board.Children.Add(player);
        }



        public Position OppPos
        {
            get { return (Position)GetValue(OppPosProperty); }
            set { SetValue(OppPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OppPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OppPosProperty =
            DependencyProperty.Register("OppPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));



        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));



        public Position InitialPos
        {
            get { return (Position)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));



        public Position PlayerPosition
        {
            get
            {
                return (Position)GetValue(PlayerPositionProperty);
            }
            set
            {
                SetValue(PlayerPositionProperty, value);
            }
        }

        public static readonly DependencyProperty PlayerPositionProperty =
            DependencyProperty.Register("PlayerPosition", typeof(Position), typeof(MazeBoard), new PropertyMetadata(MovePlayer));

        private static void MovePlayer(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Position p = (Position)e.NewValue;
            MazeBoard m = (MazeBoard)d;
            Canvas.SetLeft(m.player, p.Col);
            Canvas.SetTop(m.player, p.Row);
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
