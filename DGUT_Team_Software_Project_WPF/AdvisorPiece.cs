using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class AdvisorPiece : Piece
    {
        
        public AdvisorPiece(Players player, int currentPositionX, int currentPositionY) : base(player, currentPositionX, currentPositionY)
        {
            if (player == Players.red)
            {
                this.Name = "士";
                this.Words = "a";
            }
            if (player == Players.black)
            {
                this.Name = "仕";
                this.Words = "A";
            }
            //Advisor - 士
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            Piece[,] board = gameboard.getPieces();
            int temp_x;

            //Determining whether the current player is red or black
            if (player == Players.red) temp_x = 0;
            else temp_x = 7;
            
            //Determine if the end point is in the palace
            if (newPositionX <= (temp_x+2) && newPositionX >= temp_x && newPositionY <= 5 && newPositionY >= 3)
            {
                //Determining compliance with the rules of moving pieces (diagonal)
                if ((newPositionX - currentPositionX == 1 || newPositionX - currentPositionX == -1 ) && (newPositionY - currentPositionY == 1 || newPositionY - currentPositionY == -1))
                {
                    //Determine if there are pieces in the target position
                    if (board[newPositionX, newPositionY] != null)
                    {
                        //Determine whether the piece in the target position is your own piece
                        if (board[newPositionX, newPositionY].getPlayer() == this.player)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            
            return false;
        }
    }
}
