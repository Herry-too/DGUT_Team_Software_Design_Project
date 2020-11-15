using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class HorcePiece:Piece
    {
        int intX;
        int intY;
        string player;
        string Name;
        public HorcePiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "H";
        }
        public override bool ValidMoves(int x, int y, GameBoard gameboard, string player)
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

            //to right
            if (y == intY + 2 && (x == intX + 1 || x == intX - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[intX, intY + 1] == null)
                    return true;
            }

            //to left
            if (y == intY - 2 && (x == intX + 1 || x == intX - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[intX, intY - 1] == null)
                    return true;
            }

            //to up
            if (x == intX - 2 && (y == intY + 1 || y == intY - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[intX - 1, intY] == null)
                    return true;
            }

            //to down
            if (x == intX + 2 && (y == intY + 1 || y == intY - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[intX + 1, intY] == null)
                    return true;
            }
            return false;
        }
    }
}
