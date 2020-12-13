using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
