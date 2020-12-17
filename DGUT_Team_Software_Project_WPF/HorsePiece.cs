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
                this.Words = "n";
            }
            if (player == Players.black)
            {
                this.Name = "馬";
                this.Words = "N";
            }
            //Horse - 马
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            //to right
            if (newPositionY == currentPositionY + 2 && (newPositionX == currentPositionX + 1 || newPositionX == currentPositionX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[currentPositionX, currentPositionY + 1] == null)
                    return true;
            }

            //to left
            if (newPositionY == currentPositionY - 2 && (newPositionX == currentPositionX + 1 || newPositionX == currentPositionX - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[currentPositionX, currentPositionY - 1] == null)
                    return true;
            }

            //to up
            if (newPositionX == currentPositionX - 2 && (newPositionY == currentPositionY + 1 || newPositionY == currentPositionY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[currentPositionX - 1, currentPositionY] == null)
                    return true;
            }

            //to down
            if (newPositionX == currentPositionX + 2 && (newPositionY == currentPositionY + 1 || newPositionY == currentPositionY - 1))
            {
                //whether stuck by other pieces ?
                if (gameboard.getPieces()[currentPositionX + 1, currentPositionY] == null)
                    return true;
            }
            return false;
        }
    }
}
