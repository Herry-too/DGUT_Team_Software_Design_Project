using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    abstract class Piece
    {
        int intX;
        int intY;
        string player;
        string Name;

        public Piece(string player)
        {
            this.player = player;
            this.Name = "NULL";
            this.intX = -1;
            this.intY = -1;
            //后续重写

        }
        public override string ToString()
        {
            string str= "Test String";
            return str;
        }

        public bool ValidMoves(int x,int y, GameBoard gameboard,String player)
        {
            if(player != this.player)
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
            //后面写具体的判断
            return false;
        }
    }
}
