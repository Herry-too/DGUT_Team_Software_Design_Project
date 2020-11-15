using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GameBoard
    {
        String player = "red";
        Piece[,] pieces;

        public GameBoard()
        {
             pieces = new Piece[10, 9];
        }

        public Piece[,] returnpieces()
        {
            return pieces;
        }

        public string getPieceName(int x,int y)
        {
            if(pieces[x,y] == null)
            {
                return " ";
            }
            else
            {
                return pieces[x, y].name;
            }
        }

        public void SwitchPlayer()
        {
            if(player == "red")
            {
                player = "black";
            }
            else
            {
                player = "red";
            }
        }

        public bool SelectPiece()
        {
            return true;
        }

        public bool MovePiece()
        {
            return false;
        }

        public void CalculateValidMoves()
        {

        }
    }
}
