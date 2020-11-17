using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class ElephantPiece : Piece
    {
        public ElephantPiece(string player, int intX, int intY): base (player,intX,intY)
        {
            this.Name = "E";
            //Elephant - 象
        }


        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            Piece[,] board = gameboard.getPieces();

            //判断在红方还是黑方
            if (this.player == "red")
            {
                //判断是否符合符合运子规则
                if (x >= 0 && x <= 4 && y >= 0 && y <= 8)
                {
                    if (x - intX == 2 || x - intX == -2)
                    {
                        if (y - intY == 2 || y - intY == -2)
                        {
                            //判断“田”字路径中间有没有子
                            if (board[(x + intX) / 2, (y + intY) / 2] == null)
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
                }
            }
            else if (this.player == "black")
            {
                if (x >= 5 && x <= 9 && y >= 0 && y <= 8)
                {
                    if (x - intX == 2 || x - intX == -2)
                    {
                        if (y - intY == 2 || y - intY == -2)
                        {
                            //判断“田”字路径中间有没有子
                            if (board[(x + intX) / 2, (y + intY) / 2] == null)
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
                }
            }
            return false;
        }
    }
}
