using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Test
{
    class GameDisplay
    {
        public void DisplayBoard()
        {
            const string BoardLayout =
                "┏━┳━┳━┳━┳━┳━┳━┳━┓\n" +
                "┃ ┃ ┃ ┃╲┃╱┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃╱┃╲┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃\n" +
                "┣━┻━┻━┻━┻━┻━┻━┻━┫\n" +
                "┃               ┃\n" +
                "┣━┳━┳━┳━┳━┳━┳━┳━┫\n" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃╲┃╱┃ ┃ ┃ ┃\n" +
                "┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
                "┃ ┃ ┃ ┃╱┃╲┃ ┃ ┃ ┃\n" +
                "┗━┻━┻━┻━┻━┻━┻━┻━┛\n";
            Console.WriteLine(BoardLayout);
        }

        public void AskSelectPiece()
        {

        }

        public void AskMovePiece()
        {

        }
    }
}
