using MazeLib;
using MazeRunner.ViewModels;
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
using System.Windows.Threading;

namespace MazeRunner.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        //private MazeViewModel myVM;
        private Image goalImg;
        private Image wallImg;
        private Image player;
        public Image Player
        {
            get
            {
                return player;
            }
            set
            {
                player= value;
            }
        }
        private bool isOpponent;

        private bool finishedGame = false;
        private bool FinishedGame {
            get
            {
                return finishedGame;
            }
            set
            {
                finishedGame = value;
            }
        }

        public MazeBoard()
        {
            InitializeComponent();
        }


        public void UserControl_Loaded(object o, RoutedEventArgs e)
        {
            
        }


        private SolidColorBrush GetColour(int r, int c)
        {
            if (MazeString[r][c] == '0'|| MazeString[r][c] == '*')
                return Brushes.PaleTurquoise;
            /*if (MazeString[r][c] == '*')
                return Brushes.White;
            if (MazeString[r][c] == '#')
                return Brushes.White;*/
            return Brushes.White;
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

        public void DrawBoard(bool competitor)
        {

            //loop
            int x = 0;
            int y = 0;
            int rowsDiff = 300/Rows;
            int colsDiff = 300 / Cols;
            int xPlace = 0;
            int yPlace = 0;
            isOpponent = competitor;
            
            for (x = 0; xPlace < Cols; x += colsDiff )
            {
                for (y = 0; yPlace < Rows; y += rowsDiff)
                {
                    Path p = new Path();
                    RectangleGeometry r = new RectangleGeometry(new Rect(x,y,colsDiff, rowsDiff));
                    SolidColorBrush b = GetColour(yPlace, xPlace);
                    p.Fill = b;
                    p.Data = r;
                    //p.Stroke = Brushes.Green;
                    //p.StrokeThickness = 1;
                    Board.Children.Add(p);


                    if (MazeString[yPlace][xPlace] == '1')
                    {
                        wallImg = new Image();
                        wallImg.Width = colsDiff;
                        wallImg.Height = rowsDiff;
                        if (isOpponent)
                        {
                            wallImg.Source = new BitmapImage(new Uri(@"/Images/annawall.jpg", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            wallImg.Source = new BitmapImage(new Uri(@"/Images/elsawall.jpg", UriKind.RelativeOrAbsolute));
                        }
                        Canvas.SetLeft(wallImg, xPlace * colsDiff);
                        Canvas.SetTop(wallImg, yPlace * rowsDiff);
                        wallImg.Stretch = Stretch.Fill;
                        Board.Children.Add(wallImg);
                    }
                    yPlace++;

                }
                yPlace = 0;
                xPlace++;
            }
            
            goalImg = new Image();
            goalImg.Width = colsDiff;
            goalImg.Height = rowsDiff;
            goalImg.Source = new BitmapImage(new Uri(@"/Images/castle.jpg", UriKind.RelativeOrAbsolute));            
            Canvas.SetLeft(goalImg, GoalPos.Col * colsDiff);
            Canvas.SetTop(goalImg, GoalPos.Row * rowsDiff);
            goalImg.Stretch = Stretch.Fill;
            Board.Children.Add(goalImg);
            player = new Image();
            player.Width = colsDiff;
            player.Height = rowsDiff;
            if (isOpponent)
            {
                player.Source = new BitmapImage(new Uri(@"/Images/anna.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                player.Source = new BitmapImage(new Uri(@"/Images/elsa.png", UriKind.RelativeOrAbsolute));
            }
            PlayerPosition = InitialPos;
            Canvas.SetLeft(player, PlayerPosition.Col* colsDiff);
            Canvas.SetTop(player, PlayerPosition.Row*rowsDiff);
            player.Stretch = Stretch.Fill;
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


        private Position playerPosition;
        public Position PlayerPosition
        {
            get
            {
                return playerPosition;
            }
            set
            {
                playerPosition=value;
            }
        }

       /* public static readonly DependencyProperty PlayerPositionProperty =
            DependencyProperty.Register("PlayerPosition", typeof(Position), typeof(MazeBoard), new PropertyMetadata(MovePlayer));*/

        public int MovePlayer(Key k)
        {
            if (finishedGame) {
                return 1;
            }
            if(k == Key.Delete)
            {
                //LoserWindow lw = new LoserWindow();
                //lw.Show();
                return -1;
            }
            int rowsDiff = 300 / Rows;
            int colsDiff = 300 / Cols;
            Position current = PlayerPosition;
           
          //  Canvas.SetLeft(Brushes.White, PlayerPosition.Row);
            switch (k)
            {
                case Key.Left:
                    current.Col = playerPosition.Col - 1;
                    if (FinishedGame || !IsInBounds(current) || MazeString[playerPosition.Row][playerPosition.Col - 1] == '1')
                    {
                        break;
                    }
                    playerPosition.Col -= 1;
                    Canvas.SetLeft(Player, PlayerPosition.Col*colsDiff);
                    break;
                case Key.Right:
                    current.Col = playerPosition.Col + 1;
                    if (FinishedGame || !IsInBounds(current) || MazeString[playerPosition.Row][playerPosition.Col + 1] == '1')
                    {
                        break;
                    }
                    playerPosition.Col += 1;
                    Canvas.SetLeft(Player, PlayerPosition.Col* colsDiff);
                    break;
                case Key.Up:
                    current.Row = playerPosition.Row - 1;
                    if (FinishedGame || !IsInBounds(current) || MazeString[playerPosition.Row-1][playerPosition.Col] == '1')
                    {
                        break;
                    }
                    playerPosition.Row -= 1;
                    Canvas.SetTop(Player, PlayerPosition.Row*rowsDiff);
                    break;
                case Key.Down:
                    current.Row = playerPosition.Row + 1;
                    if (FinishedGame || !IsInBounds(current) || MazeString[playerPosition.Row+1][playerPosition.Col] == '1')
                    {
                        break;
                    }
                    playerPosition.Row += 1;
                    Canvas.SetTop(Player, PlayerPosition.Row*rowsDiff);
                    break;
            }
            if (MazeString[playerPosition.Row][playerPosition.Col] == '#' && isOpponent==false)
            {
                FinishedGame = true;
                FinishWindow fw = new FinishWindow();
                fw.Show();
                return 0;
            }
            if(MazeString[playerPosition.Row][playerPosition.Col] == '#' && isOpponent)
            {
               // Debug d = new Debug();
               // d.SetText("OPP arrived, returning 0");
               // d.Show();
                return 0;
            }
            return 1;
        }

        private bool IsInBounds(Position playerPosition)
        {
           if(playerPosition.Row>=0 && playerPosition.Row<=Rows-1 && playerPosition.Col>=0 && playerPosition.Col <= Cols - 1)
            {
                return true;
            }
            return false;
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
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata());

       
        public void RestartGame()
        {
            int rowsDiff = 300 / Rows;
            int colsDiff = 300 / Cols;
            PlayerPosition = InitialPos;
            Canvas.SetLeft(player, PlayerPosition.Col * colsDiff);
            Canvas.SetTop(player, PlayerPosition.Row * rowsDiff);
            if (finishedGame)
            {
                finishedGame = false;
            }
        }

        public void BackToMain()
        {
            
        }

        public void SolveMaze(string solution)
        {
            int rowsDiff = 300 / Rows;
            int colsDiff = 300 / Cols;
            playerPosition.Col = InitialPos.Col;
            playerPosition.Row = InitialPos.Row;
            Canvas.SetTop(player, playerPosition.Row * rowsDiff);
            Canvas.SetLeft(player, playerPosition.Col * colsDiff);
            CharEnumerator solEnum = solution.GetEnumerator();
            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += delegate (object s, EventArgs ev)
            {
                if (solEnum.MoveNext())
                {
                    if (solEnum.Current == '0')
                    {
                        MovePlayer(Key.Up);

                    }

                    if (solEnum.Current == '1')
                    {
                        MovePlayer(Key.Right);
                    }

                    if (solEnum.Current == '2')
                    {
                        MovePlayer(Key.Left);
                    }

                    if (solEnum.Current == '3')
                    {
                        MovePlayer(Key.Down);
                    }
                } else
                {
                    timer.Stop();
                }

            };
            

            timer.Interval =  TimeSpan.FromSeconds(0.1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
