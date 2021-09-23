using System;

namespace TicTacToe
{
    public static class Combinator
    {
        /// <summary>
        /// Checks if a new combination has appeared.
        /// </summary>
        public static int CountOfNewCombinationsAppeared(int coordinateX, int coordinateY, Board currentBoard)
        {
            var add = currentBoard.BorderWidth;
            Field searchedFor = currentBoard.BoardMatrix[coordinateX - 1 + add, coordinateY - 1 + add];
            Console.WriteLine($"Search for a combination for a [{searchedFor.X},{searchedFor.Y}] has started...");
            var numberOfCombinationsFounded = 0;
            var minIndex = add;
            var maxIndex = currentBoard.BoardMatrix.GetUpperBound(0) - add;
            for (int rowIndex = 0; rowIndex <= currentBoard.BoardMatrix.GetUpperBound(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex <= currentBoard.BoardMatrix.GetUpperBound(0); colIndex++)
                {
                    if (rowIndex >= minIndex && rowIndex <= maxIndex
                        && colIndex >= minIndex && colIndex <= maxIndex
                        && !(rowIndex == coordinateY + 1 && colIndex == coordinateX + 1)
                        && currentBoard.BoardMatrix[rowIndex, colIndex] == searchedFor)
                    {
                        Console.WriteLine($"First neighbor is [{rowIndex - 1},{colIndex - 1}]...");
                        if (rowIndex == coordinateY + 1 && currentBoard.BoardMatrix[rowIndex, coordinateX + 1 - (coordinateY + 1 - colIndex)] == searchedFor)
                        {
                            Console.WriteLine($"Second neighbor is [{coordinateX - (coordinateX - (rowIndex - 1))},{coordinateY - (coordinateY - (colIndex - 1))}]...");
                            Console.WriteLine("Combination created!");
                            numberOfCombinationsFounded++;
                        }
                        else
                        {
                            Console.WriteLine("Second neighbor not found. Combination is not created!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No one neighbor around. not found. Combination is not created!");
                    }
                }
            }
            return numberOfCombinationsFounded;
        }
    }
}