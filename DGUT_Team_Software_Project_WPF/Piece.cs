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
        public int currentPositionX { get; set; }
        public int currentPositionY { get; set; }
        public string Name { get; set; }
        public Players player { get; set; }
        protected string Words;
        public Piece(Players player, int currentPositionX, int currentPositionY)
        {
            this.player = player;
            this.currentPositionX = currentPositionX;
            this.currentPositionY = currentPositionY;
        }

        public string GetPieceName()
        {
            return Words;
        }

        public (int, int) getCurrentPosition() { 
            return (currentPositionX, currentPositionY); 
        }
        public void setCurrentPosition(int newPositionX, int newPositionY) {
            currentPositionX = newPositionX;
            currentPositionY = newPositionY; 
        }

        public abstract bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard);
        
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
