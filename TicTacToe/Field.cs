using System;

namespace TicTacToe
{
    public class Field
    {
        public int X { get; }
        public int Y { get; }

        public Field(int coordinateX, int coordinateY)
        {
            X = coordinateX;
            Y = coordinateY;
        }
    }

    public class RandomField : Field
    {
        private static readonly Random randomCoordinate = new();

        public RandomField(int startOn, int xEndOn, int yEndOn) : base(randomCoordinate.Next(startOn, xEndOn), randomCoordinate.Next(startOn, yEndOn))
        {
        }
    }
}