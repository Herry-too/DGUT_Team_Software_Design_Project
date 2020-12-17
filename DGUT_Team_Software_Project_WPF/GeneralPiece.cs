using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class GeneralPiece : Piece
    {
        
        public GeneralPiece(Players player, int currentPositionX, int currentPositionY) : base(player, currentPositionX, currentPositionY)
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

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            int intX = this.getCurrentPosition().Item1;
            int intY = this.getCurrentPosition().Item2;

            //judge the relative position between red and black
            if (gameboard.getPieces()[newPositionX, newPositionY] != null)
            {   //from red to black
                if (gameboard.getPieces()[newPositionX, newPositionY].getPieceWords() == "將" && gameboard.getPieces()[newPositionX, newPositionY].getPlayer() == Players.black
                    && newPositionX >= 7 && newPositionX <= 9 && newPositionY == intY)
                {
                    for (int i = intX + 1; i < newPositionX; i++)
                        if (gameboard.getPieces()[i,newPositionY] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }//from black to red

                else if (gameboard.getPieces()[newPositionX, newPositionY].getPieceWords() == "帥" && gameboard.getPieces()[newPositionX, newPositionY].getPlayer() == Players.red
                    && newPositionX >= 0 && newPositionX <= 2 && newPositionY == intY)
                {
                    for (int i = intX - 1; i > newPositionX; i--)
                        if (gameboard.getPieces()[i,newPositionY] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }                                   
            }

            if (player == Players.red)//Judge the player is  red or black
            {               
                if (newPositionX < 0 || newPositionX > 2)
                    return false;
            }

            else
            {
                if (newPositionX < 7 || newPositionX > 9)
                    return false;
            }

            if (newPositionY < 3 || newPositionY > 5)
            {
                return false;
            }

            //Judge the moving less than one 
            if ((Math.Abs(intX - newPositionX) + Math.Abs(intY - newPositionY)) <= 1)
            {
                //go right
                if (gameboard.getPieces()[newPositionX, newPositionY] != null)// Judge the next position doesn't have piece  
                {
                    if (gameboard.getPieces()[newPositionX, newPositionY].getPlayer() == this.player)// Judge the player is  red or black
                        return false;
                }
                return true;
            }

            else return false;
        }
    }
}