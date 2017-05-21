using MazeLib;
using System;

namespace MazeRunner.Models
{
    public class PositionEventArgs: EventArgs
    {
        public Position playerLocation;
        public PositionEventArgs(Position p)
        {
            playerLocation = p;
        }
    }
}