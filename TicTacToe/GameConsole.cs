using System;

namespace TicTacToe
{
    public static class GameConsole
    {
        public static void Main()
        {
            Game newGame = new();
            newGame.StartGame();
            Console.WriteLine("\nSend any character to start a new game or leave blank to exit:");
            if (Console.ReadLine().Length != 0)
            {
                Main();
            }
        }
    }
}