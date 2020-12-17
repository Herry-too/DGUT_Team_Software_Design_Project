using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class AIProgram : Program
    {
        public new bool undoBoard()
        {
            if (boardHistory.Count < 3)
            {
                return false;
            }
            boardHistory.RemoveAt(boardHistory.Count - 1);
            setBoard(boardHistory[boardHistory.Count - 2]);
            boardHistory.RemoveAt(boardHistory.Count - 1);
            return true;
        }
        public new bool pieceClick(int column, int row)
        {
            return false;
        }
    }
}
