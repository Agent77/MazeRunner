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

        /// <summary>
        /// Dependency Property for Rows
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(GameInfoControl), new PropertyMetadata(0));


        /// <summary>
        /// Dependency Property for Cols
        /// </summary>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(GameInfoControl), new PropertyMetadata(0));

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
