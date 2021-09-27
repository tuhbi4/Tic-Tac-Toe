using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents a player with the following properties:
    /// string type properties <see cref="Name"/> and <see cref="Figure"/>;
    /// 32-bit integer <see cref="CombinationsMadeCount"/>, contains the number of combinations made by the player;
    /// always greater than or equal to zero;
    /// 32-bit integer <see cref="PossibleCombinationsCount"/>, contains the number of combinations that a player can still make on the current board;
    /// always greater than or equal to zero;
    /// Provides a method to increment count of combinations maded.
    /// </summary>
    public class Player
    {
        public string Name { get; }
        public string Figure { get; }
        public int CombinationsMadeCount { get; private set; }
        public int PossibleCombinationsCount { get; private set; }
        public int RemainingTurnsCount { get; private set; }

        public Player() : this(string.Empty, string.Empty, 0)
        {
        }

        public Player(string name, string symbol, int remainingTurnsCount)
        {
            Name = name;
            Figure = symbol;
            CombinationsMadeCount = 0;
            RemainingTurnsCount = remainingTurnsCount;
        }

        /// <summary>
        /// Increases the count of combinations maded by player.
        /// </summary>
        /// <param name="count">The number by which to increase the counter.</param>
        public void IncreaseCombinationMadeCount(int count)
        {
            CombinationsMadeCount += count;
        }

        /// <summary>
        /// Sets the number of possible combinations for the player.
        /// </summary>
        /// <param name="count">The number that will be set to the counter.</param>
        public void SetPossibleCombinationsCount(int count)
        {
            PossibleCombinationsCount = count;
        }

        /// <summary>
        /// Decreases the count of remaining turns.
        /// </summary>
        public void DecreaseRemainingTurnsCount()
        {
            if (RemainingTurnsCount > 0)
            {
                RemainingTurnsCount--;
            }
        }
    }

    /// <summary>
    /// Represents a simple bot with the following properties:
    /// 32-bit integer <see cref="FirstDice"/> and <see cref="SecondDice"/> that are represents a random value in a given range.
    /// Provides method to generate a new values.
    /// </summary>
    public class Bot : Player
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        private readonly Random diceRandomValue = new();

        public Bot() : this(string.Empty, string.Empty, 0)
        {
        }

        public Bot(string name, string symbol, int remainingTurnsCount) : base(name, symbol, remainingTurnsCount)
        {
        }

        /// <summary>
        /// Generates new values.
        /// </summary>
        /// <param name="minValue">The minimum number in the range.</param>
        /// <param name="maxValue">The maximum number in the range.</param>
        /// <param name="coordinateX">Output value for column number .</param>
        /// <param name="coordinateY">Output value for row number.</param>
        public void MakeTurn(int minValue, int maxValue, out int coordinateX, out int coordinateY)
        {
            FirstDice = diceRandomValue.Next(minValue, maxValue + 1);
            coordinateX = FirstDice;
            SecondDice = diceRandomValue.Next(minValue, maxValue + 1);
            coordinateY = SecondDice;
        }
    }

    /// <summary>
    /// Represents a simple bot with the following properties:
    /// 32-bit integer <see cref="FirstDice"/> and <see cref="SecondDice"/> that are represents a random value in a given range.
    /// Provides method to generate a new values.
    /// </summary>
    public class BotAI : Player
    {
        public BotAI() : this(string.Empty, string.Empty, 0)
        {
        }

        public BotAI(string name, string symbol, int remainingTurnsCount) : base(name, symbol, remainingTurnsCount)
        {
        }

        /// <summary>
        /// Generates new values. Uses an intelligent generation algorithm.
        /// </summary>
        /// <param name="coordinateX">Output value for column number .</param>
        /// <param name="coordinateY">Output value for row number.</param>
        public void MakeIntellectualTurn()
        {
            throw new NotSupportedException(); // TODO: Will be implemented in the future
        }
    }
}