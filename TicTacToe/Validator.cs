﻿using System;

namespace TicTacToe
{
    public static class Validator
    {
        /// <summary>
        /// Checks that the user entered a number, not a string, and converts the string representation of the number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="number">When this method returns, contains the 32-bit signed integer value equivalent of the number from user input.</param>
        /// <returns>32-bit signed integer equivalent to user input.</returns>
        public static int NumberValidation()
        {
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("This is not a number. Please enter the correct answer:");
                return NumberValidation();
            }
            return number;
        }        

        /// <summary>
        /// Checks that the user has entered a valid number of <see cref="valueName"/> and converts the string representation of the number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="valueName">Name of the variable, the value of which needs to be checked.</param>
        /// <param name="minValue">Minimum allowable value for the variable to be checked.</param>
        /// <param name="maxValue">Maximum allowable value for the variable to be checked..</param>
        /// <returns>32-bit signed integer equivalent to user input.</returns>
        public static int ValueOfVariableValidation(string valueName, int minValue, int maxValue)
        {
            Console.WriteLine($"Set the {valueName}: (minimum is: {minValue}; maximum is {maxValue})");
            while (true)
            {
                var valueOut = NumberValidation();
                if (valueOut < minValue)
                {
                    Console.WriteLine($"Please enter a value greater than or equal to {minValue}");
                }
                else if (valueOut > maxValue)
                {
                    Console.WriteLine($"Please enter a value less than or equal to {maxValue}");
                }
                else
                {
                    return valueOut;
                }
            }
        }

        /// <summary>
        /// Checks that the user has entered a valid number of <see cref="valueName"/> and converts the string representation of the number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="valueName">Name of the variable, the value of which needs to be checked.</param>
        /// <param name="minValue">Minimum allowable value for the variable to be checked.</param>
        /// <param name="maxValue">Maximum allowable value for the variable to be checked..</param>
        /// <returns>32-bit signed integer equivalent to user input.</returns>
        public static int ValueAndOddOfVariableValidation(string valueName, int minValue, int maxValue)
        {

            Console.WriteLine($"Set the {valueName}: (minimum is: {minValue}; maximum is {maxValue})");
            while (true)
            {
                var valueOut = NumberValidation();
                if (valueOut < minValue)
                {
                    Console.WriteLine($"Please enter a value greater than or equal to {minValue}");
                }
                else if (valueOut > maxValue)
                {
                    Console.WriteLine($"Please enter a value less than or equal to {maxValue}");
                }
                else if (valueOut % 2 == 0)
                {
                    Console.WriteLine($"Please enter an odd value");
                }
                else
                {
                    return valueOut;
                }
            }
        }
    }
}