using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class CannonPiece:Piece
    {
        
        public CannonPiece(Players player, int currentPositionX, int currentPositionY) : base(player, currentPositionX, currentPositionY)
        {          
            if (player == Players.red)
            {
                this.Name = "炮";
                this.Words = "C";
            }
            if (player == Players.black)
            {
                this.Name = "砲";
                this.Words = "c";
            }
            //Cannon - 炮
        }
        

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            //to count how many pieces on the way it move forward
            int count = -1;

            //move horizontally
            if (newPositionX == currentPositionX && newPositionY != currentPositionY)
            {
                if (newPositionY > currentPositionY)
                {
                    //to right
                    count = 0;
                    for (int i = currentPositionY + 1; i < newPositionY; i++)
                        if (gameboard.getPieces()[newPositionX, i] != null)
                            count++;
                }
                if (newPositionY < currentPositionY)
                {
                    //to left
                    count = 0;
                    for (int i = currentPositionY - 1; i > newPositionY; i--)
                    {
                        if (gameboard.getPieces()[newPositionX, i] != null)
                            count++;
                    }
                }
            }
            //move vertically
            if (newPositionY == currentPositionY && newPositionX != currentPositionX)
            {
                //up
                if (newPositionX < currentPositionX)
                {
                    count = 0;
                    for (int i = currentPositionX - 1; i > newPositionX; i--)
                        if (gameboard.getPieces()[i, newPositionY] != null)
                            count++;
                }
                //down
                if (newPositionX > currentPositionX)
                {
                    count = 0;
                    for (int i = currentPositionX + 1; i < newPositionX; i++)
                        if (gameboard.getPieces()[i, newPositionY] != null)
                            count++;
                }
            }
            //move and eat the piece
            if (count == 1 && gameboard.getPieces()[newPositionX, newPositionY] != null)  //if count == 1 it means that there is only one piece on the way it move  
                return true;                                        //forward and it must be the eaten piece
            //just move the Cannon
            if (count == 0 && gameboard.getPieces()[newPositionX, newPositionY] == null)  //if count == 0 it means that there is no piece on the way it move forward
                return true;                                        //and the destination must of no piece 

            return false;
        }
    }
}
