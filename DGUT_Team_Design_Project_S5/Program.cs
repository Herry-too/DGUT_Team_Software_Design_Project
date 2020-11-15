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
                Piece[,] test1 = Board.returnpieces();
                if(test1[0,0] == null)
                {
                    Console.WriteLine("NULL");
                }
                Console.WriteLine(Board.getPieceName(0, 0));
                Console.WriteLine(Board.getPieceName(1, 1));
                break;
            }

        }
    }
}
