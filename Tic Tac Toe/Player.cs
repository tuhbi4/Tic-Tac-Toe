namespace TicTacToe
{
    public class Player
    {
        public string Name { get; }
        public char Filler { get; }
        public int CountOfCombinationMade { get; private set; }
        public bool GameOver { get; set; }

        public Player(string name, char filler)
        {
            Name = name;
            Filler = filler;
            CountOfCombinationMade = 0;
        }

        public void IncrementCountOfCombinationMade()
        {
            CountOfCombinationMade++;
        }
    }
}