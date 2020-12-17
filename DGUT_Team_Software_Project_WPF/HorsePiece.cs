using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class HorsePiece:Piece
    {
        
        public HorsePiece(Players player, int currentPositionX, int currentPositionY) : base(player, currentPositionX, currentPositionY)
        {
            if (player == Players.red)
            {
                this.Name = "傌";
                this.Words = "N";
            }
            if (player == Players.black)
            {
                this.Name = "馬";
                this.Words = "n";
            }
            //Horse - 马
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //to right
            if (newPositionY == CurrentY + 2 && (newPositionX == CurrentX + 1 || newPositionX == CurrentX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX, CurrentY + 1] == null)
                    return true;
            }

            //to left
            if (newPositionY == CurrentY - 2 && (newPositionX == CurrentX + 1 || newPositionX == CurrentX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX, CurrentY - 1] == null)
                    return true;
            }

            //to up
            if (newPositionX == CurrentX - 2 && (newPositionY == CurrentY + 1 || newPositionY == CurrentY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX - 1, CurrentY] == null)
                    return true;
            }

            //to down
            if (newPositionX == CurrentX + 2 && (newPositionY == CurrentY + 1 || newPositionY == CurrentY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX + 1, CurrentY] == null)
                    return true;
            }
            return false;
        }
    }
}
