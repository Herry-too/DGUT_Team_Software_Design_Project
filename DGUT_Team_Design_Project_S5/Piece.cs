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

        

        public Piece(string player, int intX, int intY)
        {
            this.player = player;
            this.Name = "NULL";
            this.IntX = intX;
            this.intY = intY;
            //intX and intY are current position
            //后续重写

        }
        public override string ToString()
        {
            string str= "Test String";
            return str;
        }

        public abstract bool ValidMoves(int x, int y, GameBoard gameboard, String player);
        
        public string GetPieceWords()
        {
            string strPiece = "";
            switch (this.Name)
            {
                case "C":
                    strPiece = "C";
                    break;
                case "H":
                    strPiece = "H";
                    break;
                case "E":
                    strPiece = "E";
                    break;
                case "A":
                    strPiece = "A";
                    break;
                case "G":
                    strPiece = "G";
                    break;
                case "S":
                    strPiece = "S";
                    break;
                case "P":
                    strPiece = "P";
                    break;
                default:
                    break;
            }
            
            return strPiece;
        }
    }
}
