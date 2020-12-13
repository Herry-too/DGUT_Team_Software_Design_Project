using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class SoldierPiece : Piece
    {
        public SoldierPiece(Players player, int intX, int intY) : base(player, intX, intY)
        {
            if (player == Players.red) this.Name = "兵";
            if (player == Players.black) this.Name = "卒";
            //Soldier - 兵
        }

        public override bool ValidMoves(int x, int y, GameBoard gameboard)
        {
            int CurrentX = this.getCurrentPosition().Item1;
            int CurrentY = this.getCurrentPosition().Item2;

            // red is above, black is below
            // red side
            if (player == Players.red)
            {
                //it hasn't passed the river 
                if (CurrentX <= 4)
                {
                    //down
                    if (x == CurrentX + 1 && y == CurrentY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //down
                    if (x == CurrentX + 1 && y == CurrentY)
                        return true;
                    //left or right
                    if ((y == CurrentY - 1 && x == CurrentX) || (x == CurrentX && y == CurrentY + 1))
                        return true;
                }
            }
            //black side
            else
            {
                //it hasn't passed the river 
                if (CurrentX >= 5)
                {
                    //up
                    if (x == CurrentX - 1 && y == CurrentY)
                        return true;
                }
                //it has passed the river
                else
                {
                    //up
                    if (x == CurrentX - 1 && y == CurrentY)
                        return true;
                    //left or right
                    if ((y == CurrentY - 1 && x == CurrentX) || (x == CurrentX && y == CurrentY + 1))
                        return true;
                }
            }
            return false;
        }
    }
}
