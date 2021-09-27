using System;
using System.Collections.Generic;
using static TicTacToe.Combinator;
using static TicTacToe.UIInput;
using static TicTacToe.UIOutput;

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
    /// string <see cref="EmptyFieldFigure"/> contains the figure for the board..
    /// string <see cref="PlayerOneFigure"/> contains the figure of one from player.
    /// string <see cref="PlayerTwoFigure"/> contains the figure of another player.
    /// string <see cref="Winner"/> contains the name of the winner.
    /// boolean <see cref="GameOver"/> true, if game is over, otherwise false.
    /// <see cref="CurrentBoard"/> Board with parameters for the current game specified at the start.
    /// <see cref="Combinator"/> Instance of Combinator type, which makes combinations and simulates new.
    /// <see cref="Players"/> List of players for the current game specified at the start.
    /// </summary>
    public class Game
    {
        public enum GameModes
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer
        };
        public enum BotsLevels
        {
            Random = 1,
            Thinking,
            Analyzing
        };

        public GameModes GameMode { get; private set; }
        public BotsLevels BotLevel { get; private set; }
        public string EmptyFieldFigure { get; private set; }
        public string PlayerOneFigure { get; private set; }
        public string PlayerTwoFigure { get; private set; }
        public string Winner { get; private set; }
        public bool GameOver { get; private set; }
        public Board CurrentBoard { get; private set; }
        private Combinator Combinator { get; set; }

        public List<Player> Players { get; } = new() { };

        public Game() : this(" ", "X", "0")
        {
        }

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
            if (GameMode == GameModes.PlayerVsComputer)
            {
                BotLevel = (BotsLevels)RequestBotLevel();
            }
            CurrentBoard = new Board(RequestBoardSize(), emptyFieldFigure);
            Combinator = new(CurrentBoard);
            DefinePlayersList();
            DefinePlayersOrder();
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
            var possibleTurnsCount = CurrentBoard.EmptyCellsCount / 2;
            Players.Add(new Player(playerOneName, PlayerOneFigure, possibleTurnsCount + 1));
            if (GameMode.Equals(GameModes.PlayerVsPlayer))
            {
                var playerTwoName = RequestName("Player 2");
                Players.Add(new Player(playerTwoName, PlayerTwoFigure, possibleTurnsCount));
            }
            else if (GameMode.Equals(GameModes.PlayerVsComputer))
            {
                MessageIfOpponentIsBot();
                if (BotLevel.Equals(BotsLevels.Random))
                {
                    if (Players[0].Figure.Equals(PlayerOneFigure))
                    {
                        Players.Add(new Bot("Computer", PlayerTwoFigure, possibleTurnsCount));
                    }
                    else
                    {
                        Players.Add(new Bot("Computer", PlayerOneFigure, possibleTurnsCount));
                    }
                }
                else if (BotLevel.Equals(BotsLevels.Thinking))
                {
                    if (Players[0].Figure.Equals(PlayerOneFigure))
                    {
                        Players.Add(new BotAI("Computer", PlayerTwoFigure, possibleTurnsCount));
                    }
                    else
                    {
                        Players.Add(new BotAI("Computer", PlayerOneFigure, possibleTurnsCount));
                    }
                }
            }
        }

        private void DefinePlayersOrder()
        {
            switch (RequestPlayersOrder(Players[0].Name, Players[1].Name))
            {
                case 2:
                    Players.Reverse();
                    break;
                case 3:
                    Random random = new();
                    var number = random.Next(1, Players.Count);
                    MakeFirst(number);
                    break;
            }
        }

        private void MakeFirst(int number)
        {
            Players.Insert(0, Players[number]);
            Players.RemoveAt(number + 1);
        }

        /// <summary>
        /// Main control algorithm.
        /// </summary>
        public void StartGame()
        {
            MessageAboutGameStarted();
            var currentTurnNumber = 0;
            DrawBoardInConsole(CurrentBoard, true);
            do
            {
                currentTurnNumber++;
                MessageAboutTurn(currentTurnNumber);
                NextTurn();
            }
            while (!GameOver);
            WhoWins();
        }

        /// <summary>
        /// Makes a turn request for each player.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when type of player is invalid.</exception>
        private void NextTurn()
        {
            foreach (var player in Players)
            {
                if (!GameOver)
                {
                    MessageWhoseTurn(player.Name);
                    FlagTheField(player, out int coordinateX, out int coordinateY);
                    player.DecrementRemainingTurnsCount();
                    DrawBoardInConsole(CurrentBoard, true);
                    var combinationsFoundedCount = Combinator.GetNewCombinationsCount(CurrentBoard, coordinateX, coordinateY, false);
                    if (combinationsFoundedCount > 0)
                    {
                        MessageNewCombinationsCount(combinationsFoundedCount);
                        player.IncreaseCombinationMadeCount(combinationsFoundedCount);
                        MessageAbountPlayerCombinations(player.Name, player.CombinationsMadeCount);
                        SetFieldsDirections(Combinator);
                    }
                    foreach (var person in Players)
                    {
                        if (CurrentBoard.EmptyCellsCount <= CurrentBoard.BoardMatrix.Length - 2)
                        {
                            var possibleCombinationsCount = Combinator.SimulationCombinationsForEmptyFields(CurrentBoard, person);
                            person.SetPossibleCombinationsCount(possibleCombinationsCount);
                            CheckGameOver(person);
                        }
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
        private void ComputerTurn(Player player, out int coordinateX, out int coordinateY)
        {
            int x = 0;
            int y = 0;
            if (BotLevel.Equals(BotsLevels.Random))
            {
                ((Bot)player).MakeTurn(1, CurrentBoard.Rows, out x, out y);
            }
            else if (BotLevel.Equals(BotsLevels.Thinking))
            {
                DrawTheDices(((Bot)player).FirstDice, ((Bot)player).SecondDice);
            }
            coordinateX = x;
            coordinateY = y;
        }

        /// <summary>
        /// Marks the specified field with the specified symbol.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        /// <param name="coordinateX">Board column number.</param>
        /// <param name="coordinateY">Board row number.</param>
        /// <returns>true if the field is occupied successfully, otherwise false.</returns>
        public void FlagTheField(Player player, out int coordinateX, out int coordinateY)
        {
            var flaggedSuccessfully = false;
            do
            {
                if (player.GetType() == typeof(Player))
                {
                    PlayerTurn(out coordinateX, out coordinateY);
                }
                else if (player.GetType() == typeof(Bot) || player.GetType() == typeof(BotAI))
                {
                    ComputerTurn(player, out coordinateX, out coordinateY);
                    MessageBotMadeTurn(player.Name);
                }
                else
                {
                    throw new InvalidOperationException();
                }
                if (CurrentBoard.IsFieldEmpty(coordinateX, coordinateY))
                {
                    (CurrentBoard.BoardMatrix[coordinateY - 1, coordinateX - 1]).ChangeFiller(player.Figure);
                    CurrentBoard.DecreaceEmptyCellsCount();
                    flaggedSuccessfully = true;
                }
                else
                {
                    MessageThatFieldAlreadyOccupied(coordinateX, coordinateY);
                }
            }
            while (!flaggedSuccessfully);
        }

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <returns>true if no more empty fields on the board
        /// or if one of the participants reaches the number of combinations in which the opponent cannot get ahead of him
        /// or if there are no more options to make a combination;
        /// otherwise, false</returns>
        private void CheckGameOver(Player player)
        {
            if (player.PossibleCombinationsCount == 0)
            {
                MessageThatNoMoreCombinationsPossible(player.Name);
                GameOver = true;
                SetWinner();
            }
            else if (Players[0].CombinationsMadeCount - Players[1].CombinationsMadeCount > Players[1].PossibleCombinationsCount)
            {
                MessageThatNoChanseToWin(Players[1].Name);
                GameOver = true;
                SetWinner();
            }
            else if (Players[1].CombinationsMadeCount - Players[0].CombinationsMadeCount > Players[0].PossibleCombinationsCount)
            {
                MessageThatNoChanseToWin(Players[0].Name);
                GameOver = true;
                SetWinner();
            }
            else if (CurrentBoard.EmptyCellsCount == 0)
            {
                MessageThatNoMoreFields();
                GameOver = true;
                SetWinner();
            }
        }

        /// <summary>
        /// Sets the winner based on the number of combinations made.
        /// </summary>
        private void SetWinner()
        {
            MessageWithScores(Players[0].Name, Players[0].CombinationsMadeCount, Players[1].Name, Players[1].CombinationsMadeCount);
            if (Players[0].CombinationsMadeCount != Players[1].CombinationsMadeCount)
            {
                if (Players[0].CombinationsMadeCount > Players[1].CombinationsMadeCount)
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
                if (Players[0].CombinationsMadeCount != 0)
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
        ///  Writes the direction of combinations in the fields that make up these combinations.
        /// </summary>
        /// <param name="combinator">An instance of the combinator from which information about the combinations will be obtained.</param>
        private static void SetFieldsDirections(Combinator combinator)
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