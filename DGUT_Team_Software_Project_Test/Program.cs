using System;

namespace DGUT_Team_Software_Project_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameDisplay Displayer = new GameDisplay();
                GameBoard Board = new GameBoard();
                Displayer.DisplayBoard();
            }

        }
    }
}
