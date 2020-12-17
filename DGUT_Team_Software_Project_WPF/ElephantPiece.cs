using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class ElephantPiece : Piece
    {
        
        public ElephantPiece(Players player, int currentPositionX, int currentPositionY): base (player,currentPositionX, currentPositionY)
        {
            if (player == Players.red)
            {
                this.Name = "相";
                this.Words = "E";
            }
            if (player == Players.black)
            {
                this.Name = "象";
                this.Words = "e";
            }
            //Elephant - 象
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            Piece[,] board = gameboard.getPieces();
            int temp_x;

            //Determining whether the current player is red or black
            if (player == Players.red) temp_x = 0;
            else temp_x = 5;

            //Determining compliance with the rules of moving pieces(田）
            if (newPositionX >= temp_x && newPositionX <= (temp_x+4) && newPositionY >= 0 && newPositionY <= 8)
            {
                if (newPositionX - currentPositionX == 2 || newPositionX - currentPositionX == -2)
                {
                    if (newPositionY - currentPositionY == 2 || newPositionY - currentPositionY == -2)
                    {
                        //Determine if there are any pieces in the middle of "田"
                        if (board[(newPositionX + currentPositionX) / 2, (newPositionY + currentPositionY) / 2] == null)
                        {
                            //Determine if there are pawns in the target position
                            if (board[newPositionX, newPositionY] != null)
                            {
                                //Determine whether the piece in the target position is your own pawn
                                if (board[newPositionX, newPositionY].getPlayer() == this.player)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            
            return false;
        }
    }
}
