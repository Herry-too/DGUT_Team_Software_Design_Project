using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class HorcePiece:Piece
    {
        public HorcePiece(string player, int CurrentX, int CurrentY) : base(player, CurrentX, CurrentY)
        {
            this.Name = "H";
        }
        public override bool ValidMoves(int x, int y, GameBoard gameboard, string player)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

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
            if (y == CurrentY + 2 && (x == CurrentX + 1 || x == CurrentX - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[CurrentX, CurrentY + 1] == null)
                    return true;
            }

            //to left
            if (y == CurrentY - 2 && (x == CurrentX + 1 || x == CurrentX - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[CurrentX, CurrentY - 1] == null)
                    return true;
            }

            //to up
            if (x == CurrentX - 2 && (y == CurrentY + 1 || y == CurrentY - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[CurrentX - 1, CurrentY] == null)
                    return true;
            }

            //to down
            if (x == CurrentX + 2 && (y == CurrentY + 1 || y == CurrentY - 1))
            {
                //whether stuck?
                if (gameboard.returnpieces()[CurrentX + 1, CurrentY] == null)
                    return true;
            }
            return false;
        }
    }
}
