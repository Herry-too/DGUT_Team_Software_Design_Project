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
                this.Words = "r";

            }
            if (player == Players.black)
            {
                this.Name = "車";
                this.Words = "R";
            }
            //Rook - 车
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            //make the rook move horizontally
            if (currentPositionX == newPositionX && currentPositionY != newPositionY)
            {
                //go right
                if (newPositionY > currentPositionY)
                {
                    for (int i = currentPositionY + 1; i < newPositionY; i++)
                        if (gameboard.getPieces()[newPositionX, i] != null)  //if there is a piece on the way of its moving forward, then return false 
                            return false;
                }
                else
                {
                    //go left
                    for (int i = currentPositionY - 1; i > newPositionY; i--)
                        if (gameboard.getPieces()[newPositionX, i] != null)  //the same
                            return false;
                }
                return true;
            }

            //move vertically
            if (newPositionX != currentPositionX && newPositionY == currentPositionY)
            {
                //go down
                if (newPositionX > currentPositionX)
                {
                    for (int i = currentPositionX + 1; i < newPositionX; i++)
                        if (gameboard.getPieces()[i, newPositionY] != null)  //the same
                            return false;
                }
                else
                {
                    //go up
                    for (int i = currentPositionX - 1; i > newPositionX; i--)
                        if (gameboard.getPieces()[i, newPositionY] != null)  //the same
                            return false;
                }
                return true;
            }
            return false;
        }
    }
}
