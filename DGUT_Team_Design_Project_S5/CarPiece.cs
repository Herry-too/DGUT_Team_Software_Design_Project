using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class CarPiece:Piece
    {

        public CarPiece(string player, int intX, int intY):base(player, intX, intY)
        {
            this.Name = "C";
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard, String player)
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

            //后面写具体的判断
            //move horizontally
            if (intX == x && intY != y)
            {
                //go right
                if (y > intY)
                {
                    for (int i = intY + 1; i < y; i++)
                        if (gameboard.returnpieces()[x, i] != null)
                            return false;
                }
                else
                {
                    //go left
                    for (int i = intY - 1; i > y; i--)
                        if (gameboard.returnpieces()[x, i] != null)
                            return false;
                }
                return true;
            }

            //move vertically
            if (x != intX && y == intY)
            {
                //go down
                if (x > intX)
                {
                    for (int i = intX + 1; i < x; i++)
                        if (gameboard.returnpieces()[i, y] != null)
                            return false;
                }
                else
                {
                    //go up
                    for (int i = intX - 1; i > x; i--)
                        if (gameboard.returnpieces()[i, y] != null)
                            return false;
                }
                return true;
            }
            
            return false;
        }
    }
}
