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

        public MultiMazeModel() : base()
        {
           
        }


        public void MovePlayer(string direction)
        {
            base.MovePlayer(direction);
            string s = "play ";
            s += direction;
            TcpMessenger.Write(s);

            new Task(() =>
            {
                while (true)
                {
                    string pos = TcpMessenger.read();
                    Position currPos = OppPos;

                    if (pos.Contains("Left"))
                    {
                        currPos.Col = PlayerLocation.Col - 1;
                        OppPos = currPos;
                    }
                    else if (pos.Contains("Right"))
                    {
                        currPos.Col = PlayerLocation.Col + 1;
                        OppPos = currPos;
                    }
                    else if (pos.Contains("Up"))
                    {
                        currPos.Row = PlayerLocation.Row - 1;
                        OppPos = currPos;
                    }
                    else if (pos.Contains("Down"))
                    {
                        currPos.Row = PlayerLocation.Row + 1;
                        OppPos = currPos;
                    }
                }

            }).Start();
        }

        public void Join()
        {
            string s = "join ";
            s += Name;
            TcpMessenger.Write(s);


        }

        private Position oppPos;
        public Position OppPos
        {
            get
            {
                return oppPos;
            }
            set
            {
                oppPos = value;
                NotifyPropertyChanged("OppPos");
            }
        }

    }
       
}
