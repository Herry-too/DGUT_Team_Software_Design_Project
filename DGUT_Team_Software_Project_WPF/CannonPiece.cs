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
                this.Words = "c";
            }
            if (player == Players.black)
            {
                this.Name = "砲";
                this.Words = "C";
            }
            //Cannon - 炮
        }
        

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //to count how many pieces on the way it move forward
            int count = -1;

            //move horizontally
            if (newPositionX == CurrentX && newPositionY != CurrentY)
            {
                if (newPositionY > CurrentY)
                {
                    //to right
                    count = 0;
                    for (int i = CurrentY + 1; i < newPositionY; i++)
                        if (gameboard.getPieces()[newPositionX, i] != null)
                            count++;
                }
                if (newPositionY < CurrentY)
                {
                    //to left
                    count = 0;
                    for (int i = CurrentY - 1; i > newPositionY; i--)
                    {
                        if (gameboard.getPieces()[newPositionX, i] != null)
                            count++;
                    }
                }
            }
            //move vertically
            if (newPositionY == CurrentY && newPositionX != CurrentX)
            {
                //up
                if (newPositionX < CurrentX)
                {
                    count = 0;
                    for (int i = CurrentX - 1; i > newPositionX; i--)
                        if (gameboard.getPieces()[i, newPositionY] != null)
                            count++;
                }
                //down
                if (newPositionX > CurrentX)
                {
                    count = 0;
                    for (int i = CurrentX + 1; i < newPositionX; i++)
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
