using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace DGUT_Team_Software_Project_WPF
{
    class AIProgram : Program
    {
        Process elephanteye = new Process();

        public AIProgram() : base()
        {
            elephanteye.StartInfo.FileName = "C:\\Users\\herry\\Source\\Repos\\DGUT_Team_Design_Project_S5\\DGUT_Team_Design_Project_S5\\DGUT_Team_Software_Project_WPF\\src\\elephanteye\\ELEEYE.EXE";
            elephanteye.StartInfo.UseShellExecute = false;
            elephanteye.StartInfo.RedirectStandardInput = true;
            elephanteye.StartInfo.RedirectStandardOutput = true;
            elephanteye.StartInfo.CreateNoWindow = true;
        }
        public override bool undoBoard()
        {
            if (boardHistory.Count < 3)
            {
                return false;
            }
            boardHistory.RemoveAt(boardHistory.Count - 1);
            boardHistory.RemoveAt(boardHistory.Count - 1);
            setBoard(boardHistory[boardHistory.Count - 1]);
            return true;
        }
        string intArrtoStr(int column, int row)//Analog keyboard input
        {
            string returnStr = "";
            char alphabet = (char)(column + 97);
            returnStr = alphabet.ToString() + row.ToString();
            return returnStr;
        }

        public void aicalculate()
        {
            elephanteye.Start();
            elephanteye.StandardInput.WriteLine("ucci\nsetoption batch true\nsetoption usemillisec true\nposition fen "+ 
                board.outputFENFile(board) + "\ngo time 1000\nquit");
            Debug.WriteLine(board.outputFENFile(board));
            elephanteye.WaitForExit();
            string[] getmove = elephanteye.StandardOutput.ReadToEnd().Split('\n');
            string bestmovestr = "";
            foreach (string command in getmove)
            {
                if(command.Contains("bestmove"))
                {
                    Debug.WriteLine("Find it"+ command);
                    bestmovestr = command;
                    bestmovestr = bestmovestr.Substring(9, 4);
                    break;
                }
            }
            Debug.WriteLine("NOW Suggest:"+bestmovestr);
        }

        public override bool pieceClick(int column, int row)
        {
            aicalculate();
            if (!board.getGameStatus())//If game over ignore anything
            {
                return false;
            }
            //In console, row: 1-9，column: a-i
            if (board.getSelectedX() == -1)//If not selected, select
            {
                board.boolSelectPiece(intArrtoStr(column, row));
            }
            else
            {
                if (board.boolMovePiece(intArrtoStr(column, row)))//If move success, change the player
                {
                    //Console.WriteLine(board.outputFENFile(board));
                    board.SwitchPlayer();
                    boardHistory.Add(board.toJson());

                }
            }
            return true;
        }
    }
}
