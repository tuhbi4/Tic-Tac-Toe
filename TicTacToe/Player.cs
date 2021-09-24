using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents a player with the following properties:
    /// string type properties <see cref="Name"/> and <see cref="Symbol"/>;
    /// 32-bit integer <see cref="CountOfCombinationsMade"/>, that always greater than or equal to zero;
    /// boolean <see cref="Winner"/>.
    /// Provides a method to increment count of combinations maded.
    /// </summary>
    public class Player
    {
        public string Name { get; }
        public string Symbol { get; }
        public int CountOfCombinationsMade { get; private set; }
        public bool Winner { get; set; }

        public Player() : this(string.Empty, string.Empty)
        {
        }

        public Player(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            CountOfCombinationsMade = 0;
        }

        /// <summary>
        /// Increases the count of maded combinations.
        /// </summary>
        /// <param name="count">The number by which to increase the counter.</param>
        public void IncreaseCountOfCombinationMade(int count)
        {
            CountOfCombinationsMade += count;
        }
    }

    /// <summary>
    /// Represents a simple bot with the following properties:
    /// 32-bit integer <see cref="FirstDice"/> and <see cref="SecondDice"/> that are represents a random value in a given range.
    /// If <see cref="PrintingDicesEnabled"/> is true, dice will be printed every time a roll occurs.Enabled by default.
    /// Provides method to generate a new values.
    /// </summary>
    public class Bot : Player
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        public bool PrintingDicesEnabled { get; private set; }
        private readonly Random diceRandomValue = new();

        public Bot() : this(string.Empty, string.Empty, true)
        {
        }

        public Bot(string name, string symbol) : this(name, symbol, true)
        {
        }

        public Bot(string name, string symbol, bool printingDicesEnabled) : base(name, symbol)
        {
            PrintingDicesEnabled = printingDicesEnabled;
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
            if (PrintingDicesEnabled)
            {
                DrawTheDices();
            }
        }
        private void DrawTheDices()
        {
            Console.WriteLine("Computer roll the dices ...\n+-------+       +-------+\n|\\       \\     /       /|");
            Console.WriteLine($"| +-------+   +-------+ |\n| |       |   |       | | \n+ |   {FirstDice}   |   |   {SecondDice}   | +");
            Console.WriteLine(" \\|       |   |       |/ \n  +-------+   +-------+  ");
        }
    }

    /// <summary>
    /// Represents a simple bot with the following properties:
    /// 32-bit integer <see cref="FirstDice"/> and <see cref="SecondDice"/> that are represents a random value in a given range.
    /// Provides method to generate a new values.
    /// </summary>
    public class BotAI : Player
    {
        public BotAI() : this(string.Empty, string.Empty)
        {
        }

        public BotAI(string name, string symbol) : base(name, symbol)
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