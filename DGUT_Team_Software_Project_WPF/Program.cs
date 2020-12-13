using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DGUT_Team_Software_Project_WPF
{
    class Program
    {
        GameBoard board = new GameBoard();
        public Program()
        {
            //Just a bridge from console to WPF
        }

        public GameBoard GetBoard()
        {
            return board;//Return the board
        }

        string intArrtoStr(int column, int row)//Analog keyboard input
        {
            string returnStr = "";
            char alphabet = (char)(column + 97);
            returnStr = alphabet.ToString() + row.ToString();
            return returnStr;
        }
        public bool pieceClick(int column,int row)
        {
            if (!board.getGameStatus())//If game over ignore anything
            {
                return false;
            }
            //In console, row: 1-9，column: a-i
            if(board.getSelectedX() == -1)//If not selected, select
            {
                board.boolSelectPiece(intArrtoStr(column, row));
            }
            else
            {
                if(board.boolMovePiece(intArrtoStr(column, row)))//If move success, change the player
                {
                    board.SwitchPlayer();
                }
            }
            return true;
        }
    }
}
