using System;

namespace TicTacToe
{
    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }
        public int CountOfCombinationMade { get; private set; }
        public bool Winner { get; set; }

        public Player(string name, char filler)
        {
            Name = name;
            Symbol = filler;
            CountOfCombinationMade = 0;
        }

        public void IncrementCountOfCombinationMade()
        {
            CountOfCombinationMade++;
        }
    }

    public class Bot : Player
    {
        public int FirstDice { get; private set; }
        public int SecondDice { get; private set; }
        private static readonly Random diceRandomValue = new();

        public Bot(string name, char symbol) : base(name, symbol)
        {
            MakeTurn();
        }

        private void MakeTurn()
        {
            FirstDice = diceRandomValue.Next(1, 6);
            SecondDice = diceRandomValue.Next(1, 6);
            DrawTheDices();
        }
        public void DrawTheDices()
        {
            Console.WriteLine("Throwing the dices ...\n+-------+       +-------+\n|\\       \\     /       /|");
            Console.WriteLine($"| +-------+   +-------+ |\n| |       |   |       | | \n+ |   {FirstDice}   |   |   {SecondDice}   | +");
            Console.WriteLine(" \\|       |   |       |/ \n  +-------+   +-------+  ");
        }
    }
}