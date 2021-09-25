using System;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe
{
    public class Combinator

    {
        private Board CurrentBoard { get; set; }
        private Field SearchedFor { get; set; }
        private Field FirstNeighbor { get; set; }
        private Field SecondNeighbor { get; set; }
        public int NumberOfCombinationsFounded { get; private set; }
        public List<(Directions, Field)> Neighbors { get; private set; }
        public enum Directions
        {
            InHorizontal,
            InVertical,
            InLeftDiagonal,
            InRightDiagonal,
        };

        private readonly int maxX;
        private readonly int maxY;
        private bool alreadyFoundItInHorizontal;
        private bool alreadyFoundItInVertical;
        private bool alreadyFoundItInLeftDiagonal;
        private bool alreadyFoundItInRightDiagonal;

        public Combinator(int coordinateX, int coordinateY, Board currentBoard)
        {
            CurrentBoard = currentBoard;
            Neighbors = new(3);
            NumberOfCombinationsFounded = 0;
            SearchedFor = currentBoard.BoardMatrix[coordinateY - 1, coordinateX - 1];
            maxY = currentBoard.BoardMatrix.GetUpperBound(0) + 1;
            maxX = currentBoard.BoardMatrix.GetUpperBound(1) + 1;
            CountOfNewCombinationsAppeared();
        }

        /// <summary>
        /// Checks if a new combinations has appeared.
        /// </summary>
        private void CountOfNewCombinationsAppeared()
        {

            var minYForSearch = SearchedFor.Y - 1;
            var maxYForSearch = SearchedFor.Y + 1;
            var minXForSearch = SearchedFor.X - 1;
            var maxXForSearch = SearchedFor.X + 1;

            for (int firstPossibleNeighborY = minYForSearch; firstPossibleNeighborY <= maxYForSearch; firstPossibleNeighborY++)
            {
                for (int firstPossibleNeighborX = minXForSearch; firstPossibleNeighborX <= maxXForSearch; firstPossibleNeighborX++)
                {

                    if (firstPossibleNeighborY >= 1 && firstPossibleNeighborY <= CurrentBoard.BoardMatrix.GetUpperBound(0) + 1
                        && firstPossibleNeighborX >= 1 && firstPossibleNeighborX <= CurrentBoard.BoardMatrix.GetUpperBound(1) + 1)
                    {
                        var firstPossibleNeighbor = CurrentBoard.BoardMatrix[firstPossibleNeighborY - 1, firstPossibleNeighborX - 1];
                        if (firstPossibleNeighbor != SearchedFor
                        && firstPossibleNeighbor.Filler == SearchedFor.Filler)
                        {
                            FirstNeighbor = firstPossibleNeighbor;
                            if (firstPossibleNeighborY == SearchedFor.Y
                                && !alreadyFoundItInHorizontal
                                && !SearchedFor.InHorizontalCombination
                                && !FirstNeighbor.InHorizontalCombination)
                            {
                                SearchInHorizontalAxis();
                            }
                            else if (firstPossibleNeighborX == SearchedFor.X
                                && !alreadyFoundItInVertical
                                && !SearchedFor.InVerticalCombination
                                && !FirstNeighbor.InVerticalCombination)
                            {
                                SearchInVerticalAxis();
                            }
                            else if (firstPossibleNeighborY - SearchedFor.Y == firstPossibleNeighborX - SearchedFor.X
                                && !alreadyFoundItInLeftDiagonal
                                && !SearchedFor.InLeftDiagonalCombination
                                && !FirstNeighbor.InLeftDiagonalCombination)
                            {
                                SearchInLeftDiagonalAxis();
                            }
                            else if (firstPossibleNeighborY - SearchedFor.Y == SearchedFor.X - firstPossibleNeighborX
                                && !alreadyFoundItInRightDiagonal
                                && !SearchedFor.InRightDiagonalCombination
                                && !FirstNeighbor.InRightDiagonalCombination)
                            {
                                SearchInRightDiagonalAxis();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Search a new combinations in horizontal axis.
        /// </summary>
        private void SearchInHorizontalAxis()
        {
            var aheadFirstNeighborX = FirstNeighbor.X - (SearchedFor.X - FirstNeighbor.X);
            var behindSearchedForX = SearchedFor.X + (SearchedFor.X - FirstNeighbor.X);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX)
            {
                secondPossibleNeighborAhead = CurrentBoard.BoardMatrix[FirstNeighbor.Y - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX)
            {
                secondPossibleNeighborBehind = CurrentBoard.BoardMatrix[FirstNeighbor.Y - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == SearchedFor.Filler
                && !secondPossibleNeighborAhead.InHorizontalCombination)
            {
                alreadyFoundItInHorizontal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InHorizontal);
            }
            else if (secondPossibleNeighborBehind.Filler == SearchedFor.Filler
                && !secondPossibleNeighborBehind.InHorizontalCombination)
            {
                alreadyFoundItInHorizontal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InHorizontal);
            }
        }



        /// <summary>
        /// Search a new combinations in vertical axis.
        /// </summary>
        private void SearchInVerticalAxis()
        {
            var aheadFirstNeighborY = FirstNeighbor.Y - (SearchedFor.Y - FirstNeighbor.Y);
            var behindSearchedForY = SearchedFor.Y + (SearchedFor.Y - FirstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = CurrentBoard.BoardMatrix[aheadFirstNeighborY - 1, FirstNeighbor.X - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = CurrentBoard.BoardMatrix[behindSearchedForY - 1, FirstNeighbor.X - 1];
            }
            if (secondPossibleNeighborAhead.Filler == SearchedFor.Filler
                && !secondPossibleNeighborAhead.InVerticalCombination)
            {
                alreadyFoundItInVertical = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InVertical);
            }
            else if (secondPossibleNeighborBehind.Filler == SearchedFor.Filler
                && !secondPossibleNeighborBehind.InVerticalCombination)
            {
                alreadyFoundItInVertical = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InVertical);
            }
        }

        /// <summary>
        /// Search a new combinations in left diagonal axis.
        /// </summary>
        private void SearchInLeftDiagonalAxis()
        {
            var aheadFirstNeighborX = FirstNeighbor.X - (SearchedFor.X - FirstNeighbor.X);
            var aheadFirstNeighborY = FirstNeighbor.Y - (SearchedFor.Y - FirstNeighbor.Y);
            var behindSearchedForX = SearchedFor.X + (SearchedFor.X - FirstNeighbor.X);
            var behindSearchedForY = SearchedFor.Y + (SearchedFor.Y - FirstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX
                && 1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = CurrentBoard.BoardMatrix[aheadFirstNeighborY - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX
                && 1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = CurrentBoard.BoardMatrix[behindSearchedForY - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == SearchedFor.Filler
                && !secondPossibleNeighborAhead.InLeftDiagonalCombination)
            {
                alreadyFoundItInLeftDiagonal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InLeftDiagonal);
            }
            else if (secondPossibleNeighborBehind.Filler == SearchedFor.Filler
                && !secondPossibleNeighborBehind.InLeftDiagonalCombination)
            {
                alreadyFoundItInLeftDiagonal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InLeftDiagonal);
            }
        }

        /// <summary>
        /// Search a new combinations in rght diagonal axis.
        /// </summary>
        private void SearchInRightDiagonalAxis()
        {
            var aheadFirstNeighborX = FirstNeighbor.X - (SearchedFor.X - FirstNeighbor.X);
            var aheadFirstNeighborY = FirstNeighbor.Y - (SearchedFor.Y - FirstNeighbor.Y);
            var behindSearchedForX = SearchedFor.X + (SearchedFor.X - FirstNeighbor.X);
            var behindSearchedForY = SearchedFor.Y + (SearchedFor.Y - FirstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX
                && 1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = CurrentBoard.BoardMatrix[aheadFirstNeighborY - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX
                && 1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = CurrentBoard.BoardMatrix[behindSearchedForY - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == SearchedFor.Filler
                && !secondPossibleNeighborAhead.InRightDiagonalCombination)
            {
                alreadyFoundItInRightDiagonal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InRightDiagonal);
            }
            else if (secondPossibleNeighborBehind.Filler == SearchedFor.Filler
                && !secondPossibleNeighborBehind.InRightDiagonalCombination)
            {
                alreadyFoundItInRightDiagonal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InRightDiagonal);
            }
        }

        /// <summary>
        /// Adds to the list the fields involved in a combination.
        /// </summary>
        /// <param name="secondPossibleNeighborAhead"></param>
        private void AddCombinationToList(Field secondNeighbor, Directions direction)
        {
            SecondNeighbor = secondNeighbor;
            Neighbors.Add((direction, SearchedFor));
            Neighbors.Add((direction, FirstNeighbor));
            Neighbors.Add((direction, SecondNeighbor));
            NumberOfCombinationsFounded++;
        }
    }
}