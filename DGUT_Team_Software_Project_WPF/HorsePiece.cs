using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class HorsePiece:Piece
    {
        public HorsePiece(string player, int CurrentX, int CurrentY) : base(player, CurrentX, CurrentY)
        {
            this.Name = "H";
            //Horse - 马
        }
        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //to right
            if (y == CurrentY + 2 && (x == CurrentX + 1 || x == CurrentX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX, CurrentY + 1] == null)
                    return true;
            }

            //to left
            if (y == CurrentY - 2 && (x == CurrentX + 1 || x == CurrentX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX, CurrentY - 1] == null)
                    return true;
            }

            //to up
            if (x == CurrentX - 2 && (y == CurrentY + 1 || y == CurrentY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX - 1, CurrentY] == null)
                    return true;
            }

            //to down
            if (x == CurrentX + 2 && (y == CurrentY + 1 || y == CurrentY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[CurrentX + 1, CurrentY] == null)
                    return true;
            }
            return false;
        }
    }
}
