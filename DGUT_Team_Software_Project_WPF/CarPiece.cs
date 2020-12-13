using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class CarPiece:Piece
    {
        public CarPiece(Players player, int intX, int intY):base(player, intX, intY)
        {
            if (player == Players.red) this.Name = "俥";
            if (player == Players.black) this.Name = "車";
            //Rook - 车
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //make the rook move horizontally
            if (CurrentX == x && CurrentY != y)
            {
                //go right
                if (y > CurrentY)
                {
                    for (int i = CurrentY + 1; i < y; i++)
                        if (gameboard.getPieces()[x, i] != null)  //if there is a piece on the way of its moving forward, then return false 
                            return false;
                }
                else
                {
                    //go left
                    for (int i = CurrentY - 1; i > y; i--)
                        if (gameboard.getPieces()[x, i] != null)  //the same
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
                        if (gameboard.getPieces()[i, y] != null)  //the same
                            return false;
                }
                else
                {
                    //go up
                    for (int i = CurrentX - 1; i > x; i--)
                        if (gameboard.getPieces()[i, y] != null)  //the same
                            return false;
                }
                return true;
            }
            return false;
        }
    }
}
