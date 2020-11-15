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
            //Red
            pieces[0, 0] = new CarPiece("red", 0, 0);
            pieces[0, 1] = new HorsePiece("red", 0, 1);
            pieces[0, 2] = new ElephantPiece("red", 0, 2);
            pieces[0, 3] = new AdvisorPiece("red", 0, 3);
            pieces[0, 4] = new GeneralPiece("red", 0, 4);
            pieces[0, 5] = new AdvisorPiece("red", 0, 5);
            pieces[0, 6] = new ElephantPiece("red", 0, 6);
            pieces[0, 7] = new HorsePiece("red", 0, 7);
            pieces[0, 8] = new CarPiece("red", 0, 8);
            pieces[2, 1] = new CannonPiece("red", 2, 1);
            pieces[2, 7] = new CannonPiece("red", 2, 7);
            pieces[3, 0] = new SoldierPiece("red", 3, 0);
            pieces[3, 2] = new SoldierPiece("red", 3, 2);
            pieces[3, 4] = new SoldierPiece("red", 3, 4);
            pieces[3, 6] = new SoldierPiece("red", 3, 6);
            pieces[3, 8] = new SoldierPiece("red", 3, 8);
            //Black
            pieces[9, 0] = new CarPiece("black", 9, 0);
            pieces[9, 1] = new HorsePiece("black", 9, 1);
            pieces[9, 2] = new ElephantPiece("black", 9, 2);
            pieces[9, 3] = new AdvisorPiece("black", 9, 3);
            pieces[9, 4] = new GeneralPiece("black", 9, 4);
            pieces[9, 5] = new AdvisorPiece("black", 9, 5);
            pieces[9, 6] = new ElephantPiece("black", 9, 6);
            pieces[9, 7] = new HorsePiece("black", 9, 7);
            pieces[9, 8] = new CarPiece("black", 9, 8);
            pieces[7, 1] = new CannonPiece("black", 7, 1);
            pieces[7, 7] = new CannonPiece("black", 7, 7);
            pieces[6, 0] = new SoldierPiece("black", 6, 0);
            pieces[6, 2] = new SoldierPiece("black", 6, 2);
            pieces[6, 4] = new SoldierPiece("black", 6, 4);
            pieces[6, 6] = new SoldierPiece("black", 6, 6);
            pieces[6, 8] = new SoldierPiece("black", 6, 8);
        }

        public Piece[,] getPieces()
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
                return pieces[x, y].GetPieceWords();
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
