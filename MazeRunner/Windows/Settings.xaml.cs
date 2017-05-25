using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace MazeRunner.Windows
{
    /// <summary>
    /// Settings window
    /// </summary>
    public partial class Settings : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When ok button is clicked, the relevant default maze properties are changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (txtServerIp.Text.Length > 0)
            {
                ConfigurationManager.AppSettings["ip"] = txtServerIp.Text;
            }
            if (txtPortNum.Text.Length > 0)
            {
                ConfigurationManager.AppSettings["port"] = txtPortNum.Text;
            }
            if (txtRows.Text.Length > 0)
            {
                ConfigurationManager.AppSettings["rows"] = txtRows.Text;
            }
            if (txtCols.Text.Length > 0)
            {
                ConfigurationManager.AppSettings["cols"] = txtCols.Text;
            }
            if (searchAlgorithm.Text.Length > 0)
            {
                ConfigurationManager.AppSettings["algorithm"] = searchAlgorithm.Text;
            }            
            this.Close();
        }

        /// <summary>
        /// When back button is clicked the window closes itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
