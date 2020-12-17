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
            elephanteye.StartInfo.FileName = "C:\\Program Files (x86)\\XQWizard\\ELEEYE.EXE";
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
            //foreach (Piece piece in board.getPieces())
            //{
            //   if (piece == null)
            //        continue;
            //    if (piece.getPlayer() == Piece.Players.red)
            //        continue;
            //    if(piece.ValidMoves(board.redGeneralPiece[0], board.redGeneralPiece[1], board))
            //    {
            //        pieceClick(piece.currentPositionX, piece.currentPositionY);
            //        pieceClick(board.redGeneralPiece[0], board.redGeneralPiece[1]);
            //        return;
            //    }
            //}
            try
            {
                elephanteye.Start();
            } catch(Exception)
            {
                MessageBox.Show("NO AI Model!");
                Environment.Exit(1);
            }
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
            (int baseX, int baseY, int destX, int destY) = bestMoveStrIntoInt(bestmovestr);
            pieceClick(baseX,baseY);
            pieceClick(destX, destY);

        }

        public override bool pieceClick(int column, int row)
        {

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
            if (board.player == Piece.Players.black && board.selectedX == -1)
            {
                aicalculate();
            }
            return true;
        }
        //input example:a9a8
        public (int baseX,int baseY,int destX,int destY) bestMoveStrIntoInt(string fenstr)
        {
            int iniX, iniY, DesX, DesY;

            char[] X0 = new char[1];
            char[] X1 = new char[1];
            //b2e2
            X0 = fenstr.Substring(0, 1).ToCharArray();
            X1 = fenstr.Substring(2, 1).ToCharArray();
            iniY = 9 - int.Parse(fenstr.Substring(1, 1));
            iniX = (int)((int)X0[0] - 97);
            DesY = 9 - int.Parse(fenstr.Substring(3, 1));
            DesX = (int)((int)X1[0] - 97);

            return (iniX, iniY, DesX, DesY);
        }
    }
}
