using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GeneralPiece : Piece
    {
        public GeneralPiece(string player, int intX, int intY) : base(player, intX, intY)
        {
            this.Name = "G";
            //General - 將
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            //judge the relative position between red and black
            if (gameboard.getPieces()[x, y] != null)
            {   //from red to black
                if (gameboard.getPieces()[x, y].getPieceWords() == "G" && gameboard.getPieces()[x, y].getPlayer() == "black"
                    && x >= 7 && x <= 9 && y == CurrentY)
                {
                    for (int i = CurrentX + 1; i < x; i++)
                        if (gameboard.getPieces()[i,y] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }//from black to red
                else if (gameboard.getPieces()[x, y].getPieceWords() == "G" && gameboard.getPieces()[x, y].getPlayer() == "red"
                    && x >= 0 && x <= 2 && y == CurrentY)
                {
                    for (int i = CurrentX - 1; i > x; i--)
                        if (gameboard.getPieces()[i,y] != null)
                        {
                            return false;
                        }
                    return true; //if true the general could eat the opponent general piece directly
                }                                   
            }

            if (player == "red")//Judge the player is  red or black
            {               
                if (x < 0 || x > 2)
                {
                    return false;
                }

            }
            else
            {
                if (x < 7 || x > 9)
                {
                    return false;
                }
            }

            if (y < 3 || y > 5)
            {
                return false;
            }

            //Judge the moving more than one 
            if (Math.Abs(CurrentX - x) > 1 || Math.Abs(CurrentY - y) > 1)
            {
                return false;
            }


            //Judge the moving position of General 
            //Horizontal
            if (CurrentX == x && CurrentY != y)
            {
               
                if (y > CurrentY)
                { 
                    //go right
                    if(gameboard.getPieces()[x, CurrentY + 1] != null)// Judge the position doesn't have piece  
                        if (gameboard.getPieces()[x, CurrentY + 1].getPlayer() == this.player)// Judge the player is  red or black
                            return false;
                }

                else
                {
                    //go left
                    if(gameboard.getPieces()[x, CurrentY - 1] != null)
                        if (gameboard.getPieces()[x, CurrentY - 1].getPlayer() == this.player)
                            return false;
                }

                return true;

            }

            //Vertical
            if (x != CurrentX && y == CurrentY)
            {

                if (x > intX)
                {
                    //go down
                    if(gameboard.getPieces()[CurrentX + 1, y] != null)
                        if (gameboard.getPieces()[CurrentX + 1, y].getPlayer() == this.player)
                            return false;
                }
                else
                {
                    //go up
                    if (gameboard.getPieces()[CurrentX - 1, y] != null)
                        if (gameboard.getPieces()[CurrentX - 1, y].getPlayer() == this.player)
                            return false;
                }
                return true;
            }

            return false;
        }
    }
}