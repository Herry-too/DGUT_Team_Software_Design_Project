﻿using System;

namespace DGUT_Team_Software_Project_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GameDisplay Displayer = new GameDisplay();
            GameBoard board = new GameBoard();
            while (true)
            {
                Displayer.DisplayBoard(board);
                Displayer.AskSelectPiece();
                while (!board.SelectPiece(Console.ReadLine()))
                    Displayer.ErrorInput();
                Displayer.DisplayBoard(board);
                Displayer.AskMovePiece();
                while (!board.MovePiece(Console.ReadLine()))
                    Displayer.ErrorInput();
            }

        }
    }
}
