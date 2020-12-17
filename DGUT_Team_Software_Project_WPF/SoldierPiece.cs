using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class SoldierPiece : Piece
    {
        
        public SoldierPiece(Players player, int currentPositionX, int currentPositionY) : base(player, currentPositionX, currentPositionY)
        {
            if (player == Players.red)
            {
                this.Name = "兵";
                this.Words = "p";
            }
            if (player == Players.black)
            {
                this.Name = "卒";
                this.Words = "P";
            }
            //Soldier - 兵
        }

        public override bool ValidMoves(int newPositionX, int newPositionY, GameBoard gameboard)
        {
            // red is above, black is below
            // red side
            if (player == Players.red)
            {
                //it hasn't passed the river 
                if (currentPositionX <= 4)
                {
                    //down
                    if (newPositionX == currentPositionX + 1 && newPositionY == currentPositionY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //down
                    if (newPositionX == currentPositionX + 1 && newPositionY == currentPositionY)
                        return true;
                    //left or right
                    if ((newPositionY == currentPositionY - 1 && newPositionX == currentPositionX) || (newPositionX == currentPositionX && newPositionY == currentPositionY + 1))
                        return true;
                }
            }
            //black side
            else
            {
                //it hasn't passed the river 
                if (currentPositionX >= 5)
                {
                    //up
                    if (newPositionX == currentPositionX - 1 && newPositionY == currentPositionY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //up
                    if (newPositionX == currentPositionX - 1 && newPositionY == currentPositionY)
                        return true;
                    //left or right
                    if ((newPositionY == currentPositionY - 1 && newPositionX == currentPositionX) || (newPositionX == currentPositionX && newPositionY == currentPositionY + 1))
                        return true;
                }
            }
            return false;
        }
    }
}
