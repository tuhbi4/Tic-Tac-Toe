using System;
using System.Collections.Generic;
using static TicTacToe.InputValidator;
using static TicTacToe.Combinator;
using static TicTacToe.UI;

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
        public string EmptyFieldFigure { get; }
        public string PlayerOneFigure { get; private set; }
        public string PlayerTwoFigure { get; private set; }
        public string Winner { get; private set; }
        public bool GameOver { get; private set; }
        public Board CurrentBoard { get; private set; }

        public List<Player> Players { get; } = new() { };

        public Game(string emptyFieldFigure, string playerOneFigure, string playerTwoFigure)
        {
            if (emptyFieldFigure.Length != playerOneFigure.Length || emptyFieldFigure.Length != playerTwoFigure.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(emptyFieldFigure), "The length of all figures must be the same.");
            }
            if (emptyFieldFigure == playerOneFigure || emptyFieldFigure == playerTwoFigure || playerOneFigure == playerTwoFigure)
            {
                throw new ArgumentOutOfRangeException(nameof(emptyFieldFigure), "All figures must be different.");
            }
            else
            {
                EmptyFieldFigure = emptyFieldFigure;
                PlayerOneFigure = playerOneFigure;
                PlayerTwoFigure = playerTwoFigure;
                InitializeGame(emptyFieldFigure);
            }
        }

        /// <summary>
        /// Initialize the game.
        /// </summary>
        private void InitializeGame(string emptyFieldFigure)
        {
            PrintLogo();
            GameMode = (GameModes)RequestGameMode();
            DefinePlayersList();
            CurrentBoard = new Board(RequestBoardSize(), emptyFieldFigure);
        }

        /// <summary>
        /// Define a list of players (with names and figures) depending on the game mode.
        /// </summary>
        private void DefinePlayersList()
        {
            var playerOneName = RequestName("Player 1");
            if (PlayerRequestFigureChange(playerOneName, PlayerOneFigure, PlayerTwoFigure))
            {
                var temp = PlayerOneFigure;
                PlayerOneFigure = PlayerTwoFigure;
                PlayerTwoFigure = temp;
            }
            Players.Add(new Player(playerOneName, PlayerOneFigure));
            if (GameMode.Equals(GameModes.PlayerVsPlayer))
            {
                var playerTwoName = RequestName("Player 2");
                Players.Add(new Player(playerTwoName, PlayerTwoFigure));
            }
            else if (GameMode.Equals(GameModes.PlayerVsComputer))
            {
                MessageIfOpponentIsBot();
                if (Players[0].Symbol.Equals(PlayerOneFigure))
                {
                    Players.Add(new Bot("Computer", PlayerTwoFigure));
                }
                else
                {
                    Players.Add(new Bot("Computer", PlayerOneFigure));
                }
            }
        }

        /// <summary>
        /// Main control algorithm.
        /// </summary>
        public void StartGame()
        {
            MessageAboutGameStarted();
            var currentTurnNumber = 0;
            DrawBoardInConsole(CurrentBoard, true);
            while (!GameOver)
            {
                currentTurnNumber++;
                MessageAboutTurn(currentTurnNumber);
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
                    MessageWhoseTurn(Players[playerNumber].Name);
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
                            MessageBotMadeTurn(Players[playerNumber].Name);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                        isFieldFlaggedSuccessfully = CurrentBoard.FlagTheField(coordinateX, coordinateY, Players[playerNumber].Symbol);
                        if (!isFieldFlaggedSuccessfully)
                        {
                            MessageThatFieldAlreadyOccupied(coordinateX, coordinateY);
                        }
                    }
                    while (!isFieldFlaggedSuccessfully);
                    DrawBoardInConsole(CurrentBoard, true);
                    var currentCountOfCombinations = Players[playerNumber].CountOfCombinationsMade;
                    Combinator combinator = new(coordinateX, coordinateY, CurrentBoard); // TODO:
                    if (combinator.NumberOfCombinationsFounded > 0)
                    {
                        MessageNewCombinationsCount(combinator.NumberOfCombinationsFounded);

                    }
                    Players[playerNumber].IncreaseCountOfCombinationMade(combinator.NumberOfCombinationsFounded);
                    SetFieldsDirections(combinator);
                    if (Players[playerNumber].CountOfCombinationsMade > currentCountOfCombinations)
                    {
                        MessageAbountPlayerCombinations(Players[playerNumber].Name, Players[playerNumber].CountOfCombinationsMade);
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
                MessageThatNoMoreFields();
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
                MessageCongratsToPlayer(Winner);
            }
            else
            {
                if (Players[0].CountOfCombinationsMade != 0)
                {
                    MessageIsDraw();
                }
                else
                {
                    MessageNotScored();
                }
            }
        }

        /// <summary>
        ///  Writes in the fields the direction of the combinations in which they are involved.
        /// </summary>
        /// <param name="combinator">An instance of the combinator from which information about the combinations will be obtained.</param>
        private void SetFieldsDirections(Combinator combinator)
        {
            foreach ((Directions direction, Field neighbor) in combinator.Neighbors)
            {
                if (direction == Directions.InHorizontal)
                {
                    neighbor.SetInHorizontalCombination();
                }
                else if (direction == Directions.InVertical)
                {
                    neighbor.SetInVerticalCombination();
                }
                else if (direction == Directions.InLeftDiagonal)
                {
                    neighbor.SetInLeftDiagonalCombination();
                }
                else if (direction == Directions.InRightDiagonal)
                {
                    neighbor.SetInRightDiagonalCombination();
                }
            }
        }
    }
}