using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
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
            
            if (x < 0 || x > 9 || y < 0 || y > 8)
            {
                return false;
            }

            //判断当前玩家是红方还是黑方
            if (this.player == "red")
            {
                //判断终点是否在米字格里
                if (x <= 2 && x >= 0 && y <= 5 && y >= 3)
                {
                    //判断是否对角线移动
                    if ((x - intX == 1 || x - intX == -1 ) && (y - intY == 1 || y - intY == -1))
                    {
                        //判断目标位置是否有子
                        if (board[x, y] != null)
                        {
                            //若有子，则判断目标位置的棋子是否为己方
                            if (board[x, y].getPlayer() == this.player)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            else if (this.player == "black")
            {
                //判断终点是否在米字格里
                if (x <= 9 && x >= 7 && y <= 5 && y >= 3)
                {
                    if ((x - intX == 1 || x - intX == -1) && (y - intY == 1 || y - intY == -1))
                    {
                        //判断目标位置是否有子
                        if (board[x, y] != null)
                        {
                            //若有子，则判断目标位置的棋子是否为己方
                            if (board[x, y].getPlayer() == this.player)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
