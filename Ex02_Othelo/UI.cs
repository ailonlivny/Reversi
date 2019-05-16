using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class UI
    {
        public static void NamePlayer1(out string io_player1Str)
        {
            Console.WriteLine("Enter your name:");
            io_player1Str = Console.ReadLine();
        }

        public static void CheckIfCoopOrVsComputer(out string io_checkGameType)
        {
            Console.WriteLine("Co-op game (Press p) or vs computer (Press c)?");
            io_checkGameType = Console.ReadLine();
        }

        public static void InvalidCheckGameType(out string io_checkGameType)
        {
            Console.WriteLine("Invalid input,for Co - op game(Press p) or vs computer(Press c)");
            io_checkGameType = Console.ReadLine();
        }

        public static void NamePlayer2(out string io_player2)
        {
            Console.WriteLine("Enter the second player name:");
            io_player2 = Console.ReadLine();
        }

        public static void InputSizeGame(out string io_sizeGame)
        {
            Console.WriteLine("Choose the size of the game(for 6*6 press 6 and for 8*8 press 8):");
            io_sizeGame = Console.ReadLine();
        }

        public static void InvalidInputBoardSize(out string io_BoardSize)
        {
            Console.WriteLine("Invalid Input! ");
            InputSizeGame(out io_BoardSize);
        }

        public static void GetSqureInput(Player i_player ,ref string squreInputStr , char tokken)
        {
            Console.WriteLine("{0}'s turn,your tokken is {1} please make your move(Letter and number ex E3)", i_player.Name , tokken);
            squreInputStr = Console.ReadLine();
        }

        public static void NoPossileMoves(string i_namePlayer)
        {
            Console.WriteLine("No possible moves for {0} ", i_namePlayer);
        }

        public static void InvalidSqureInput()
        {
            Console.WriteLine("Invalid Input! ");
        }

        public static void PrintWhoIsTheWinner(string i_winner)
        {
            Console.WriteLine(String.Format("The winner is :{0}", i_winner));
        }

        public static void IsStartNewGame(out string io_NewGame)
        {
            Console.WriteLine("To start new game press N");
            io_NewGame = Console.ReadLine();
        }

        public static void ShowBoard(Board m_Board,int m_SizeBoard)
        {
            string collIndex = "ABCDEFGH";
            int rowLength = m_SizeBoard;
            int colLength = m_SizeBoard;

            Console.Write("    ");

            for (int i = 0; i < m_SizeBoard; i++)
            {
                Console.Write(collIndex[i]);
                Console.Write("   ");
            }

            Console.Write(Environment.NewLine);
            Console.Write("  =");

            for (int i = 0; i < m_SizeBoard; i++)
            {
                Console.Write("====");
            }

            Console.Write(Environment.NewLine);

            for (int i = 0; i < rowLength; i++)
            {
                Console.Write(i + 1);
                Console.Write(" ");
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write("|");

                    if (m_Board.m_Board[i, j] == (int)Ex02_Othelo.SqureState.eSqureState.Black)
                    {
                        Console.Write(" X ");
                    }
                    else if (m_Board.m_Board[i, j] == (int)Ex02_Othelo.SqureState.eSqureState.White)
                    {
                        Console.Write(" O ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }

                Console.Write("|");
                Console.Write(Environment.NewLine);
                Console.Write("  =");

                for (int j = 0; j < m_SizeBoard; j++)
                {
                    Console.Write("====");
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
