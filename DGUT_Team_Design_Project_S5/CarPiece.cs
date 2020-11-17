using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class CarPiece:Piece
    {
        public CarPiece(string player, int intX, int intY):base(player, intX, intY)
        {
            this.Name = "R";
            //Rook - 车
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
            if (CurrentX == x && CurrentY != y)
            {
                //go right
                if (y > CurrentY)
                {
                    for (int i = CurrentY + 1; i < y; i++)
                        if (gameboard.getPieces()[x, i] != null)
                            return false;
                }
                else
                {
                    //go left
                    for (int i = CurrentY - 1; i > y; i--)
                        if (gameboard.getPieces()[x, i] != null)
                            return false;
                }
                return true;
            }

            //move vertically
            if (x != CurrentX && y == CurrentY)
            {
                //go down
                if (x > CurrentX)
                {
                    for (int i = CurrentX + 1; i < x; i++)
                        if (gameboard.getPieces()[i, y] != null)
                            return false;
                }
                else
                {
                    //go up
                    for (int i = CurrentX - 1; i > x; i--)
                        if (gameboard.getPieces()[i, y] != null)
                            return false;
                }
                return true;
            }
            return false;
        }
    }
}
