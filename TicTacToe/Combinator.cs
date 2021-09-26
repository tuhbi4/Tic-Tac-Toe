using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Represents a combinator for the given figures. Allows to search for matches and simulate possible.
    /// <see cref="Neighbors"/> contains a list of fields that are included in the combinations found.
    /// <see cref="Directions"/> enumeration of possible directions of combinations .
    /// Provides methods for finding combinations of any player figures and simulating possible combinations for the entire board.
    /// </summary>
    public class Combinator

    {
        public List<(Directions, Field)> Neighbors { get; private set; }

        public enum Directions
        {
            InHorizontal,
            InVertical,
            InLeftDiagonal,
            InRightDiagonal,
        };

        public Combinator(Board currentBoard)
        {
            maxY = currentBoard.BoardMatrix.GetUpperBound(0) + 1;
            maxX = currentBoard.BoardMatrix.GetUpperBound(1) + 1;
            Neighbors = new(3);
        }

        private Field firstNeighbor;
        private readonly int maxX;
        private readonly int maxY;
        private bool alreadyFoundItInHorizontal;
        private bool alreadyFoundItInVertical;
        private bool alreadyFoundItInLeftDiagonal;
        private bool alreadyFoundItInRightDiagonal;
        private int numberOfCombinationsFound;
        private List<(Directions, Field)> neighbors = new(3);

        /// <summary>
        /// Checks if a new combinations has appeared.
        /// </summary>
        public int GetCountOfNewCombinations(Board currentBoard, int coordinateX, int coordinateY, bool isSimulation)
        {
            var searchFor = currentBoard.BoardMatrix[coordinateY - 1, coordinateX - 1];
            numberOfCombinationsFound = 0;
            Neighbors = new();
            var minYForSearch = searchFor.Y - 1;
            var maxYForSearch = searchFor.Y + 1;
            var minXForSearch = searchFor.X - 1;
            var maxXForSearch = searchFor.X + 1;

            for (int firstPossibleNeighborY = minYForSearch; firstPossibleNeighborY <= maxYForSearch; firstPossibleNeighborY++)
            {
                for (int firstPossibleNeighborX = minXForSearch; firstPossibleNeighborX <= maxXForSearch; firstPossibleNeighborX++)
                {
                    if (firstPossibleNeighborY >= 1 && firstPossibleNeighborY <= currentBoard.BoardMatrix.GetUpperBound(0) + 1
                        && firstPossibleNeighborX >= 1 && firstPossibleNeighborX <= currentBoard.BoardMatrix.GetUpperBound(1) + 1)
                    {
                        var firstPossibleNeighbor = currentBoard.BoardMatrix[firstPossibleNeighborY - 1, firstPossibleNeighborX - 1];
                        if (firstPossibleNeighbor != searchFor
                        && firstPossibleNeighbor.Filler == searchFor.Filler)
                        {
                            firstNeighbor = firstPossibleNeighbor;
                            if (firstPossibleNeighborY == searchFor.Y
                                && !alreadyFoundItInHorizontal
                                && !searchFor.InHorizontalCombination
                                && !firstNeighbor.InHorizontalCombination)
                            {
                                SearchInHorizontalAxis(currentBoard, searchFor, isSimulation);
                            }
                            else if (firstPossibleNeighborX == searchFor.X
                                && !alreadyFoundItInVertical
                                && !searchFor.InVerticalCombination
                                && !firstNeighbor.InVerticalCombination)
                            {
                                SearchInVerticalAxis(currentBoard, searchFor, isSimulation);
                            }
                            else if (firstPossibleNeighborY - searchFor.Y == firstPossibleNeighborX - searchFor.X
                                && !alreadyFoundItInLeftDiagonal
                                && !searchFor.InLeftDiagonalCombination
                                && !firstNeighbor.InLeftDiagonalCombination)
                            {
                                SearchInLeftDiagonalAxis(currentBoard, searchFor, isSimulation);
                            }
                            else if (firstPossibleNeighborY - searchFor.Y == searchFor.X - firstPossibleNeighborX
                                && !alreadyFoundItInRightDiagonal
                                && !searchFor.InRightDiagonalCombination
                                && !firstNeighbor.InRightDiagonalCombination)
                            {
                                SearchInRightDiagonalAxis(currentBoard, searchFor, isSimulation);
                            }
                        }
                    }
                }
            }
            return numberOfCombinationsFound;
        }

        /// <summary>
        /// Search a new combinations in horizontal axis.
        /// </summary>
        private void SearchInHorizontalAxis(Board currentBoard, Field searchFor, bool isSimulation)
        {
            var aheadFirstNeighborX = firstNeighbor.X - (searchFor.X - firstNeighbor.X);
            var behindSearchedForX = searchFor.X + (searchFor.X - firstNeighbor.X);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX)
            {
                secondPossibleNeighborAhead = currentBoard.BoardMatrix[firstNeighbor.Y - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX)
            {
                secondPossibleNeighborBehind = currentBoard.BoardMatrix[firstNeighbor.Y - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == searchFor.Filler
                && !secondPossibleNeighborAhead.InHorizontalCombination)
            {
                alreadyFoundItInHorizontal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InHorizontal, searchFor, isSimulation);
            }
            else if (secondPossibleNeighborBehind.Filler == searchFor.Filler
                && !secondPossibleNeighborBehind.InHorizontalCombination)
            {
                alreadyFoundItInHorizontal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InHorizontal, searchFor, isSimulation);
            }
        }

        /// <summary>
        /// Search a new combinations in vertical axis.
        /// </summary>
        private void SearchInVerticalAxis(Board currentBoard, Field searchFor, bool isSimulation)
        {
            var aheadFirstNeighborY = firstNeighbor.Y - (searchFor.Y - firstNeighbor.Y);
            var behindSearchedForY = searchFor.Y + (searchFor.Y - firstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = currentBoard.BoardMatrix[aheadFirstNeighborY - 1, firstNeighbor.X - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = currentBoard.BoardMatrix[behindSearchedForY - 1, firstNeighbor.X - 1];
            }
            if (secondPossibleNeighborAhead.Filler == searchFor.Filler
                && !secondPossibleNeighborAhead.InVerticalCombination)
            {
                alreadyFoundItInVertical = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InVertical, searchFor, isSimulation);
            }
            else if (secondPossibleNeighborBehind.Filler == searchFor.Filler
                && !secondPossibleNeighborBehind.InVerticalCombination)
            {
                alreadyFoundItInVertical = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InVertical, searchFor, isSimulation);
            }
        }

        /// <summary>
        /// Search a new combinations in left diagonal axis.
        /// </summary>
        private void SearchInLeftDiagonalAxis(Board currentBoard, Field searchFor, bool isSimulation)
        {
            var aheadFirstNeighborX = firstNeighbor.X - (searchFor.X - firstNeighbor.X);
            var aheadFirstNeighborY = firstNeighbor.Y - (searchFor.Y - firstNeighbor.Y);
            var behindSearchedForX = searchFor.X + (searchFor.X - firstNeighbor.X);
            var behindSearchedForY = searchFor.Y + (searchFor.Y - firstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX
                && 1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = currentBoard.BoardMatrix[aheadFirstNeighborY - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX
                && 1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = currentBoard.BoardMatrix[behindSearchedForY - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == searchFor.Filler
                && !secondPossibleNeighborAhead.InLeftDiagonalCombination)
            {
                alreadyFoundItInLeftDiagonal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InLeftDiagonal, searchFor, isSimulation);
            }
            else if (secondPossibleNeighborBehind.Filler == searchFor.Filler
                && !secondPossibleNeighborBehind.InLeftDiagonalCombination)
            {
                alreadyFoundItInLeftDiagonal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InLeftDiagonal, searchFor, isSimulation);
            }
        }

        /// <summary>
        /// Search a new combinations in rght diagonal axis.
        /// </summary>
        private void SearchInRightDiagonalAxis(Board currentBoard, Field searchFor, bool isSimulation)
        {
            var aheadFirstNeighborX = firstNeighbor.X - (searchFor.X - firstNeighbor.X);
            var aheadFirstNeighborY = firstNeighbor.Y - (searchFor.Y - firstNeighbor.Y);
            var behindSearchedForX = searchFor.X + (searchFor.X - firstNeighbor.X);
            var behindSearchedForY = searchFor.Y + (searchFor.Y - firstNeighbor.Y);
            Field secondPossibleNeighborAhead = new();
            if (1 <= aheadFirstNeighborX && aheadFirstNeighborX <= maxX
                && 1 <= aheadFirstNeighborY && aheadFirstNeighborY <= maxY)
            {
                secondPossibleNeighborAhead = currentBoard.BoardMatrix[aheadFirstNeighborY - 1, aheadFirstNeighborX - 1];
            }
            Field secondPossibleNeighborBehind = new();
            if (1 <= behindSearchedForX && behindSearchedForX <= maxX
                && 1 <= behindSearchedForY && behindSearchedForY <= maxY)
            {
                secondPossibleNeighborBehind = currentBoard.BoardMatrix[behindSearchedForY - 1, behindSearchedForX - 1];
            }

            if (secondPossibleNeighborAhead.Filler == searchFor.Filler
                && !secondPossibleNeighborAhead.InRightDiagonalCombination)
            {
                alreadyFoundItInRightDiagonal = true;
                AddCombinationToList(secondPossibleNeighborAhead, Directions.InRightDiagonal, searchFor, isSimulation);
            }
            else if (secondPossibleNeighborBehind.Filler == searchFor.Filler
                && !secondPossibleNeighborBehind.InRightDiagonalCombination)
            {
                alreadyFoundItInRightDiagonal = true;
                AddCombinationToList(secondPossibleNeighborBehind, Directions.InRightDiagonal, searchFor, isSimulation);
            }
        }

        /// <summary>
        /// Adds to the list the fields involved in a combination.
        /// </summary>
        /// <param name="secondPossibleNeighbor"></param>
        private void AddCombinationToList(Field secondPossibleNeighbor, Directions direction, Field searchFor, bool isSimulation)
        {
            if (isSimulation)
            {
                neighbors.Add((direction, searchFor));
                neighbors.Add((direction, firstNeighbor));
                neighbors.Add((direction, secondPossibleNeighbor));
            }
            else
            {
                Neighbors.Add((direction, searchFor));
                Neighbors.Add((direction, firstNeighbor));
                Neighbors.Add((direction, secondPossibleNeighbor));
                numberOfCombinationsFound++;
            }
        }

        /// <summary>
        /// Simulates possible combinations for all empty fields for the current player for the current board.
        /// </summary>
        /// <param name="currentBoard">The board for simulating combinations.</param>
        /// <param name="player">The player for simulating combinations.</param>
        /// <returns>The number of possible combinations</returns>
        public int SimulationCombinationsForEmptyFields(Board currentBoard, Player player)
        {
            var simulationBoard = currentBoard.CloneBoardMatrix();
            var countOfPossibleCombinations = 0;

            for (var y = 1; y <= maxY; y++)
            {
                for (var x = 1; x <= maxX; x++)
                {
                    alreadyFoundItInHorizontal = false;
                    alreadyFoundItInVertical = false;
                    alreadyFoundItInLeftDiagonal = false;
                    alreadyFoundItInRightDiagonal = false;

                    if (simulationBoard.BoardMatrix[y - 1, x - 1].Filler == simulationBoard.Filler)
                    {
                        simulationBoard.BoardMatrix[y - 1, x - 1] = new(x, y, player.Figure);
                        neighbors = new();
                        GetCountOfNewCombinations(simulationBoard, x, y, true);
                        countOfPossibleCombinations += neighbors.Count / 3;
                        SetFieldsDirections();
                    }
                }
            }
            return countOfPossibleCombinations;
        }

        /// <summary>
        ///  Writes the direction of combinations in the fields that make up these combinations.
        ///  required for the correct operation of the simulator.
        /// </summary>
        private void SetFieldsDirections()
        {
            foreach ((Directions direction, Field neighbor) in neighbors)
            {
                if (direction == Directions.InHorizontal)
                {
                    neighbor.SetInHorizontalCombination();
                }
                else if (direction == Directions.InVertical)
                {
                    neighbor.SetInVerticalCombination();
                }
                else if (direction == Directions.InLeftDiagonal)
                {
                    neighbor.SetInLeftDiagonalCombination();
                }
                else if (direction == Directions.InRightDiagonal)
                {
                    neighbor.SetInRightDiagonalCombination();
                }
            }
        }
    }
}