using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GameDisplay
    {
        public void DisplayBoard(GameBoard board)
        {
            Console.Clear();
            int selectedX = board.getSelectedX();
            int selectedY = board.getSelectedY();
            Console.BackgroundColor =  ConsoleColor.DarkYellow;
            const string BoardLayout =
                "┏━┳━┳━┳━┳━┳━┳━┳━┓" +
                "┃ ┃ ┃ ┃╲┃╱┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃╱┃╲┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃" +
                "┣━┻━┻━┻━┻━┻━┻━┻━┫" +
                "┃               ┃" +
                "┣━┳━┳━┳━┳━┳━┳━┳━┫" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃╲┃╱┃ ┃ ┃ ┃" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫" +
                "┃ ┃ ┃ ┃╱┃╲┃ ┃ ┃ ┃" +
                "┗━┻━┻━┻━┻━┻━┻━┻━┛";
            for(int i = 0; i < 19; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                if(i%2 == 0)
                {
                    Console.Write(i/2 + " ");
                }
                else
                {
                    Console.Write("  ");
                }

                for (int j = 0; j < 17; j++)
                {
                    if(i % 2 == 0 && j % 2 == 0 && board.getPieceName(i/2, j / 2) != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        if(i/2==selectedX &&j/2 == selectedY)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        if(board.getPiecePlayer(i/2,j/2) == "red")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        Console.Write(board.getPieceName(i / 2, j / 2));
                        if (i / 2 == selectedX && j / 2 == selectedY)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(BoardLayout[i * 17 + j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h i");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Currrent Player: ");
            if (board.getPlayer() == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Red");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Black");
            }
        }

        public void AskSelectPiece()
        {
            Console.WriteLine("Which piece do you want to move?");
        }

        public void AskMovePiece()
        {
            Console.WriteLine("Which piece do you want to move to?");
        }

        public void ErrorInput()
        {
            Console.WriteLine("Error Input! Please check it and try again!");
        }

        public void Delivered()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("WARNING:Delivered a check!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Congratulation()
        {
            Console.WriteLine("Congratulations! You win this game!");
        }
    }
}
