﻿using System;
using static TicTacToe.Validator;

namespace TicTacToe
{
    public static class Input
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
            Console.WriteLine("Please set the size of the field. The number must be positive and odd (mininum is: 3x3)");
            var boardSize = ValueOfVariableValidation("size", 3, 50);
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
        /// <param name="playerOneSymbol">The default symbol for first player to be set if the user will reject input.</param>
        /// <param name="playerTwoSymbol">The default name that will be returned if the user will reject input.</param>
        /// <returns>32-bit signed integer equivalent to user choise.</returns>
        public static void RequestPlayerSymbol(string playerOneName, out string playerOneSymbol, out string playerTwoSymbol)
        {
            Console.WriteLine($"{playerOneName} your symbol by default is \"X\". If you want change it to \"0\" enter any key below, otherwise leave the field blank (press Enter):");
            if (Console.ReadLine().Length == 0)
            {
                Console.WriteLine($"Your symbol is \"X\".");
                playerOneSymbol = " X ";
                playerTwoSymbol = " 0 ";
            }
            else
            {
                Console.WriteLine($"Your symbol is \"0\".");
                playerTwoSymbol = " 0 ";
                playerOneSymbol = " X ";
            }
        }
    }
}