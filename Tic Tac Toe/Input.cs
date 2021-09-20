using System;
using static TicTacToe.Validator;

namespace TicTacToe
{
    public static class Input
    {
        public static int RequestGameMode()
        {
            int modeSelector;
            Console.WriteLine("Do you want to play against another player or the bot?");
            while (true)
            {
                Console.WriteLine("Enter the number of your choice\n1. Vs Player\n2. Vs Bot");
                modeSelector = NumberValidation();
                if (modeSelector == 1 || modeSelector == 2)
                {
                    break;
                }
            }
            return modeSelector;
        }

        public static int RequestBoardSize()
        {
            Console.WriteLine("Please set the size of the field. The number must be positive and odd (mininum is: 3x3)");
            var size = SizeValidation("size", 50);          
            return size;
        }

        public static string RequestName(string defaultName)
        {
            string name;
            Console.WriteLine($"{defaultName}, enter your name or leave the field blank:");
            name = Console.ReadLine();
            if (name.Length == 0)
            {
                return defaultName;
            }
            return name;
        }
    }
}