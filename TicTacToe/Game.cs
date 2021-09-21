using System;
using System.Collections.Generic;
using static TicTacToe.Input;

namespace TicTacToe
{
    public class Game
    {
        public string GameMode { get; }
        private readonly List<string> gameModes = new()
        {
            "Player Vs Player",
            "Player Vs Computer"
        };
        public static string Winner { get; private set; }
        public static bool GameOver { get; private set; }
        public static Board CurrentBoard { get; private set; }
        public static List<Player> Players { get; } = new() { };

        public Game()
        {
            PrintLogo();
            GameMode = gameModes[RequestGameMode() - 1];
            DefinePlayersList();
            CurrentBoard = new Board(RequestBoardSize());
        }

        /// <summary>
        /// Print game logo to console.
        /// </summary>
        private static void PrintLogo()
        {
            Console.WriteLine("THE TIC TAC TOE GAME"); // TODO: game logo
        }

        /// <summary>
        /// Define a list of players (with names and symbols) depending on the game mode.
        /// </summary>
        private void DefinePlayersList()
        {
            var playerOneName = RequestName("Player 1");
            RequestPlayerSymbol(playerOneName, out string playerOneSymbol, out string playerTwoSymbol);
            Players.Add(new Player(playerOneName, playerOneSymbol));
            if (GameMode.Equals(gameModes[0]))
            {
                var playerTwoName = RequestName("Player 2");
                Players.Add(new Player(playerTwoName, playerTwoSymbol));
            }
            else if (GameMode.Equals(gameModes[1]))
            {
                Console.WriteLine("Your opponent is Computer!");
                if (Players[0].Symbol.Equals(playerOneSymbol))
                {
                    Players.Add(new("Computer", playerTwoSymbol));
                }
                else
                {
                    Players.Add(new("Computer", playerOneSymbol));
                }
            }
        }

        /// <summary>
        /// Main control algorithm.
        /// </summary>
        public void StartGame()
        {
            Console.WriteLine($"\nGame started!");
            var currentTurnNumber = 0;
            CurrentBoard.DrawBoardInConsole();
            while (!GameOver)
            {
                currentTurnNumber++;
                Console.WriteLine($"\nCurrent turn: {currentTurnNumber}.");
                NextTurn();
            }
            WhoWins();
        }

        /// <summary>
        /// Makes a turn request for each player.
        /// </summary>
        private static void NextTurn()
        {
            foreach (Player player in Players)
            {
                if (!IsGameOver())
                {
                    Console.WriteLine($"\n{player.Name}, your turn!");
                    var field = new Field(Validator.ValueOfVariableValidation("coordinate X", 1, CurrentBoard.Rows), Validator.ValueOfVariableValidation("coordinate Y", 1, CurrentBoard.Cols));
                    CurrentBoard.FlagTheField(field.X, field.Y, player.Symbol);
                    IsNewCombinationAppeared();
                }
            }
        }

        /// <summary>
        /// Checks if a new combination has appeared.
        /// </summary>
        private static void IsNewCombinationAppeared()
        {
            // TODO: implement the search for new combinations
        }

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <returns>true if no more empty fields on the board 
        /// or if one of the participants reaches the number of combinations in which the opponent cannot get ahead of him
        /// or if there are no more options to make a combination; 
        /// otherwise, false</returns>
        private static bool IsGameOver()
        {
            if (CurrentBoard.EmptyCellsCount == 0)
            {
                Console.WriteLine("There are no more empty fields on the board...");
                GameOver = true;
                SetWinner();
                return true;
            } // TODO: implement the remaining conditions for the return
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the winner based on the number of combinations made.
        /// </summary>
        private static void SetWinner()
        {
            if (Players[0].CountOfCombinationsMade > Players[1].CountOfCombinationsMade)
            {
                Winner = Players[0].Name;
            }
            else
            {
                Winner = Players[1].Name;
            }
        }

        /// <summary>
        /// Sets the winner based on the number of combinations made.
        /// </summary>
        private static void WhoWins()
        {
            Console.WriteLine($"\n*****GAME OVER!*****\nCongratulations {Winner}, you wins!");
        }
    }
}