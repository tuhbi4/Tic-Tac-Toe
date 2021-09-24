using System;

namespace TicTacToe
{
    public static class Combinator
    {
        /// <summary>
        /// Checks if a new combinations has appeared.
        /// </summary>
        public static int CountOfNewCombinationsAppeared(int coordinateX, int coordinateY, Board currentBoard)
        {
            Field searchedFor = currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1];
            var numberOfCombinationsFounded = 0;
            for (int y = searchedFor.Y - 1; y <= searchedFor.Y + 1; y++)
            {
                for (int x = searchedFor.X - 1; x <= searchedFor.X + 1; x++)
                {
                    if (y >= 1 && y <= currentBoard.BoardMatrix.GetUpperBound(0) - currentBoard.BorderWidth - 1
                        && x >= 1 && x <= currentBoard.BoardMatrix.GetUpperBound(1) - currentBoard.BorderWidth - 1
                        && !(y == coordinateY && x == coordinateX)
                        && currentBoard.BoardMatrix[y + 1, x + 1].Filler == searchedFor.Filler
                        && !currentBoard.BoardMatrix[y + 1, x + 1].InVerticalCombination)
                    {
                        if (x == coordinateX)
                        {
                            numberOfCombinationsFounded = SearchInVerticalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                        else if (y == coordinateY)
                        {
                            numberOfCombinationsFounded = SearchInHorizontalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                        else if (y - coordinateY == x - coordinateX)
                        {
                            numberOfCombinationsFounded = SearchInDiagonalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                    }
                }
            }

            if (numberOfCombinationsFounded > 0)
            {
                Console.WriteLine($"! {numberOfCombinationsFounded} combination created!");
            }

            return numberOfCombinationsFounded;
        }

        /// <summary>
        /// Search a new combinations in horizontal axis.
        /// </summary>
        private static int SearchInHorizontalAxis(int coordinateX, int coordinateY, Board currentBoard, Field searchedFor, int numberOfCombinationsFounded, int y, int x)
        {
            if (currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].Filler == searchedFor.Filler
                                            && !currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].SetInHorizontalCombination();
                numberOfCombinationsFounded++;
            }
            else if (currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].SetInHorizontalCombination();
                numberOfCombinationsFounded++;
            }

            return numberOfCombinationsFounded;
        }

        /// <summary>
        /// Search a new combinations in vertical axis.
        /// </summary>
        private static int SearchInVerticalAxis(int coordinateX, int coordinateY, Board currentBoard, Field searchedFor, int numberOfCombinationsFounded, int y, int x)
        {
            if (currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x + 1].Filler == searchedFor.Filler
                                            && !currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInVerticalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInVerticalCombination();
                currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x + 1].SetInVerticalCombination();
                numberOfCombinationsFounded++;
            }
            else if (currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInVerticalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInVerticalCombination();
                currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].SetInVerticalCombination();
                numberOfCombinationsFounded++;
            }

            return numberOfCombinationsFounded;
        }

        /// <summary>
        /// Search a new combinations in dizgonal axis.
        /// </summary>
        private static int SearchInDiagonalAxis(int coordinateX, int coordinateY, Board currentBoard, Field searchedFor, int numberOfCombinationsFounded, int y, int x)
        {
            if (currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x - (coordinateX - x) + 1].Filler == searchedFor.Filler
                                            && !currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x - (coordinateX - x) + 1].SetInHorizontalCombination();
                numberOfCombinationsFounded++;
            }
            else if (currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].InVerticalCombination)
            {
                currentBoard.BoardMatrix[coordinateY, coordinateX].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
                currentBoard.BoardMatrix[y - (coordinateY - y) + 1, coordinateX + (coordinateX - x) + 1].SetInHorizontalCombination();
                numberOfCombinationsFounded++;
            }

            return numberOfCombinationsFounded;
        }
    }
}