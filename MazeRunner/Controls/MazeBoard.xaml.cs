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
    /// MazeBoard user control, represents the maze board and in charge of its view logic
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        private Image goalImg; //Reperesents the goal cell on the board
        private Image wallImg; //Represents every wall cell
        private Image player; //Reperesents the player on the board
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

        private bool isOpponent; //A boolean member that is set to true when it's opponent's board

        private bool finishedGame = false; //A boolean member that is set to true when player at goal position
        public bool FinishedGame {
            get
            {
                return finishedGame;
            }
            set
            {
                finishedGame = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MazeBoard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to set the color on a given cell on the board
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns>Relavent color</returns>
        private SolidColorBrush GetColour(int r, int c)
        {
            if (MazeString[r][c] == '0'|| MazeString[r][c] == '*')
                return Brushes.PaleTurquoise;
            return Brushes.White;
        }


        /// <summary>
        /// Represents the string of the given maze from server
        /// </summary>
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

        /// <summary>
        /// Main method to draw the maze Board. 
        /// Gets false as parameter when its palyer's board and true otherwise
        /// </summary>
        /// <param name="competitor"></param>
        public void DrawBoard(bool competitor)
        {

            
            int x = 0;
            int y = 0;
            int rowsDiff = 300 / Rows;
            int colsDiff = 300 / Cols;
            int xPlace = 0;
            int yPlace = 0;
            isOpponent = competitor;
            
            // Loop for creating each cell
            for (x = 0; xPlace < Cols; x += colsDiff )
            {
                for (y = 0; yPlace < Rows; y += rowsDiff)
                {
                    Path p = new Path();
                    RectangleGeometry r = new RectangleGeometry(new Rect(x,y,colsDiff, rowsDiff));
                    SolidColorBrush b = GetColour(yPlace, xPlace);
                    p.Fill = b;
                    p.Data = r;
                    Board.Children.Add(p);

                    //When a cell is a wall it adds the relevant wall image
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
            
            //Adding goal cell image
            goalImg = new Image();
            goalImg.Width = colsDiff;
            goalImg.Height = rowsDiff;
            goalImg.Source = new BitmapImage(new Uri(@"/Images/castle.jpg", UriKind.RelativeOrAbsolute));            
            Canvas.SetLeft(goalImg, GoalPos.Col * colsDiff);
            Canvas.SetTop(goalImg, GoalPos.Row * rowsDiff);
            goalImg.Stretch = Stretch.Fill;
            Board.Children.Add(goalImg);

            //Adding relevant image for player
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

        
        /// <summary>
        /// Represents opponents position on its board
        /// </summary>
        public Position OppPos
        {
            get { return (Position)GetValue(OppPosProperty); }
            set { SetValue(OppPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OppPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OppPosProperty =
            DependencyProperty.Register("OppPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));


        /// <summary>
        /// Represents goal position on the board
        /// </summary>
        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));


        /// <summary>
        /// Represents initial position on the board
        /// </summary>
        public Position InitialPos
        {
            get { return (Position)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position()));

        /// <summary>
        /// Represents player's position on the board
        /// </summary>
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

        /// <summary>
        /// Main method for moving the player on the board
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Number to represent current move's result</returns>
        public int MovePlayer(Key k)
        {
            //Other player won
            if(k == Key.Delete)
            {
                return -1;
            }
            //Other player quit
            if(k == Key.Back)
            {
                return -2;
            }

            int rowsDiff = 300 / Rows;
            int colsDiff = 300 / Cols;
            Position current = PlayerPosition;
           
            //Moving player by pressed key
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
            //Return 3 when current player won 
            if (MazeString[playerPosition.Row][playerPosition.Col] == '#' && isOpponent==false)
            {
                FinishedGame = true;
                return 3;
               
            }
            //Return 2 when opponent player won
            if(MazeString[playerPosition.Row][playerPosition.Col] == '#' && isOpponent)
            {
                finishedGame = true;
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// Method that checks whether a player can move and stay in board's bounds
        /// </summary>
        /// <param name="playerPosition"></param>
        /// <returns>True when player can move and false otherwise</returns>
        private bool IsInBounds(Position playerPosition)
        {
           if(playerPosition.Row>=0 && playerPosition.Row<=Rows-1 && playerPosition.Col>=0 && playerPosition.Col <= Cols - 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Represents rows of board
        /// </summary>
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

        /// <summary>
        /// Represents columns of board 
        /// </summary>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata());

        /// <summary>
        /// Method that takes player back to initial position
        /// </summary>
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

        
        /// <summary>
        /// Method that gets a solution for the game and moves player from initial position to goal position
        /// </summary>
        /// <param name="solution"></param>
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
