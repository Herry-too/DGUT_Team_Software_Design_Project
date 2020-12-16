using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace DGUT_Team_Software_Project_WPF
{
    class Program
    {
        protected GameBoard board = new GameBoard();
        protected List<string> boardHistory = new List<string>();
        public Program()
        {
            boardHistory.Add(board.toJson());
            //Just a bridge from console to WPF
        }

        public bool undoBoard()
        {
            if (boardHistory.Count < 2)
            {
                return false;
            }
            setBoard(boardHistory[boardHistory.Count - 2]);
            boardHistory.RemoveAt(boardHistory.Count - 1);
            return true;
        }

        public void setBoard(string str)
        {
            board = JsonConvert.DeserializeObject<GameBoard>(str, new JsonSerializerSettings
            {
               TypeNameHandling = TypeNameHandling.Auto
            });
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

            //MessageBox.Show(board.toJson());
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
                    boardHistory.Add(board.toJson());

                }
            }
            return true;
        }
    }
}
