using System;

namespace TicTacToe
{
    /// <summary>
    /// Represents a set of instances of the type field in the form of a board of a given size with a specific cell content and controls the number of cells with this filler.
    /// 32-bit integer <see cref="Cols"/> contains the board width equivalent to the board size excluding the border width.
    /// 32-bit integer <see cref="Rows"/> contains the board height equivalent to the board size excluding the border width.
    /// String <see cref="Filler"/> contains filler if given. Otherwise an empty string. 
    /// Boolean <see cref="InHorizontalCombination"/> true if the cell participates in a horizontal combination, false otherwise.
    /// Boolean <see cref="InVerticalCombination"/> true if the cell participates in a vertical combination, false otherwise.
    /// Boolean <see cref="InDiagonalCombination"/> true if the cell participates in a dizgonal combination, false otherwise.
    /// Provides a method to increment count of combinations maded.
    /// </summary>
    public class Board
    {
        public int Cols { get; }
        public int Rows { get; }
        public int EmptyCellsCount { get; private set; }
        private string Filler { get; }
        public int BorderWidth { get; }
        public bool WithBorder { get; }
        public Field[,] BoardMatrix { get; private set; }

        public Board() : this(3, false)
        {
        }
        public Board(int size) : this(size, false)
        {
        }

        public Board(int size, bool withBorder)
        {
            Cols = size;
            Rows = size;
            WithBorder = withBorder;
            EmptyCellsCount = size * size;

            if (withBorder)
            {
                BorderWidth = 2;
                Filler = "     ";
                BoardMatrix = new Field[size + BorderWidth * 2, size + BorderWidth * 2];
                BoardWithBorderConstruction();
            }
            else
            {
                BorderWidth = 0;
                Filler = string.Empty;
                BoardMatrix = new Field[size, size];
                BoardConstruction();
            }
        }

        /// <summary>
        /// Builds a board with borders and empty fields.
        /// </summary>
        private void BoardWithBorderConstruction()
        {
            for (int rowIndex = 0; rowIndex <= BoardMatrix.GetUpperBound(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex <= BoardMatrix.GetUpperBound(1); colIndex++)
                {
                    if (colIndex == 0 || colIndex == BoardMatrix.GetUpperBound(1))
                    {
                        if (rowIndex == 0 || rowIndex == BoardMatrix.GetUpperBound(0))
                        {
                            BoardMatrix[rowIndex, colIndex] = new Field($"|+++|");
                        }
                        else
                        {
                            if (rowIndex <= 10)
                            {
                                BoardMatrix[rowIndex, colIndex] = new Field($"|-{rowIndex - 1}-|");
                            }
                            else
                            {
                                BoardMatrix[rowIndex, colIndex] = new Field($"|-{rowIndex - 1}|");
                            }
                        }
                    }
                    else if (rowIndex == 0 || rowIndex == BoardMatrix.GetUpperBound(0))
                    {
                        if (colIndex <= 10)
                        {
                            BoardMatrix[rowIndex, colIndex] = new Field($"|-{colIndex - 1}-|");
                        }
                        else
                        {
                            BoardMatrix[rowIndex, colIndex] = new Field($"|-{colIndex - 1}|");
                        }
                    }
                    else
                    {
                        BoardMatrix[rowIndex, colIndex] = new Field(Filler);
                    }
                    if ((colIndex == 1 || colIndex == BoardMatrix.GetUpperBound(0) - 1) || (rowIndex == 1 || rowIndex == BoardMatrix.GetUpperBound(1) - 1))
                    {
                        BoardMatrix[rowIndex, colIndex] = new Field($"|---|");
                    }
                }
            }
        }

        /// <summary>
        /// Builds a board with blank fields.
        /// </summary>
        private void BoardConstruction()
        {
            for (int rowIndex = 0; rowIndex <= BoardMatrix.GetUpperBound(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex <= BoardMatrix.GetUpperBound(1); colIndex++)
                {

                    BoardMatrix[rowIndex, colIndex] = new Field(Filler);
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
        /// <returns>true if the field is occupied successfully, otherwise false.</returns>
        public bool FlagTheField(int coordinateX, int coordinateY, string symbol)
        {
            if (FieldEmpty(coordinateX, coordinateY))
            {
                (BoardMatrix[coordinateY + 1, coordinateX + 1]).ChangeFiller(symbol);
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
        private bool FieldEmpty(int coordinateX, int coordinateY)
        {
            if (BoardMatrix[coordinateY + 1, coordinateX + 1].Filler.Equals(Filler))
            {
                return true;
            }
            return false;
        }
    }
}