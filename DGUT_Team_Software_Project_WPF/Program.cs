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
        }

        public GameBoard GetBoard()
        {
            return board;
        }

        string intArrtoStr(int column, int row)
        {
            string returnStr = "";
            char alphabet = (char)(column + 97);
            returnStr = alphabet.ToString() + row.ToString();
            return returnStr;
        }
        public bool pieceClick(int column,int row)
        {
            if (!board.getGameStatus())
            {
                return false;
            }
            //原代码中数字是行row1-9，字母是列a-i
            if(board.getSelectedX() == -1)
            {
                board.boolSelectPiece(intArrtoStr(column, row));
            }
            else
            {
                if(board.boolMovePiece(intArrtoStr(column, row)))
                {
                    board.SwitchPlayer();
                }
            }
            return true;
        }
    }
}
