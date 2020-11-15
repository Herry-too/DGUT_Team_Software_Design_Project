using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class CannonPiece:Piece
    {
        public CannonPiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "C";
            //Cannon - 炮
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
            //to count how many pieces on the way it move forward
            int count = -1;

            //move horizontally
            if (x == CurrentX && y != CurrentY)
            {
                if (y > CurrentY)
                {
                    //to right
                    count = 0;
                    for (int i = CurrentY + 1; i < y; i++)
                        if (gameboard.returnpieces()[x, i] != null)
                            count++;
                }
                else
                {
                    //to left
                    count = 0;
                    for (int i = CurrentY - 1; i > y; i--)
                    {
                        if (gameboard.returnpieces()[x, i] != null)
                            count++;
                    }
                }
            }
            //move verically
            if (y == CurrentY && x != CurrentX)
            {
                //up
                if (x > CurrentX)
                {
                    for (int i = CurrentX - 1; i > x; i--)
                        if (gameboard.returnpieces()[i, y] != null)
                            count++;
                }
                //down
                else
                {
                    for (int i = CurrentX + 1; i < x; i++)
                        if (gameboard.returnpieces()[i, y] != null)
                            count++;
                }
            }
            //move and eat the piece
            if (count == 1 && gameboard.returnpieces()[x, y] != null)
                return true;
            //just move the shell
            if (count == 0 && gameboard.returnpieces()[x, y] == null)
                return true;

            return false;
        }
    }
}
