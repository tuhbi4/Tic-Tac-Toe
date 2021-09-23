using System;

namespace TicTacToe
{
    public static class GameConsole
    {
        public static void Main()
        {
            Game newGame = new();
            newGame.StartGame();
            Console.ReadLine();
        }
    }
}