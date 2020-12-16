using System;
using System.Collections.Generic;
using System.Text;

namespace DGUT_Team_Software_Project_WPF
{
    class AIModel
    {
        public Piece[,] board { get; set; }
        public GameBoard GameBoard;
        public int[,,] addmove = new int[7, 256, 4];
        public int[] movehistory = new int[7] { -1, -1, -1, -1, -1, -1, -1 };
        public int[,] cucvlPiecePos = new int[7, 256];
        public AIModel(GameBoard gameBoard)
        {
            this.board = gameBoard.getPieces();
            this.GameBoard = gameBoard;
        }
        public int evaluate()
        {
            int vlEvaluate = 0; // 相对于红方来说的局面评价值
            for (sq = 0; sq < 256; sq++)
            {
                if (IS_RED(pc))
                {
                    vlEvaluate += cucvlPiecePos[PIECE_TYPE(pc)][sq];
                }
                else if (IS_BLACK(pc))
                {
                    vlEvaluate -= cucvlPiecePos[PIECE_TYPE(pc)][SQUARE_FLIP(sq)];
                }
            }
            return vlEvaluate;
        }
        public void AddMove(string name, int intX, int intY, int DesX, int DesY)
        {
            int playerNo;
            switch (name)
            {
                case "兵":
                    {
                        playerNo = 0;
                        movehistory[playerNo]++;
                        break;
                    }
                case "炮":
                    {
                        playerNo = 1;
                        movehistory[playerNo]++;
                        break;
                    }
                case "俥":
                    {
                        playerNo = 2;
                        movehistory[playerNo]++;
                        break;
                    }
                case "傌":
                    {
                        playerNo = 3;
                        movehistory[playerNo]++;
                        break;
                    }
                case "相":
                    {
                        playerNo = 4;
                        movehistory[playerNo]++;
                        break;
                    }
                case "士":
                    {
                        playerNo = 5;
                        movehistory[playerNo]++;
                        break;
                    }
                case "帥":
                    {
                        playerNo = 6;
                        movehistory[playerNo]++;
                        break;
                    }
                default:
                    {
                        playerNo = -1;
                        break;
                    }
            }
            addmove[playerNo, movehistory[playerNo], 0] = intX;
            addmove[playerNo, movehistory[playerNo], 1] = intY;
            addmove[playerNo, movehistory[playerNo], 2] = DesX;
            addmove[playerNo, movehistory[playerNo], 3] = DesY;
        }
        public int AlphaBeta(int vlAlpha, int vlBeta, int nDepth)
        {
            
            if (nDepth == 0)
            {
                return evaluate();
            }
            //生成全部走法
            foreach (Piece piece in board)
            {
                //检查当前是否为红方
                if (piece.getPlayer()==Piece.Players.red)
                {
                    //低效遍历，需改进
                    for(int i = 0; i < 9; i++)
                    {
                        for(int j = 0; j < 8; j++)
                        {
                            if (piece.ValidMoves(piece.intX + i, piece.intY + j, GameBoard))
                            {
                                AddMove(piece.getPieceWords(), piece.intX, piece.intY, piece.intX + i, piece.intY + j);
                            }
                        }
                    }
                }
            }
            for(int i = 0; i < 6; i++)
            {
                int posX = addmove[i, movehistory[i], 2];
                int posY = addmove[i, movehistory[i], 3];
                bool flag;
                if (GameBoard.boolMovePiece(posX, posY))
                {
                    if (GameBoard.ifDeliveredCheck())
                    {
                        flag = GameBoard.boolMovePiece(addmove[i, movehistory[i], 0], addmove[i, movehistory[i], 1]);
                    }
                    else
                    {
                        int vl = -AlphaBeta(-vlBeta, -vlAlpha, nDepth - 1);
                        flag = GameBoard.boolMovePiece(addmove[i, movehistory[i], 0], addmove[i, movehistory[i], 1]);
                        if (vl >= vlBeta)
                        {
                            return vlBeta;
                        }
                        if (vl > vlAlpha)
                        {
                            vlAlpha = vl;
                        }
                    }
                }
            }
            return vlAlpha;
        }
        
    }
    
}
