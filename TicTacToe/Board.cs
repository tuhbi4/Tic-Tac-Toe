﻿using System;

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
        public string Filler { get; }
        public Field[,] BoardMatrix { get; private set; }

        public Board() : this(3, string.Empty)
        {
        }
        public Board(int size) : this(size, string.Empty)
        {
        }

        public Board(int size, string filler)
        {
            Cols = size;
            Rows = size;
            EmptyCellsCount = size * size;
            Filler = filler;
            BoardMatrix = new Field[size, size];
            BoardConstruction();
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

                    BoardMatrix[rowIndex, colIndex] = new Field(colIndex + 1, rowIndex + 1, Filler);
                }
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
                (BoardMatrix[coordinateY - 1, coordinateX - 1]).ChangeFiller(symbol);
                EmptyCellsCount--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Сhecks if the field can be occupied.
        /// </summary>
        /// <param name="coordinateX">Board column number.</param>
        /// <param name="coordinateY">Board row number.</param>
        /// <returns>32-bit signed integer equivalent to user input.</returns>
        private bool FieldEmpty(int coordinateX, int coordinateY)
        {
            return BoardMatrix[coordinateY - 1, coordinateX - 1].Filler.Equals(Filler);
        }
    }
}