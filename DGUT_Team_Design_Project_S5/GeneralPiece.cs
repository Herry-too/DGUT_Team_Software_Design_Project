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
                //go right
                if (y > CurrentY)
                {
                    if(gameboard.getPieces()[x, CurrentY + 1] != null)
                    if (gameboard.getPieces()[x, CurrentY + 1].getPlayer() == this.player)
                        return false;
                }

                else
                {
                    //go left
                    if(gameboard.getPieces()[x, CurrentY + 1] != null)
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