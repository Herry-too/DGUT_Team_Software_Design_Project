using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GameBoard
    {
        String player = "red";
        public GameBoard()
        {

        }

        public void SwitchPlayer()
        {
            if(player == "red")
            {
                player = "black";
            }
            else
            {
                player = "red";
            }
        }

        public bool SelectPiece()
        {
            return true;
        }

        public bool MovePiece()
        {
            return false;
        }

        public void CalculateValidMoves()
        {

        }
    }
}
