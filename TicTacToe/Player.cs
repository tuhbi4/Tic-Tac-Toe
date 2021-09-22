using System;

namespace TicTacToe
{
    public class Player
    {
        public string Name { get; }
        public string Symbol { get; }
        public int CountOfCombinationsMade { get; private set; }
        public bool Winner { get; set; }

        public Player(string name, string filler)
        {
            Name = name;
            Symbol = filler;
            CountOfCombinationsMade = 0;
        }

        public void IncrementCountOfCombinationMade()
        {
            CountOfCombinationsMade++;
        }
    }

    public class Bot : Player
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        private static readonly Random diceRandomValue = new();

        public Bot(string name, string symbol) : base(name, symbol)
        {
        }

        public void MakeTurn(int maxValue, out int coordinateX, out int coordinateY)
        {
            FirstDice = diceRandomValue.Next(1, maxValue);
            coordinateX = FirstDice;
            SecondDice = diceRandomValue.Next(1, maxValue);
            coordinateY = SecondDice;
            DrawTheDices();
        }
        private void DrawTheDices()
        {
            Console.WriteLine("Throwing the dices ...\n+-------+       +-------+\n|\\       \\     /       /|");
            Console.WriteLine($"| +-------+   +-------+ |\n| |       |   |       | | \n+ |   {FirstDice}   |   |   {SecondDice}   | +");
            Console.WriteLine(" \\|       |   |       |/ \n  +-------+   +-------+  ");
        }
    }
}