using System;

namespace TicTacToe
{
    public static class Validator
    {
        public static int NumberValidation()
        {
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("This is not a number. Please enter the correct answer:");
                return NumberValidation();
            }
            return number;
        }

        public static int SizeValidation(string valueName, int maxValue)
        {
            
            Console.WriteLine($"Set the odd number of board {valueName}: (minimum is: 3; maximum is {maxValue})");
            while (true)
            {
                var size = NumberValidation();
                if (size < 3)
                {
                    Console.WriteLine($"Please enter a value greater than or equal to 3");
                }
                else if (size > maxValue)
                {
                    Console.WriteLine($"Please enter a value less than or equal to {maxValue}");
                }
                else if (size % 2 == 0)
                {
                    Console.WriteLine($"Please enter a odd value");
                }
                else
                {
                    return size;
                }
            }
        }

        public static int ValueValidation(string valueName, int minValue, int maxValue)
        {
            int valueOut;
            Console.WriteLine($"Set the number of {valueName}: (minimum is: {minValue}; maximum is {maxValue})");
            while (true)
            {
                valueOut = NumberValidation();
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
    }
}