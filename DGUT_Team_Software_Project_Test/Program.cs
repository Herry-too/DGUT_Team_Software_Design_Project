using System;

namespace DGUT_Team_Software_Project_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDisplay Displayer = new GameDisplay();
            GameBoard Board = new GameBoard();
            Displayer.DisplayBoard();
        }
    }
}
