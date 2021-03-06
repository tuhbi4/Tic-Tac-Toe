namespace TicTacToe
{
    /// <summary>
    /// Represents a field cell, with information about the coordinates to which it is associated and information about the combinations in which it participates.
    /// 32-bit integer <see cref="X"/> contains the value of the X coordinate to which this field is related; zero if the field has no relation to the coordinate.
    /// 32-bit integer <see cref="Y"/> contains the value of the Y coordinate to which this field is related; zero if the field has no relation to the coordinate.
    /// String <see cref="Filler"/> сontains filler, if given. Otherwise an empty string.
    /// Boolean <see cref="InHorizontalCombination"/> true if the cell participates in a horizontal combination, false otherwise.
    /// Boolean <see cref="InVerticalCombination"/> true if the cell participates in a vertical combination, false otherwise.
    /// Boolean <see cref="InLeftDiagonalCombination"/> true if the cell participates in a left diagonal combination, false otherwise.
    /// Boolean <see cref="InRightDiagonalCombination"/> true if the cell participates in a right diagonal combination, false otherwise.
    /// Provides a method to increment count of combinations maded.
    /// </summary>
    public class Field
    {
        public int X { get; }
        public int Y { get; }
        public string Filler { get; private set; }
        public bool InHorizontalCombination { get; private set; }
        public bool InVerticalCombination { get; private set; }
        public bool InLeftDiagonalCombination { get; private set; }
        public bool InRightDiagonalCombination { get; private set; }

        public Field() : this(0, 0, string.Empty)
        {
        }

        public Field(string filler) : this(0, 0, filler)
        {
        }

        public Field(int coordinateX, int coordinateY) : this(coordinateX, coordinateY, string.Empty)
        {
        }

        public Field(int coordinateX, int coordinateY, string filler)
        {
            X = coordinateX;
            Y = coordinateY;
            Filler = filler;
            InHorizontalCombination = false;
            InVerticalCombination = false;
            InLeftDiagonalCombination = false;
            InRightDiagonalCombination = false;
        }

        /// <summary>
        /// Returns the instance of String.
        /// </summary>
        /// <returns><see cref="Filler"/></returns>
        public override string ToString()
        {
            return Filler;
        }

        /// <summary>
        /// Provides a method to change the filler of а field.
        /// </summary>
        public void ChangeFiller(string filler)
        {
            Filler = filler;
        }

        /// <summary>
        /// Set <see cref="InHorizontalCombination"/> property to state "true".
        /// </summary>
        public void SetInHorizontalCombination()
        {
            InHorizontalCombination = true;
        }

        /// <summary>
        /// Set <see cref="InVerticalCombination"/> property to state "true".
        /// </summary>
        public void SetInVerticalCombination()
        {
            InVerticalCombination = true;
        }

        /// <summary>
        /// Set <see cref="InLeftDiagonalCombination"/> property to state "true".
        /// </summary>
        public void SetInLeftDiagonalCombination()
        {
            InLeftDiagonalCombination = true;
        }

        /// <summary>
        /// Set <see cref="InDiagonalCombination"/> property to state "true".
        /// </summary>
        public void SetInRightDiagonalCombination()
        {
            InRightDiagonalCombination = true;
        }
    }
}