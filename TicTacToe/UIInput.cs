using System;
using static TicTacToe.ValueValidator;

namespace TicTacToe
{
    public static class UIInput
    {
        /// <summary>
        /// Provides the user with a choice of game mode.
        /// </summary>
        /// <returns>32-bit signed integer equivalent to user choise.</returns>
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

        /// <summary>
        /// Provides the user with a choice of board size.
        /// </summary>
        /// <returns>32-bit signed integer equivalent to user choise.</returns>
        public static int RequestBoardSize()
        {
            Console.WriteLine("\nPlease set the size of the field. The number must be positive and odd (mininum is: 3x3)");
            var boardSize = ValueAndOddOfVariableValidation("board size", 3, 27); // 27 for 1280x1024, 43 for 1920x1080
            return boardSize;
        }

        /// <summary>
        /// Provides the user with a choice of board size.
        /// </summary>
        /// <param name="defaultName">The default name that will be returned if the user will reject input.</param>
        /// <returns>32-bit signed integer equivalent to user choise.</returns>
        public static string RequestName(string defaultName)
        {
            string name;
            Console.WriteLine($"{defaultName}, enter your name or leave the field blank (press Enter):");
            name = Console.ReadLine();
            if (name.Length == 0)
            {
                return defaultName;
            }
            return name;
        }

        /// <summary>
        /// Provides the user with a choice of board size.
        /// </summary>
        /// <param name="playerOneName">The name of the player who will choose the character.</param>
        /// <param name="playerOneFigure">The default symbol for first player to be set if the user will reject input.</param>
        /// <param name="playerTwoFigure">The default name that will be returned if the user will reject input.</param>
        /// <returns>True if the player requested a figure change.</returns>
        public static bool PlayerRequestFigureChange(string playerOneName, string playerOneFigure, string playerTwoFigure)
        {
            Console.WriteLine($"{playerOneName} your symbol by default is \"{playerOneFigure}\". If you want change it to \"{playerTwoFigure}\" enter any key below, otherwise leave the field blank (press Enter):");
            if (Console.ReadLine().Length == 0)
            {
                Console.WriteLine($"Your symbol is \"{playerOneFigure}\".");
                return false;
            }
            else
            {
                Console.WriteLine($"Your symbol is \"{playerTwoFigure}\".");
                return true;
            }
        }
    }
}