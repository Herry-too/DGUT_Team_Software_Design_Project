﻿using System;
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


            if (player == "red")//判断颜色 红上黑下
            {
                if ( x < 0 || x > 2)
                {
                return false;
                }

            }
            else
            {
                if ( x < 7 || x > 9 )
                {
                    return false;
                }
            }

            if (y < 3 || y > 5)
            {
                return false;
            }

            //判断是否超过一步
            if(Math.Abs(CurrentX - x)>1 || Math.Abs(CurrentY - y) > 1)
            {
                return false;
            }


            //此处写判断帅将的移动位置
            //水平移动
                if (CurrentX == x && CurrentY != y)
            {
                //go right
                if (y > CurrentY)
                {
                    if (gameboard.getPieces()[x, CurrentY + 1] != null)
                        return false;
                }

                else
                {
                    //go left
                    if (gameboard.getPieces()[x, CurrentY - 1] != null)
                        return false;
                }

                return true;

            }

            //竖直移动
            if (x != CurrentX && y == CurrentY)
            {

                if (x > intX)
                {
                    //go down
                    if (gameboard.getPieces()[CurrentX + 1, y] != null)
                        return false;
                }
                else
                {
                    //go up
                    if (gameboard.getPieces()[CurrentX - 1, y] != null)
                        return false;
                }
                return true;
            }

            return false;
        }
    }
}
