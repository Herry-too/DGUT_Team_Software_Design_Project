using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class GeneralPiece : Piece
    {
        
        public GeneralPiece(Players player, int intX, int intY) : base(player, intX, intY)
        {
            if(player == Players.red)
            {
                this.Name = "帥";
                this.Words = "G";
            }
            if(player == Players.black)
            {
                this.Name = "將";
                this.Words = "g";
            }
            //General - 將
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            int intX = this.getCurrentPosition().Item1;
            int intY = this.getCurrentPosition().Item2;

            //judge the relative position between red and black
            if (gameboard.getPieces()[x, y] != null)
            {   //from red to black
                if (gameboard.getPieces()[x, y].getPieceWords() == "將" && gameboard.getPieces()[x, y].getPlayer() == Players.black
                    && x >= 7 && x <= 9 && y == intY)
                {
                    for (int i = intX + 1; i < x; i++)
                        if (gameboard.getPieces()[i,y] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }//from black to red

                else if (gameboard.getPieces()[x, y].getPieceWords() == "帥" && gameboard.getPieces()[x, y].getPlayer() == Players.red
                    && x >= 0 && x <= 2 && y == intY)
                {
                    for (int i = intX - 1; i > x; i--)
                        if (gameboard.getPieces()[i,y] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }                                   
            }

            if (player == Players.red)//Judge the player is  red or black
            {               
                if (x < 0 || x > 2)
                    return false;
            }

            else
            {
                if (x < 7 || x > 9)
                    return false;
            }

            if (y < 3 || y > 5)
            {
                return false;
            }

            //Judge the moving less than one 
            if ((Math.Abs(intX - x) + Math.Abs(intY - y)) <= 1)
            {
                //go right
                if (gameboard.getPieces()[x, y] != null)// Judge the next position doesn't have piece  
                {
                    if (gameboard.getPieces()[x, y].getPlayer() == this.player)// Judge the player is  red or black
                        return false;
                }
                return true;
            }

            else return false;
        }
    }
}