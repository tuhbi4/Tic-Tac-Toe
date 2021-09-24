using System;
using System.Collections.Generic;
using static TicTacToe.InputValidator;
using static TicTacToe.Combinator;

namespace TicTacToe
{
    /// <summary>
    /// Represents a game "Tic Tac Toe".
    /// The game is a tic-tac-toe with a field n * n, where n is a natural odd number, n >= 3.
    /// It is necessary to provide a menu to the user where he can choose a game with another player (on the same PC) or a game with a bot.
    /// And also a menu where the user chooses himself who walks first, or is chosen randomly. The user should also be able to choose what he will walk with: a cross or a zero.
    /// 
    /// First, the first player chooses a place on the field where he will put a cross(zero), after the second player(or bot) puts a zero(cross),
    /// and so on until the goal of the game is fulfilled.Goal of the game:
    /// score the largest number of combinations of three crosses / zeros(according to the rules of the game - diagonally, vertically and horizontally).
    /// 
    /// If there are no more options to make a combination, the game ends and displays the results of the game.
    /// After the participant reaches the number of combinations in which the opponent cannot get ahead of him, the game ends and displays the results of the game.
    /// 
    /// <see cref="GameMode"/> contains the game mode selected from enumeration <see cref="GameModes"/>.
    /// <see cref="Winner"/> contains the name of the winner
    /// <see cref="CurrentBoard"/> Board with parameters for the current game specified at the start.
    /// <see cref="Players"/> List of players for the current game specified at the start.
    /// </summary>
    public class Game
    {
        public enum GameModes
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer
        };
        public GameModes GameMode { get; private set; }
        public string Winner { get; private set; }
        public bool GameOver { get; private set; }
        public Board CurrentBoard { get; private set; }
        public List<Player> Players { get; } = new() { };

        public Game()
        {
            PrintLogo();
            GameMode = (GameModes)RequestGameMode();
            DefinePlayersList();
            CurrentBoard = new Board(RequestBoardSize(), true);
        }

        /// <summary>
        /// Print game logo to console.
        /// </summary>
        private static void PrintLogo()
        {
            Console.WriteLine("          +------------------------------+\n          |\\* <><><><><><><><><><><><> */|\n          | +--------------------------+ |");
            Console.WriteLine("          |*||------------------------||*| \n          + ||* THE TIC TAC TOE GAME *|| +\n           \\||------------------------||/");
            Console.WriteLine("            +--------------------------+ ");
        }

        /// <summary>
        /// Define a list of players (with names and symbols) depending on the game mode.
        /// </summary>
        private void DefinePlayersList()
        {
            var playerOneName = RequestName("Player 1");
            RequestPlayerSymbol(playerOneName, out string playerOneSymbol, out string playerTwoSymbol);
            Players.Add(new Player(playerOneName, playerOneSymbol));
            if (GameMode.Equals(GameModes.PlayerVsPlayer))
            {
                var playerTwoName = RequestName("Player 2");
                Players.Add(new Player(playerTwoName, playerTwoSymbol));
            }
            else if (GameMode.Equals(GameModes.PlayerVsComputer))
            {
                Console.WriteLine("Your opponent is Computer!");
                if (Players[0].Symbol.Equals(playerOneSymbol))
                {
                    Players.Add(new Bot("Computer", playerTwoSymbol));
                }
                else
                {
                    Players.Add(new Bot("Computer", playerOneSymbol));
                }
            }
        }

        /// <summary>
        /// Main control algorithm.
        /// </summary>
        public void StartGame()
        {
            Console.WriteLine($"\n***** Game started! *****\n");
            var currentTurnNumber = 0;
            CurrentBoard.DrawBoardInConsole();
            while (!GameOver)
            {
                currentTurnNumber++;
                Console.WriteLine($"\n*** Current turn: {currentTurnNumber} ***");
                NextTurn();
            }
            WhoWins();
        }

        /// <summary>
        /// Makes a turn request for each player.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when type of player is invalid.</exception>
        private void NextTurn()
        {
            for (int playerNumber = 0; playerNumber < Players.Count; playerNumber++)
            {
                if (!IsGameOver())
                {
                    Console.WriteLine($"\n{Players[playerNumber].Name}, now it's your turn!");
                    int coordinateX, coordinateY;
                    bool isFieldFlaggedSuccessfully;
                    do
                    {
                        if (Players[playerNumber].GetType() == typeof(Player))
                        {
                            PlayerTurn(out coordinateX, out coordinateY);
                        }
                        else if (Players[playerNumber].GetType() == typeof(Bot))
                        {
                            ComputerTurn(playerNumber, out coordinateX, out coordinateY);
                            Console.WriteLine($"\n{Players[playerNumber].Name} makes turn...");
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                        isFieldFlaggedSuccessfully = CurrentBoard.FlagTheField(coordinateX, coordinateY, Players[playerNumber].Symbol);
                        if (!isFieldFlaggedSuccessfully)
                        {
                            Console.WriteLine($"\n*** The field [{coordinateX},{coordinateY}] already flagged! You have to choose another. ***");
                        }
                    }
                    while (!isFieldFlaggedSuccessfully);
                    var currentCountOfCombinations = Players[playerNumber].CountOfCombinationsMade;
                    Players[playerNumber].IncreaseCountOfCombinationMade(CountOfNewCombinationsAppeared(coordinateX, coordinateY, CurrentBoard));
                    if (Players[playerNumber].CountOfCombinationsMade > currentCountOfCombinations)
                    {
                        
                        Console.WriteLine($"*** {Players[playerNumber].Name}, now you have {Players[playerNumber].CountOfCombinationsMade} points! ***");
                    }

                }
            }
        }

        /// <summary>
        /// Requests the coordinates of the field from the player.
        /// </summary>
        private void PlayerTurn(out int coordinateX, out int coordinateY)
        {
            coordinateX = ValueValidator.ValueOfVariableValidation("coordinate X", 1, CurrentBoard.Rows);
            coordinateY = ValueValidator.ValueOfVariableValidation("coordinate Y", 1, CurrentBoard.Cols);
        }

        /// <summary>
        /// Requests the coordinates of a field from the computer.
        /// </summary>
        private void ComputerTurn(int playerNumber, out int coordinateX, out int coordinateY)
        {
            ((Bot)Players[playerNumber]).MakeTurn(1, CurrentBoard.Rows, out int x, out int y);
            coordinateX = x;
            coordinateY = y;
        }

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <returns>true if no more empty fields on the board 
        /// or if one of the participants reaches the number of combinations in which the opponent cannot get ahead of him
        /// or if there are no more options to make a combination; 
        /// otherwise, false</returns>
        private bool IsGameOver()
        {
            if (CurrentBoard.EmptyCellsCount == 0)
            {
                Console.WriteLine("*** There are no more empty fields on the board... ***");
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
        private void SetWinner()
        {
            if (Players[0].CountOfCombinationsMade != Players[1].CountOfCombinationsMade)
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
        }

        /// <summary>
        /// Sets the winner based on the number of combinations made.
        /// </summary>
        private void WhoWins()
        {
            if (!(Winner is null))
            {
                Console.WriteLine($"\n***** GAME OVER! *****\nCongratulations {Winner}, you won!");
            }
            else
            {
                if (Players[0].CountOfCombinationsMade != 0)
                {
                    Console.WriteLine($"\n***** GAME OVER! *****\nIncredible, the players scored the same number of points! Draw!");
                }
                else
                {
                    Console.WriteLine($"\n***** GAME OVER! *****\nIncredible, the players failed to score! You are both losers!");
                }
            }
        }
    }
}