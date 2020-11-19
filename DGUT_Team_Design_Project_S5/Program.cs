using System;

namespace DGUT_Team_Software_Project_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDisplay Displayer = new GameDisplay();
            GameBoard board = new GameBoard();
            while (board.getGameStatus())
            {
                Displayer.DisplayBoard(board);
                if(board.ifDeliveredCheck())
                    Displayer.Delivered();
                Displayer.AskSelectPiece();
                while (!board.boolSelectPiece(Console.ReadLine()))
                    Displayer.ErrorInput();
                Displayer.DisplayBoard(board);
                Displayer.AskMovePiece();
                while (!board.boolMovePiece(Console.ReadLine()))
                    Displayer.ErrorInput();
                if (!board.getGameStatus())
                {
                    Displayer.Congratulation();
                }
                board.SwitchPlayer();
            }

        }
    }
}
