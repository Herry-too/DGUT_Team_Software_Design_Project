using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GameDisplay
    {
        public void DisplayBoard()
        {
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
