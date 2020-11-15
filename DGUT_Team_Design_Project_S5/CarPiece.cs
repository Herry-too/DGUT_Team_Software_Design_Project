using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class CarPiece:Piece
    {
        int intX;
        int intY;
        string player;
        string Name;
        public CarPiece(string player, int intX, int intY):base(player, intX, intY)
        {
            this.Name = "C";
        }

        public bool ValidMoves(int x, int y, GameBoard gameboard, String player)
        {
            if (player != this.player)
            {
                return false;
            }
            if (x < 0 || x > 9)
            {
                return false;
            }
            if (y < 0 || y > 8)
            {
                return false;
            }
            //后面写具体的判断

            return false;
        }
    }
}
