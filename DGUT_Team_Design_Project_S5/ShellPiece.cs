using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class ShellPiece:Piece
    {
        int intX;
        int intY;
        string player;
        string Name;
        public ShellPiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "S";
        }
        public override bool ValidMoves(int x, int y, GameBoard gameboard, string player)
        {
            
            //to count how many pieces on the way it move forward
            int count = -1;

            //move horizontally
            if (row == x && col != y)
            {
                if (col > y)
                {
                    //to right
                    count = 0;
                    for (int i = y + 1; i < col; i++)
                        if (GameDisplay.PieceArray[x, i] != null)
                            count++;
                }
                else
                {
                    //to left
                    count = 0;
                    for (int i = y - 1; i > col; i--)
                    {
                        if (GameDisplay.PieceArray[x, i] != null)
                            count++;
                    }
                }
            }
            //move verically
            if (col == y && row != x)
            {
                //up
                if (row > x)
                {
                    for (int i = x - 1; i > row; i--)
                        if (GameDisplay.PieceArray[i, y] != null)
                            count++;
                }
                //down
                else
                {
                    for (int i = x + 1; i < row; i++)
                        if (GameDisplay.PieceArray[i, y] != null)
                            count++;
                }
            }
            //move and eat the piece
            if (count == 1 && GameDisplay.PieceArray[row, col] != null)
                return true;
            //just move the shell
            if (count == 0 && GameDisplay.PieceArray[row, col] == null)
                return true;

            return false;
        }
    }
    }
}
