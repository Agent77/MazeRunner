using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeRunner.Models
{
   public class MultiMazeModel : MazeModel
    {
        public void MovePlayer(string direction)
        {
            base.MovePlayer(direction);


            //TcpMessenger.Write();
            //Send and receive
        }
    }
       
}
