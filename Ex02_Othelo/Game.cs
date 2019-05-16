using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Game
    {
        Board m_Board;
        Player m_Player1, m_Player2;
        string m_Player1NameStr = null, m_Player2NameStr = null;
        string m_CheckTypeGame = null;
        int m_BoardSize = 0;

        public Game()
        {
            StartGame();
        }

        public void StartGame()
        {
            coopGameOrPlayerVsComputer();
            sizeOfTheBoard();
            createBoard(m_BoardSize);
            ShowGame();
            initiateGame();
        }

        private void initiateGame()
        {
            if (m_Player2.Name != "Computer")
            {
                startCoopGame();
            }
            else
            {
                startPlayerVsComputerGame();
            }
        }

        private void startPlayerVsComputerGame()
        {
            if (m_Board.GameOver(m_Player1.Color, m_Player1.Color)) 
            {
                string winnerPlayer = null;
                m_Board.calculateWhoIsTheWinner(out winnerPlayer, m_Player1, m_Player2);
                EndGame(winnerPlayer);
            }

            if (m_Board.CheckIfNoMovesForPlayer(m_Player1.Color))
            {
                UI.NoPossileMoves(m_Player1.Name);
            }
            else
            {
                m_Board.StartTurn(m_Player1);
                ShowGame();
            }

            if (m_Board.CheckIfNoMovesForPlayer(m_Player2.Color))
            {
                UI.NoPossileMoves(m_Player2.Name);
            }
            else
            {
                ComputerPlayer.StartComputerTurn(ref m_Board);
                ShowGame();
            }

            startPlayerVsComputerGame();
        }

        private void coopGameOrPlayerVsComputer()
        {
            m_Player1 = new Player();
            m_Player2 = new Player();
       
            UI.NamePlayer1(out m_Player1NameStr);
            m_Player1.Name = m_Player1NameStr;
            m_Player1.Color = (int)Ex02_Othelo.SqureState.eSqureState.White;
            UI.CheckIfCoopOrVsComputer(out m_CheckTypeGame);

            while (m_CheckTypeGame != "p" && m_CheckTypeGame != "c")
            {
                UI.InvalidCheckGameType(out m_CheckTypeGame);
            }
            if (m_CheckTypeGame == "p")
            {
                UI.NamePlayer2(out m_Player2NameStr);
                m_Player2.Name = m_Player2NameStr;
            }
            else if (m_CheckTypeGame == "c")
            {
                m_Player2.Name = "Computer";
            }

            m_Player2.Color = (int)Ex02_Othelo.SqureState.eSqureState.Black;
        }

        private void sizeOfTheBoard()
        {
            string boardSizeStr = null;
            UI.InputSizeGame(out boardSizeStr);

            while (boardSizeStr != "8" && boardSizeStr != "6")
            {
                UI.InvalidInputBoardSize(out boardSizeStr);
            }

            m_BoardSize = int.Parse(boardSizeStr);
        }

        private void createBoard(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
        }

        public void ShowGame()
        {
            UI.ShowBoard(m_Board, m_BoardSize);
        }

        public void startCoopGame()
        {
            if (m_Board.CheckIfNoMovesForPlayer(m_Player1.Color) && m_Board.CheckIfNoMovesForPlayer(m_Player2.Color))
            {
                string winnerPlayer = null;
                m_Board.calculateWhoIsTheWinner(out winnerPlayer , m_Player1 , m_Player2);
                EndGame(winnerPlayer);
            }

            if (m_Board.CheckIfNoMovesForPlayer(m_Player1.Color)) 
            {
                UI.NoPossileMoves(m_Player1.Name);
            }
            else
            {
                m_Board.StartTurn(m_Player1);
                ShowGame();
            }

            if (m_Board.CheckIfNoMovesForPlayer(m_Player2.Color)) 
            {
                UI.NoPossileMoves(m_Player2.Name);
            }
            else
            {
                m_Board.StartTurn(m_Player2);
                ShowGame();
            }

            startCoopGame();
        }

        public void EndGame(string i_winner)
        {
            string newGame;

            UI.PrintWhoIsTheWinner(i_winner);
            UI.IsStartNewGame(out newGame);
            if ("N" == newGame)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                StartGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
