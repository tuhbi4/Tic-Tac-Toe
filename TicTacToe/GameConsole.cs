using System;

namespace TicTacToe
{
    public static class GameConsole
    {
        static void Main()
        {
            Game newGame = new();
            newGame.StartGame();
            Console.ReadLine();
        }
    }
}