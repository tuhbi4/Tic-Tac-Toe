using System;

namespace TicTacToe
{
    public class Board
    {
        public int Cols { get; }
        public int Rows { get; }
        public int EmptyCellsCount { get; private set; }
        private string Filler { get; }
        public string[,] BoardMatrix { get; private set; }

        // TODO: try to organize the board as a field structure instead of just a matrix of strings

        public Board(int size)
        {
            Cols = size;
            Rows = size;
            Filler = "     ";
            EmptyCellsCount = size * size;
            BoardMatrix = new string[size + 4, size + 4];
            FillInitiatedBoard();
        }

        /// <summary>
        /// Fills the board with blank fields and adds borders.
        /// </summary>
        private void FillInitiatedBoard()
        {
            for (int rowIndex = 0; rowIndex <= BoardMatrix.GetUpperBound(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex <= BoardMatrix.GetUpperBound(1); colIndex++)
                {
                    if (colIndex == 0 || colIndex == BoardMatrix.GetUpperBound(1))
                    {
                        if (rowIndex == 0 || rowIndex == BoardMatrix.GetUpperBound(0))
                        {
                            BoardMatrix[rowIndex, colIndex] = $"|+++|";
                        }
                        else
                        {
                            if (rowIndex <= 10)
                            {
                                BoardMatrix[rowIndex, colIndex] = $"|-{rowIndex - 1}-|";
                            }
                            else
                            {
                                BoardMatrix[rowIndex, colIndex] = $"|-{rowIndex - 1}|";
                            }
                        }
                    }
                    else if (rowIndex == 0 || rowIndex == BoardMatrix.GetUpperBound(0))
                    {
                        if (colIndex <= 10)
                        {
                            BoardMatrix[rowIndex, colIndex] = $"|-{colIndex - 1}-|";
                        }
                        else
                        {
                            BoardMatrix[rowIndex, colIndex] = $"|-{colIndex - 1}|";
                        }
                    }
                    else
                    {
                        BoardMatrix[rowIndex, colIndex] = Filler;
                    }
                    if ((colIndex == 1 || colIndex == BoardMatrix.GetUpperBound(0) - 1) || (rowIndex == 1 || rowIndex == BoardMatrix.GetUpperBound(1) - 1))
                        {
                        BoardMatrix[rowIndex, colIndex] = "|---|";
                    }
                }
            }
        }

        /// <summary>
        /// Prints the board to the console.
        /// </summary>
        public void DrawBoardInConsole()
        {
            for (int rowIndex = 0; rowIndex <= BoardMatrix.GetUpperBound(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex <= BoardMatrix.GetUpperBound(1); colIndex++)
                {
                    Console.Write(BoardMatrix[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Marks the specified field with the specified symbol.
        /// </summary>
        /// <param name="coordinateX">Board column number.</param>
        /// <param name="coordinateY">Board row number.</param>
        /// <param name="symbol">When this method returns, contains the 32-bit signed integer value equivalent of the number from user input.</param>
        public bool FlagTheField(int coordinateX, int coordinateY, string symbol)
        {
            if (!IsAlreadyOccupied(coordinateX, coordinateY))
            {
                BoardMatrix[coordinateY + 1, coordinateX + 1] = symbol;
                EmptyCellsCount--;
                DrawBoardInConsole();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Сhecks if the field can be occupied.
        /// </summary>
        /// <param name="coordinateX">Board column number.</param>
        /// <param name="coordinateY">Board row number.</param>
        /// <returns>32-bit signed integer equivalent to user input.</returns>
        private bool IsAlreadyOccupied(int coordinateX, int coordinateY)
        {
            if (!BoardMatrix[coordinateY + 1, coordinateX + 1].Equals(Filler))
            {
                return true;
            }
            return false;
        }
    }
}