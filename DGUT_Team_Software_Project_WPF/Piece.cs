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
        public int intX { get; set; }
        public int intY { get; set; }
        public string Name { get; set; }
        public Players player { get; set; }
        protected string Words;
        public Piece(Players player, int intX, int intY)
        {
            this.player = player;
            this.intX = intX;
            this.intY = intY;
        }

        public string GetPieceName()
        {
            return Words;
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


        public Players getPlayer()
        {
            return player;
        }
    }
}
