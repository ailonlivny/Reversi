using System;
using System.Collections.Generic;
using System.Text;
//using ailon = Ex02_Othelo.SqureState.eSqureState;
namespace Ex02_Othelo
{
    class ComputerPlayer
    {
        public static void StartComputerTurn(ref Board m_Board)
        {
            int rowToPlay = 0;
            int colToPlay = 0;

            minimaxDecision(ref m_Board, (int)Ex02_Othelo.SqureState.eSqureState.Black, out rowToPlay, out colToPlay);
            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.flipBoard((int)Ex02_Othelo.SqureState.eSqureState.Black, rowToPlay +(int)Ex02_Othelo.BoardLimits.eBoardLimits.One, colToPlay + (int)Ex02_Othelo.BoardLimits.eBoardLimits.A);
        }

        private static int heuristic(Board i_Board, int i_PlayerColor)
        {
            int opponent = (int)Ex02_Othelo.SqureState.eSqureState.Empty;

            if (i_PlayerColor == (int)Ex02_Othelo.SqureState.eSqureState.White)
            {
                opponent = (int)Ex02_Othelo.SqureState.eSqureState.Black;
            }
            else
            {
                opponent = (int)Ex02_Othelo.SqureState.eSqureState.White;
            }

            int ourScore = getScoreFromBoard(i_Board, i_PlayerColor);
            int opponentScore = getScoreFromBoard(i_Board, opponent);
            return (ourScore - opponentScore);
        }

        private static int getScoreFromBoard(Board i_Board, int i_PlayerColor)
        {
            int countTokens = 0;

            for (int row = 0; row < i_Board.BoardSize - 1; row++)
            {
                for (int col = 0; col < i_Board.BoardSize - 1; col++)
                {
                    if (i_Board.m_Board[row, col] == i_PlayerColor)
                    {
                        countTokens++;
                    }
                }
            }

            return countTokens;
        }

        private static void copyBoard(ref Board i_src, ref Board i_dest)
        {
            for (int row = 0; row < i_src.BoardSize - 1; row++)
            {
                for (int col = 0; col < i_src.BoardSize - 1; col++)
                {
                    i_dest.m_Board[row, col] = i_src.m_Board[row, col];
                }
            } 
        }

        private static void getMoveList(Board i_Board, int[] i_MoveRow, int[] i_MoveCol,ref int i_NumMoves, int i_PlayerColor)
        {
            int indexOfMoveX = 0, indexOfMoveY = 0;

            for (int row = 0; row < i_Board.BoardSize; row++)
            {
                for (int col = 0; col < i_Board.BoardSize; col++)
                {
                    if (i_Board.m_Board[row, col] == 0)
                    {
                        if (i_Board.isTheMoveValid(i_PlayerColor, -1, -1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, -1, 0, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, -1, 1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, 0, -1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, 0, 1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, 1, -1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, 1, 0, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                        if (i_Board.isTheMoveValid(i_PlayerColor, 1, 1, row, col, i_Board.m_Board))
                        {
                            i_MoveRow[indexOfMoveX++] = row;
                            i_MoveCol[indexOfMoveY++] = col;
                            i_NumMoves++;
                        }
                    }
                }
            }
        }

        public static void minimaxDecision(ref Board i_Board,int i_PlayerColor,out int io_Row,out int io_Col)
        {
            int[] moveRow = new int[60];
            int[] moveCol = new int[60];
            int numMoves = 0;
            int opponent = (int)SqureState.eSqureState.Empty;

            if (i_PlayerColor == (int)SqureState.eSqureState.Black)
            {
                opponent = (int)SqureState.eSqureState.White;
            }
            else
            {
                opponent = (int)SqureState.eSqureState.Black;
            }

            getMoveList(i_Board, moveRow, moveCol,ref numMoves, i_PlayerColor);
            if (numMoves == 0) // if no moves return -1
            {
                io_Row = -1;
                io_Col = -1;
            }
            else
            {
                int bestMoveVal = -99999;
                int bestRow = moveRow[0], bestCol = moveCol[0];

                for (int i = 0; i < numMoves; i++)
                {
                    Board tempBoard = new Board(i_Board.BoardSize); // todo 8 and 6 sizeBoard
                    copyBoard(ref i_Board,ref tempBoard);
                    tempBoard.flipBoard(i_PlayerColor,moveRow[i]+ (int)Ex02_Othelo.BoardLimits.eBoardLimits.One, moveCol[i]+ (int)Ex02_Othelo.BoardLimits.eBoardLimits.A);

                    int val = minimaxValue(ref tempBoard, i_PlayerColor, opponent, 1);

                    if (val > bestMoveVal)
                    {
                        bestMoveVal = val;
                        bestRow = moveRow[i];
                        bestCol = moveCol[i];
                    }
                }

                io_Row = bestRow;
                io_Col = bestCol;
            }
        }

        private static int minimaxValue(ref Board i_Board, int i_OriginalTurn, int i_CurrentTurn, int i_Highlevel)
        {
            if ((i_Highlevel == 5) || i_Board.GameOver((int)Ex02_Othelo.SqureState.eSqureState.Black, (int)Ex02_Othelo.SqureState.eSqureState.White))
            {
                return heuristic(i_Board, i_OriginalTurn);
            }
            int[] moveRow = new int[60];
            int[] moveCol = new int[60];
            int numMoves = 0;
            int opponent = (int)SqureState.eSqureState.Empty;

            if (i_CurrentTurn == (int)SqureState.eSqureState.Black)
            {
                opponent = (int)SqureState.eSqureState.White;
            }
            else
            {
                opponent = (int)SqureState.eSqureState.Black;
            }

            getMoveList(i_Board, moveRow, moveCol, ref numMoves, i_CurrentTurn);

            if (numMoves == 0)
            {
                return minimaxValue(ref i_Board, i_OriginalTurn, opponent, i_Highlevel + 1);
            }
            else
            {
                int bestMoveVal = -99999; // for finding max
                if (i_OriginalTurn != i_CurrentTurn)
                {
                    bestMoveVal = 99999; // for finding min
                }
                for (int i = 0; i < numMoves; i++)
                {
                    Board tempBoard = new Board(i_Board.BoardSize); // to do 6 and 8                    copyBoard(ref i_Board,ref tempBoard);
                    tempBoard.flipBoard(i_CurrentTurn, moveRow[i]+(int)Ex02_Othelo.BoardLimits.eBoardLimits.One, moveCol[i]+(int)Ex02_Othelo.BoardLimits.eBoardLimits.A);                    int minimaxValueRec  = minimaxValue(ref tempBoard, i_OriginalTurn, opponent, i_Highlevel + 1);                    if (i_OriginalTurn == i_CurrentTurn)
                    {
                        if (minimaxValueRec > bestMoveVal)
                        {
                            bestMoveVal = minimaxValueRec ;
                        }
                    }                    else
                    {
                        if (minimaxValueRec  < bestMoveVal)
                        {
                            bestMoveVal = minimaxValueRec ;
                        }
                    }
                }
                return bestMoveVal;
            }
        }
    }
}
