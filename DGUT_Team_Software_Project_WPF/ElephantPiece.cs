using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class ElephantPiece : Piece
    {
        public ElephantPiece(Players player, int intX, int intY): base (player,intX,intY)
        {
            if (player == Players.red) this.Name = "相";
            if (player == Players.black) this.Name = "象";
            //Elephant - 象
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            Piece[,] board = gameboard.getPieces();
            int temp_x;

            //Determining whether the current player is red or black
            if (player == Players.red) temp_x = 0;
            else temp_x = 5;

            //Determining compliance with the rules of moving pieces(田）
            if (x >= temp_x && x <= (temp_x+4) && y >= 0 && y <= 8)
            {
                if (x - intX == 2 || x - intX == -2)
                {
                    if (y - intY == 2 || y - intY == -2)
                    {
                        //Determine if there are any pieces in the middle of "田"
                        if (board[(x + intX) / 2, (y + intY) / 2] == null)
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
                }
            }
            
            return false;
        }
    }
}
