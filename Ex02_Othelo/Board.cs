using System;
using System.Collections.Generic;
using System.Text;


namespace Ex02_Othelo
{
    public class Board
    {
        public int[,] m_Board;
        private int m_BoardSize;
        private List<char> m_SqureInput = new List<char>();

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public Board(int i_sizeBoard)
        {
            if (i_sizeBoard == 8)
            {
                m_Board = new int[8, 8];
                m_Board[3, 3] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[4, 4] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[4, 3] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[3, 4] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
            }
            else
            {
                m_Board = new int[6, 6];
                m_Board[2, 2] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[3, 3] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[3, 2] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[2, 3] = (int)Ex02_Othelo.SqureState.eSqureState.Black;

                m_Board[0, 0] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[0, 1] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[0, 2] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[0, 3] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[0, 4] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[0, 5] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[1, 0] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[1, 1] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[1, 2] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[1, 3] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[1, 4] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[1, 5] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[2, 0] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[2, 1] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[2, 2] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[2, 3] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[2, 4] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[2, 5] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[4, 0] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[4, 1] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[4, 2] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[4, 3] = (int)Ex02_Othelo.SqureState.eSqureState.White;
                m_Board[4, 4] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
                m_Board[4, 5] = (int)Ex02_Othelo.SqureState.eSqureState.Black;
            }
            m_BoardSize = i_sizeBoard;
        }

        public bool GameOver(int i_Player1Color, int i_Player2Color)
        {
            return CheckIfNoMovesForPlayer(i_Player1Color) && CheckIfNoMovesForPlayer(i_Player2Color);
        }

        public void StartTurn(Player i_Player)
        {
            char tokkenColor;
            string squreInputStr = null;
            m_SqureInput.Clear();
            ValidSqureInputStr(i_Player, ref m_SqureInput, ref squreInputStr);
            insertStrInputToSqureInput(squreInputStr);
            CheckTokkenColor(out tokkenColor, i_Player);

            while (!isValidInput(m_SqureInput))//Run until Valid Input
            {
                squreInputStr = invalidSqureInputReset();
                UI.GetSqureInput(i_Player, ref squreInputStr, tokkenColor);
                ValidSqureInputStr(i_Player, ref m_SqureInput, ref squreInputStr);
                insertStrInputToSqureInput(squreInputStr);
            }

            while (!checkIfSqureInputValidMove(m_SqureInput, i_Player.Color))
            {
                squreInputStr = invalidSqureInputReset();
                ValidSqureInputStr(i_Player, ref m_SqureInput, ref squreInputStr);
                insertStrInputToSqureInput(squreInputStr);
            }
            Ex02.ConsoleUtils.Screen.Clear();
            flipBoard(i_Player.Color, squreInputStr[1], squreInputStr[0]);
        }

        private void insertStrInputToSqureInput(string squreInputStr)
        {
            m_SqureInput.Add(squreInputStr[0]);
            m_SqureInput.Add(squreInputStr[1]);
        }

        private string invalidSqureInputReset()
        {
            string squreInputStr;
            UI.InvalidSqureInput();
            m_SqureInput.Clear();
            squreInputStr = null;
            return squreInputStr;
        }

        public void flipBoard(int i_PlayerColor, int i_row, int i_col)
        {
            int row = i_row - (int)Ex02_Othelo.BoardLimits.eBoardLimits.One, col = i_col - (int)Ex02_Othelo.BoardLimits.eBoardLimits.A;//change chars to ints, Ex: '2'->2

            m_Board [row , col]= i_PlayerColor;
            flipLine(i_PlayerColor, -1, -1, row, col,ref m_Board);
            flipLine(i_PlayerColor, -1, 0, row, col, ref m_Board);
            flipLine(i_PlayerColor, -1, 1, row, col,ref m_Board);
            flipLine(i_PlayerColor, 0, -1, row, col,ref m_Board);
            flipLine(i_PlayerColor, 0, 1, row, col,ref m_Board);
            flipLine(i_PlayerColor, 1, -1, row, col,ref m_Board);
            flipLine(i_PlayerColor, 1, 0, row, col,ref m_Board);
            flipLine(i_PlayerColor, 1, 1, row, col,ref m_Board);
        }

        private bool flipLine(int i_PlayerColor, int i_dirRow, int i_dirCol, int i_row, int i_col,ref int[,] i_board) // recursion algoritam
        {
            if (i_row + i_dirRow < 0 || i_row + i_dirRow > m_BoardSize - 1)
            {
                return false;
            }
            else if (i_col + i_dirCol < 0 || i_col + i_dirCol > m_BoardSize - 1)
            {
                return false;
            }
            else if (m_Board[i_row + i_dirRow, i_col + i_dirCol] == 0)
            {
                return false;
            }
            else if (m_Board[i_row + i_dirRow, i_col + i_dirCol] == i_PlayerColor)
            {
                return true;
            }
            else
            {
                if (flipLine(i_PlayerColor, i_dirRow, i_dirCol, i_row + i_dirRow, i_col + i_dirCol, ref i_board))
                {
                    m_Board[i_row + i_dirRow, i_col + i_dirCol] = i_PlayerColor;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ValidSqureInputStr(Player player, ref List<char> squreInput, ref string squreInputStr)
        {
            char tokkenColor;
            bool validSqureInputStr = false;

            CheckTokkenColor(out tokkenColor, player);

            while (!validSqureInputStr)
            {     
                UI.GetSqureInput( player, ref squreInputStr,tokkenColor);
                if (squreInputStr == "Q")
                {
                    Environment.Exit(0);
                }
                else if (squreInputStr.Length==2)
                {
                    validSqureInputStr = true;
                }
            }
        }

        public void CheckTokkenColor(out char tokkenColor , Player player)
        {
            if (player.Color == (int)Ex02_Othelo.SqureState.eSqureState.White)
            {
                tokkenColor = 'O';
            }
            else
            {
                tokkenColor = 'X';
            }
        }

        private bool isValidInput(List<char> i_squreInput)
        {
            bool isValidInput = false;

            if (m_BoardSize == 8)
            {
                if ((i_squreInput[0] >= (int)Ex02_Othelo.BoardLimits.eBoardLimits.A && i_squreInput[0] <= (int)Ex02_Othelo.BoardLimits.eBoardLimits.H) 
                    && (i_squreInput[1] >= (int)Ex02_Othelo.BoardLimits.eBoardLimits.One && i_squreInput[1] <= (int)Ex02_Othelo.BoardLimits.eBoardLimits.Eight))
                {
                    isValidInput = true;
                }
            }
            else
            {
                if ((i_squreInput[0] >= (int)Ex02_Othelo.BoardLimits.eBoardLimits.A && i_squreInput[0] <= (int)Ex02_Othelo.BoardLimits.eBoardLimits.F) 
                    && (i_squreInput[1] >= (int)Ex02_Othelo.BoardLimits.eBoardLimits.One && i_squreInput[1] <= (int)Ex02_Othelo.BoardLimits.eBoardLimits.Six))
                {
                    isValidInput = true;
                }
            }
            return isValidInput;
        }

        private bool checkIfSqureInputValidMove(List<char> i_squreInput, int i_playerColor)
        {
            bool nw = false, nn = false, ne = false, ww = false, ee = false, sw = false, sn = false, se = false, isValidMoveSqureInput = false; //nw = North-west etc
            int row = i_squreInput[1] - (int)Ex02_Othelo.BoardLimits.eBoardLimits.One, col = i_squreInput[0] - (int)Ex02_Othelo.BoardLimits.eBoardLimits.A; // change ints to char, EX '2'=> 2
            if (m_Board[row, col] == 0)
            {
                nw = isTheMoveValid(i_playerColor, -1, -1, row, col, m_Board);
                nn = isTheMoveValid(i_playerColor, -1, 0, row, col, m_Board);
                ne = isTheMoveValid(i_playerColor, -1, 1, row, col, m_Board);
                ww = isTheMoveValid(i_playerColor, 0, -1, row, col, m_Board);
                ee = isTheMoveValid(i_playerColor, 0, 1, row, col, m_Board);
                sw = isTheMoveValid(i_playerColor, 1, -1, row, col, m_Board);
                sn = isTheMoveValid(i_playerColor, 1, 0, row, col, m_Board);
                se = isTheMoveValid(i_playerColor, 1, 1, row, col, m_Board);

                if (nw || nn || ne || ww || ee || sw || sn || se)
                {
                    isValidMoveSqureInput = true;
                }
            }
            return isValidMoveSqureInput;
        }

        public bool CheckIfNoMovesForPlayer(int i_playerColor)
        {
            bool nw = false, nn = false, ne = false, ww = false, ee = false, sw = false, sn = false, se = false; //nw = North-west etc
            bool IsGameOver = true;
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if (m_Board[row,col] == (int)SqureState.eSqureState.Empty)
                    {
                        nw = isTheMoveValid(i_playerColor, -1, -1, row, col, m_Board);
                        nn = isTheMoveValid(i_playerColor, -1, 0, row, col, m_Board);
                        ne = isTheMoveValid(i_playerColor, -1, 1, row, col, m_Board);
                        ww = isTheMoveValid(i_playerColor, 0, -1, row, col, m_Board);
                        ee = isTheMoveValid(i_playerColor, 0, 1, row, col, m_Board);
                        sw = isTheMoveValid(i_playerColor, 1, -1, row, col, m_Board);
                        sn = isTheMoveValid(i_playerColor, 1, 0, row, col, m_Board);
                        se = isTheMoveValid(i_playerColor, 1, 1, row, col, m_Board);

                        if (nw || nn || ne || ww || ee || sw || sn || se)
                        {
                            IsGameOver = false;
                        }
                    }
                }
            }
            return IsGameOver;
        }

        public bool isTheMoveValid(int i_PlayerColor, int i_dirRow, int i_dirCol, int i_row, int i_col, int[,] i_board)
        {
            int other = (int)Ex02_Othelo.SqureState.eSqureState.Empty;
            bool validMove = true;

            if (i_PlayerColor == (int)Ex02_Othelo.SqureState.eSqureState.Black)
            {
                other = (int)Ex02_Othelo.SqureState.eSqureState.White;
            }
            else
            {
                other = (int)Ex02_Othelo.SqureState.eSqureState.Black;
            }
            if (i_row + i_dirRow < 0 || i_row + i_dirRow > m_BoardSize-1)
            {
                validMove = false;
            }
            else if (i_col + i_dirCol < 0 || i_col + i_dirCol > m_BoardSize-1)
            {
                validMove = false;
            }
            else if (m_Board [i_row+i_dirRow, i_col + i_dirCol] != other)
            {
                validMove = false;
            }
            else if (i_row + i_dirRow + i_dirRow < 0 || i_row + i_dirRow + i_dirRow > m_BoardSize-1)
            {
                validMove = false;
            }
            else if (i_col + i_dirCol + i_dirCol < 0 || i_col + i_dirCol + i_dirCol > m_BoardSize-1)
            {
                validMove = false;
            }
            return validMove && checkLineMatch(i_PlayerColor, i_dirRow, i_dirCol , i_row + i_dirRow + i_dirRow, i_col + i_dirCol + i_dirCol,m_Board);
        }

        private bool checkLineMatch(int i_PlayerColor, int i_dirRow, int i_dirCol, int i_row, int i_col, int[,] i_board) // recursion algoritam
        {
            if (m_Board[i_row, i_col] == i_PlayerColor) // If we found same color token in the line
            {
                return true;
            }
            if (i_row + i_dirRow < 0 || i_row + i_dirRow > m_BoardSize-1) // If we reached the board limits
            {
                return false;
            }
            if (i_col + i_dirCol < 0 || i_col + i_dirCol > m_BoardSize-1) // If we reached the board limits
            {
                return false;
            }
            return checkLineMatch(i_PlayerColor, i_dirRow, i_dirCol, i_row + i_dirRow, i_col + i_dirCol, i_board); // Recursion for checking the rest of the line
        }

        public void calculateWhoIsTheWinner(out string io_WinnerPlayer , Player i_Player1 , Player i_Player2)
        {
            int countWhiteTokens = 0, countBlackTokens = 0;

            for (int row = 0; row < m_BoardSize - 1; row++)
            {
                for (int col = 0; col < m_BoardSize - 1; col++)
                {
                    if (m_Board[row, col] == (int)Ex02_Othelo.SqureState.eSqureState.Black)
                    {
                        countBlackTokens++;
                    }
                    if (m_Board[row, col] == (int)Ex02_Othelo.SqureState.eSqureState.White)
                    {
                        countWhiteTokens++;
                    }
                }
            }

            if (countWhiteTokens > countBlackTokens)
            {
                io_WinnerPlayer = i_Player1.Name;
            }
            else if (countWhiteTokens < countBlackTokens)
            {
                io_WinnerPlayer = i_Player2.Name;
            }
            else
            {
                io_WinnerPlayer = "NoBody!";
            }
        }
    }
}
