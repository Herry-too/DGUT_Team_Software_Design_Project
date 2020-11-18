using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_Console
{
    class GameBoard
    {
        String player = "red";
        Piece[,] pieces;
        int selectedX = -1;
        int selectedY = -1;

        public GameBoard()
        {
            pieces = new Piece[10, 9];                      //Create a new gameboard, include 10*9 places to hold piece
            //Set Red Player default pieces
            pieces[0, 0] = new CarPiece("red", 0, 0);
            pieces[0, 1] = new HorsePiece("red", 0, 1);
            pieces[0, 2] = new ElephantPiece("red", 0, 2);
            pieces[0, 3] = new AdvisorPiece("red", 0, 3);
            pieces[0, 4] = new GeneralPiece("red", 0, 4);
            pieces[0, 5] = new AdvisorPiece("red", 0, 5);
            pieces[0, 6] = new ElephantPiece("red", 0, 6);
            pieces[0, 7] = new HorsePiece("red", 0, 7);
            pieces[0, 8] = new CarPiece("red", 0, 8);
            pieces[2, 1] = new CannonPiece("red", 2, 1);
            pieces[2, 7] = new CannonPiece("red", 2, 7);
            pieces[3, 0] = new SoldierPiece("red", 3, 0);
            pieces[3, 2] = new SoldierPiece("red", 3, 2);
            pieces[3, 4] = new SoldierPiece("red", 3, 4);
            pieces[3, 6] = new SoldierPiece("red", 3, 6);
            pieces[3, 8] = new SoldierPiece("red", 3, 8);
            //Same as Black Player
            pieces[9, 0] = new CarPiece("black", 9, 0);
            pieces[9, 1] = new HorsePiece("black", 9, 1);
            pieces[9, 2] = new ElephantPiece("black", 9, 2);
            pieces[9, 3] = new AdvisorPiece("black", 9, 3);
            pieces[9, 4] = new GeneralPiece("black", 9, 4);
            pieces[9, 5] = new AdvisorPiece("black", 9, 5);
            pieces[9, 6] = new ElephantPiece("black", 9, 6);
            pieces[9, 7] = new HorsePiece("black", 9, 7);
            pieces[9, 8] = new CarPiece("black", 9, 8);
            pieces[7, 1] = new CannonPiece("black", 7, 1);
            pieces[7, 7] = new CannonPiece("black", 7, 7);
            pieces[6, 0] = new SoldierPiece("black", 6, 0);
            pieces[6, 2] = new SoldierPiece("black", 6, 2);
            pieces[6, 4] = new SoldierPiece("black", 6, 4);
            pieces[6, 6] = new SoldierPiece("black", 6, 6);
            pieces[6, 8] = new SoldierPiece("black", 6, 8);
        }

        public Piece[,] getPieces()
        {
            return pieces;  //return all pieces,string.
        }

        public string getPlayer()
        {
            return player;  //return current player, string.
        }
        public string getPieceName(int x,int y)
        {
            if(pieces[x,y] == null)
            {
                return "";  //if there is no piece at this place
            }
            else
            {
                return pieces[x, y].getPieceWords();//else return the Name of the Piece
            }
        }

        public string getPiecePlayer(int x,int y)
        {
            return pieces[x, y].getPlayer();    //get a piece's player
        }

        public void SwitchPlayer()  //Switch player
        {
            if(player == "red")
            {
                player = "black";
            }
            else
            {
                player = "red";
            }
        }

        int[] strInputToIntArrayInput(String strInput)
        {
            int[] returnArray = new int[2] { -1,-1};
            if (strInput.Length != 2)
                return new int[] { -1,-1};  //if there is a invaild input,return {-1,-1}
            for (int i = 0; i < strInput.Length; i++)
            {
                if (strInput[i] > 96 && strInput[i] < 106)
                    returnArray[1] = strInput[i] - 97;  //if there is a character between alphabet 'a' to 'i', change it to int and set it as the posY/intY
                if (strInput[i] > 47 && strInput[i] < 58)
                    returnArray[0] = strInput[i] - 48;// same
            }
            return returnArray;
        }
        public bool SelectPiece(String strInput)
        {
            int[] intArray = strInputToIntArrayInput(strInput);
            int posX = intArray[0];
            if (intArray[0] == -1 || intArray[1] == -1)
                return false;// if invaild number, return false
            int posY = intArray[1];
            if (pieces[posX,posY] == null)
            {
                return false;//if there is no pieces at this place,could not select nothing,return false
            }
            if (pieces[posX, posY].getPlayer() == player)//make sure only cureent player could select own pieces
            {
                selectedX = posX;
                selectedY = posY;
                return true;
            }
            return false;
        }

        public bool MovePiece(String strInput)
        {
            int posX, posY;
            int[] intArray = strInputToIntArrayInput(strInput);
            if (intArray[0] == -1 || intArray[1] == -1)
                return false;//same as SelectPiece
            posX = intArray[0];
            posY = intArray[1];
            if (posX == selectedX && posY == selectedY) //cancel the select
            {
                SwitchPlayer();
                selectedX = -1;
                selectedY = -1;
                return true;
            }
            if (CalculateValidMoves(posX, posY))//check if it could move
            {
                pieces[posX, posY] = pieces[selectedX, selectedY];//coverage the pieces
                pieces[posX, posY].setCurrentPosition(posX, posY);
                pieces[selectedX, selectedY] = null;//remove old pieces
                selectedX = selectedY = -1;// remove selected record.
                return true;
            }
            return false;
        }

        bool CalculateValidMoves(int posX, int posY)
        {
            if (posX < 0 || posX > 9)
            {
                return false;//if the point is out of board
            }
            if (posY < 0 || posY > 8)
            {
                return false;//same
            }
            if(player != pieces[selectedX, selectedY].getPlayer())
            {
                return false;//current player is the owner of the piece
            }
            if (pieces[posX, posY] != null && player == pieces[posX, posY].getPlayer())
                return false;//dont eat own pieces
            return pieces[selectedX, selectedY].ValidMoves(posX, posY, this);//let pieces check it's rule
        }

        public int getSelectedX()
        {
            return selectedX;
        }
        public int getSelectedY()
        {
            return selectedY;
        }
    }
}
