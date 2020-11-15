using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class XiangPiece : Piece
    {
        int intX;
        int intY;
        string player;
        string Name;
        public XiangPiece(string player, int intX, int intY): base (player,intX,intY)
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
            //判断是否符合符合运子规则
            if (x - intX == 2 || x - intX == -2)
            {
                if (y - intY == 2 || y - intY == -2)
                {
                    //判断“田”字路径中间有没有子
                    if (board[(x + intX) / 2, (y + intY) / 2] != null)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
