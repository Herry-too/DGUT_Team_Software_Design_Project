using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class CarPiece:Piece
    {
        
        public CarPiece(Players player, int currentPositionX, int currentPositionY):base(player, currentPositionX, currentPositionY)
        {
            if (player == Players.red)
            {
                this.Name = "俥";
                this.Words = "R";

            }
            if (player == Players.black)
            {
                this.Name = "車";
                this.Words = "r";
            }
            //Rook - 车
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //make the rook move horizontally
            if (CurrentX == newPositionX && CurrentY != newPositionY)
            {
                //go right
                if (newPositionY > CurrentY)
                {
                    for (int i = CurrentY + 1; i < newPositionY; i++)
                        if (gameboard.getPieces()[newPositionX, i] != null)  //if there is a piece on the way of its moving forward, then return false 
                            return false;
                }
                else
                {
                    //go left
                    for (int i = CurrentY - 1; i > newPositionY; i--)
                        if (gameboard.getPieces()[newPositionX, i] != null)  //the same
                            return false;
                }
                return true;
            }

            //move vertically
            if (newPositionX != CurrentX && newPositionY == CurrentY)
            {
                //go down
                if (newPositionX > CurrentX)
                {
                    for (int i = CurrentX + 1; i < newPositionX; i++)
                        if (gameboard.getPieces()[i, newPositionY] != null)  //the same
                            return false;
                }
                else
                {
                    //go up
                    for (int i = CurrentX - 1; i > newPositionX; i--)
                        if (gameboard.getPieces()[i, newPositionY] != null)  //the same
                            return false;
                }
                return true;
            }
            return false;
        }
    }
}
