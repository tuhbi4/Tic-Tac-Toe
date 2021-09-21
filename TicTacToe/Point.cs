using System;

namespace TicTacToe
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int coordinateX, int coordinateY)
        {
            X = coordinateX;
            Y = coordinateY;
        }
    }

    public class RandomPoint : Point
    {
        private static readonly Random randomCoordinate = new();

        public RandomPoint(int startOn, int xEndOn, int yEndOn) : base(randomCoordinate.Next(startOn, xEndOn), randomCoordinate.Next(startOn, yEndOn))
        {
        }
    }
}