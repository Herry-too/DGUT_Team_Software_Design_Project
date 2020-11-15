using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class PawnPiece : Piece
    {
        public PawnPiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "P";
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
            // red is above, black is below
            // red side
            if (player == "red")
            {
                //it hasn't passed the river 
                if (CurrentX <= 4)
                {
                    //down
                    if (x == CurrentX + 1 && y == CurrentY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //down
                    if (x == CurrentX + 1 && y == CurrentY)
                        return true;
                    //left or right
                    if ((y == CurrentY - 1 && x == CurrentX) || (x == CurrentX && y == CurrentY + 1))
                        return true;
                }
                return false;
            }
            //black side
            else
            {
                //it hasn't passed the river 
                if (CurrentX >= 5)
                {
                    //up
                    if (x == CurrentX - 1 && y == CurrentY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //up
                    if (x == CurrentX - 1 && y == CurrentY)
                        return true;
                    //left or right
                    if ((y == CurrentY - 1 && x == CurrentX) || (x == CurrentX && y == CurrentY + 1))
                        return true;
                }
            }
            return false;
        }
    }
}
