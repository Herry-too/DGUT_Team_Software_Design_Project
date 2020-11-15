using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class AdvisorPiece : Piece
    {
        int intX;
        int intY;
        string player;
        string Name;
        public AdvisorPiece(string player, int intX, int intY) : base(player, intX, intY)
        {

        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard, String player)
        {
            Piece[,] board = gameboard.returnpieces();
            if (player != this.player)
            {
                return false;
            }
            if (x < 0 || x > 9 || y < 0 || y > 8)
            {
                return false;
            }

            //判断当前玩家是红方还是黑方
            if (this.player == "red")
            {
                //判断终点是否在米字格里
                if (x <= 5 && x >= 3 && y <= 2 && y >= 0)
                {
                    if (x - intX == 1 && x - intX == -1 && y - intY == 1 && y - intY == -1)
                    {
                        return true;
                    }
                }
            }
            else if (this.player == "black")
            {
                //判断终点是否在米字格里
                if (x <= 5 && x >= 3 && y <= 9 && y >= 7)
                {
                    if (x - intX == 1 && x - intX == -1 && y - intY == 1 && y - intY == -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
