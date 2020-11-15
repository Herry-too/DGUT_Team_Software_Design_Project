using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    abstract class Piece
    {
        protected int intX;
        protected int intY;
        protected string player;
        protected string Name;

        

        public Piece(string player, int intX, int intY)
        {
            this.player = player;
            this.intX = intX;
            this.intY = intY;
            //intX and intY are current position
        }
        public (int, int) getCurrentPosition() { return (intX,intY); }
        public void setCurrentPosition(int NewIntX, int NewIntY) { intX = NewIntX; intY = NewIntY; }
        public override string ToString()
        {
            string str= "Test String";
            return str;
        }

        public abstract bool ValidMoves(int x, int y, GameBoard gameboard, String player);
        
        public string GetPieceWords()
        {
            return Name;
        }
    }
}
