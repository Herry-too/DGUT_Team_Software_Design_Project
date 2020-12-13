using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class AdvisorPiece : Piece
    {
        public AdvisorPiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "A";
            //Advisor - 士
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            Piece[,] board = gameboard.getPieces();
            int temp_x;

            //Determining whether the current player is red or black
            if (player == "red") temp_x = 0;
            else temp_x = 7;
            
            //Determine if the end point is in the palace
            if (x <= (temp_x+2) && x >= temp_x && y <= 5 && y >= 3)
            {
                //Determining compliance with the rules of moving pieces (diagonal)
                if ((x - intX == 1 || x - intX == -1 ) && (y - intY == 1 || y - intY == -1))
                {
                    //Determine if there are pawns in the target position
                    if (board[x, y] != null)
                    {
                        //Determine whether the piece in the target position is your own pawn
                        if (board[x, y].getPlayer() == this.player)
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
