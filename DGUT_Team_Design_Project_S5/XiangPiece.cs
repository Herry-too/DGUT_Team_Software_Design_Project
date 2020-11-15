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
            int[,] board = gameboard.board;
            if (player != this.player)
            {
                return false;
            }
            if (x < 0 || x > 9)
            {
                return false;
            }
            if (y < 0 || y > 8)
            {
                return false;
            }
            if (x-intX == 2 || x - intX == -2)
            {
                if (y - intY == 2 || y - intY == -2)
                {
                    if(board[(x+intX)/2,(y+intY)/2]==0)//判断“田”字路径中间有没有子
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
