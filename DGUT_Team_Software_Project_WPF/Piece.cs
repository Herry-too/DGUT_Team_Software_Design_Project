using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    abstract class Piece
    {
        public enum Players
        {
            red,
            black
        }
        protected int intX;
        protected int intY;
        protected string player;
        protected string Name;
        protected Players player1;//替换为用这个

        public Piece(string player, int intX, int intY)
        {
            this.player = player;
            this.intX = intX;
            this.intY = intY;
        }

        public (int, int) getCurrentPosition() { 
            return (intX,intY); 
        }
        public void setCurrentPosition(int NewIntX, int NewIntY) {
            intX = NewIntX;
            intY = NewIntY; 
        }

        public abstract bool ValidMoves(int x, int y, GameBoard gameboard);
        
        public string getPieceWords()
        {
            return Name;
        }

        public string getPlayer()
        {
            return player;
        }
    }
}
