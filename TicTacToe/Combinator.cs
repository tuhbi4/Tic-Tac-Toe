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
                        && currentBoard.BoardMatrix[y + 1, x + 1].Filler == searchedFor.Filler)
                    {
                        if (y == coordinateY 
                            && !currentBoard.BoardMatrix[y + 1, x + 1].InHorizontalCombination
                            && !currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].InHorizontalCombination)
                        {
                            numberOfCombinationsFounded = SearchInHorizontalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                        else if (x == coordinateX 
                            && !currentBoard.BoardMatrix[y + 1, x + 1].InVerticalCombination
                            && !currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].InVerticalCombination)
                        {
                            numberOfCombinationsFounded = SearchInVerticalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                        else if (Math.Abs(y - coordinateY) == Math.Abs(x - coordinateX) 
                            && !currentBoard.BoardMatrix[y + 1, x + 1].InDiagonalCombination
                            && !currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].InDiagonalCombination)
                        {
                            numberOfCombinationsFounded = SearchInDiagonalAxis(coordinateX, coordinateY, currentBoard, searchedFor, numberOfCombinationsFounded, y, x);
                        }
                    }
                }
            }

            if (numberOfCombinationsFounded > 0)
            {
                if (numberOfCombinationsFounded == 1)
                {
                    Console.WriteLine($"\n*** {numberOfCombinationsFounded} new combination have been created! ***");
                }
                else
                {
                    Console.WriteLine($"\n*** {numberOfCombinationsFounded} new combinations have been created! ***");
                }

            }

            return numberOfCombinationsFounded;
        }

        /// <summary>
        /// Search a new combinations in horizontal axis.
        /// </summary>
        private static int SearchInHorizontalAxis(int coordinateX, int coordinateY, Board currentBoard, Field searchedFor, int numberOfCombinationsFounded, int y, int x)
        {
            if (currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].InHorizontalCombination)
            {
                numberOfCombinationsFounded = HorizontalNeighborsAhead(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }

            else if (currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].InHorizontalCombination)
            {
                numberOfCombinationsFounded = HorizontalBetweenNeighbors(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }

            return numberOfCombinationsFounded;
        }

        private static int HorizontalNeighborsAhead(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInHorizontalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
            currentBoard.BoardMatrix[y + 1, x - (coordinateX - x) + 1].SetInHorizontalCombination();
            numberOfCombinationsFounded++;
            return numberOfCombinationsFounded;
        }

        private static int HorizontalBetweenNeighbors(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInHorizontalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInHorizontalCombination();
            currentBoard.BoardMatrix[y + 1, coordinateX + (coordinateX - x) + 1].SetInHorizontalCombination();
            numberOfCombinationsFounded++;
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
                numberOfCombinationsFounded = VerticalNeighborsAhead(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }
            else if (currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].InVerticalCombination)
            {
                numberOfCombinationsFounded = VerticalBetweenNeighbors(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }

            return numberOfCombinationsFounded;
        }

        private static int VerticalNeighborsAhead(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInVerticalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInVerticalCombination();
            currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x + 1].SetInVerticalCombination();
            numberOfCombinationsFounded++;
            return numberOfCombinationsFounded;
        }

        private static int VerticalBetweenNeighbors(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInVerticalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInVerticalCombination();
            currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, x + 1].SetInVerticalCombination();
            numberOfCombinationsFounded++;
            return numberOfCombinationsFounded;
        }

        /// <summary>
        /// Search a new combinations in diagonal axis.
        /// </summary>
        private static int SearchInDiagonalAxis(int coordinateX, int coordinateY, Board currentBoard, Field searchedFor, int numberOfCombinationsFounded, int y, int x)
        {
            if (currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x - (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x - (coordinateX - x) + 1].InDiagonalCombination)
            {
                numberOfCombinationsFounded = DiagonalNeighborsAhead(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }
            else if (currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, coordinateX + (coordinateX - x) + 1].Filler == searchedFor.Filler
                && !currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, coordinateX + (coordinateX - x) + 1].InDiagonalCombination)
            {
                numberOfCombinationsFounded = DiagonalBetweenNeighbors(coordinateX, coordinateY, currentBoard, numberOfCombinationsFounded, y, x);
            }

            return numberOfCombinationsFounded;
        }

        private static int DiagonalNeighborsAhead(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInDiagonalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInDiagonalCombination();
            currentBoard.BoardMatrix[y - (coordinateY - y) + 1, x - (coordinateX - x) + 1].SetInDiagonalCombination();
            numberOfCombinationsFounded++;
            return numberOfCombinationsFounded;
        }

        private static int DiagonalBetweenNeighbors(int coordinateX, int coordinateY, Board currentBoard, int numberOfCombinationsFounded, int y, int x)
        {
            currentBoard.BoardMatrix[coordinateY + 1, coordinateX + 1].SetInDiagonalCombination();
            currentBoard.BoardMatrix[y + 1, x + 1].SetInDiagonalCombination();
            currentBoard.BoardMatrix[coordinateY + (coordinateY - y) + 1, coordinateX + (coordinateX - x) + 1].SetInDiagonalCombination();
            numberOfCombinationsFounded++;
            return numberOfCombinationsFounded;
        }
    }
}