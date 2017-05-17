using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeRunner.Models
{
    public class SingleMazeModel : MazeModel
    {

        public SingleMazeModel(): base()
        {
            //Cols = 0;
            //Rows = 0;
        }

        public void MovePlayer(string direction)
        {
            Position currPos = PlayerLocation;
            switch (direction)
            {
                case "UP":
                    currPos.Row = PlayerLocation.Row - 1;
                    PlayerLocation = currPos;
                    break;
                case "DOWN":
                    currPos.Row = PlayerLocation.Row + 1;
                    PlayerLocation = currPos;
                    break;
                case "LEFT":
                    currPos.Col = PlayerLocation.Col - 1;
                    PlayerLocation = currPos;
                    break;
                case "RIGHT":
                    currPos.Col = PlayerLocation.Col + 1;
                    PlayerLocation = currPos;
                    break;
            }
        }


    }
}