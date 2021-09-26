using NUnit.Framework;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Tests
{
    [TestFixture()]

    public class CombinatorTests
    {
        [TestCase(3, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(3, 1, true, false, false, ExpectedResult = 0)]
        [TestCase(3, 1, false, true, false, ExpectedResult = 0)]
        [TestCase(3, 1, true, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 1, false, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, true, ExpectedResult = 0)]
        [TestCase(1, 1, false, true, true, ExpectedResult = 0)]
        [TestCase(2, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(2, 1, true, false, false, ExpectedResult = 0)]
        [TestCase(2, 1, false, false, true, ExpectedResult = 0)]
        [TestCase(2, 1, true, false, true, ExpectedResult = 0)]

        public int HorizontalTest_BothDirectionsAndBetween_WithLocked(int x, int y, bool firstLocked, bool secondLocked, bool thirdLocked)
        {
            var currentBoard = new Board(3);
            currentBoard.BoardMatrix[0, 0] = new Field(1, 1, "X");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[0, 0].SetInHorizontalCombination();
            }
            currentBoard.BoardMatrix[0, 1] = new Field(2, 1, "X");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[0, 1].SetInHorizontalCombination();
            }
            currentBoard.BoardMatrix[0, 2] = new Field(3, 1, "X");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[0, 2].SetInHorizontalCombination();
            }
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

        [TestCase(1, 3, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 3, true, false, false, ExpectedResult = 0)]
        [TestCase(1, 3, false, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, true, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 1, false, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, true, ExpectedResult = 0)]
        [TestCase(1, 1, false, true, true, ExpectedResult = 0)]
        [TestCase(1, 2, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 2, true, false, false, ExpectedResult = 0)]
        [TestCase(1, 2, false, false, true, ExpectedResult = 0)]
        [TestCase(1, 2, true, false, true, ExpectedResult = 0)]
        public int VerticalTest_BothDirectionsAndBetween_WithLocked(int x, int y, bool firstLocked, bool secondLocked, bool thirdLocked)
        {
            var currentBoard = new Board(3);
            currentBoard.BoardMatrix[0, 0] = new Field(1, 1, "X");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[0, 0].SetInVerticalCombination();
            }
            currentBoard.BoardMatrix[1, 0] = new Field(1, 2, "X");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[1, 0].SetInVerticalCombination();
            }
            currentBoard.BoardMatrix[2, 0] = new Field(1, 3, "X");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[2, 0].SetInVerticalCombination();
            }
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

        [TestCase(3, 3, false, false, false, ExpectedResult = 1)]
        [TestCase(3, 3, true, false, false, ExpectedResult = 0)]
        [TestCase(3, 3, false, true, false, ExpectedResult = 0)]
        [TestCase(3, 3, true, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 1, false, true, false, ExpectedResult = 0)]
        [TestCase(1, 1, false, false, true, ExpectedResult = 0)]
        [TestCase(1, 1, false, true, true, ExpectedResult = 0)]
        [TestCase(2, 2, false, false, false, ExpectedResult = 1)]
        [TestCase(2, 2, true, false, false, ExpectedResult = 0)]
        [TestCase(2, 2, false, false, true, ExpectedResult = 0)]
        [TestCase(2, 2, true, false, true, ExpectedResult = 0)]
        public int DiagonalTest_FromTopLeftToBottomRightAndBack_WithLocked(int x, int y, bool firstLocked, bool secondLocked, bool thirdLocked)
        {
            var currentBoard = new Board(3);
            currentBoard.BoardMatrix[0, 0] = new Field(1, 1, "X");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[0, 0].SetInLeftDiagonalCombination();
            }
            currentBoard.BoardMatrix[1, 1] = new Field(2, 2, "X");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[1, 1].SetInLeftDiagonalCombination();
            }
            currentBoard.BoardMatrix[2, 2] = new Field(3, 3, "X");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[2, 2].SetInLeftDiagonalCombination();
            }
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

        [TestCase(1, 3, false, false, false, ExpectedResult = 1)]
        [TestCase(1, 3, true, false, false, ExpectedResult = 0)]
        [TestCase(1, 3, false, true, false, ExpectedResult = 0)]
        [TestCase(1, 3, true, true, false, ExpectedResult = 0)]
        [TestCase(3, 1, false, false, false, ExpectedResult = 1)]
        [TestCase(3, 1, false, true, false, ExpectedResult = 0)]
        [TestCase(3, 1, false, false, true, ExpectedResult = 0)]
        [TestCase(3, 1, false, true, true, ExpectedResult = 0)]
        [TestCase(2, 2, false, false, false, ExpectedResult = 1)]
        [TestCase(2, 2, true, false, false, ExpectedResult = 0)]
        [TestCase(2, 2, false, false, true, ExpectedResult = 0)]
        [TestCase(2, 2, true, false, true, ExpectedResult = 0)]

        public int DiagonalTest_FromTopRightToBottomLeftAndBack_WithLocked(int x, int y, bool firstLocked, bool secondLocked, bool thirdLocked)
        {
            var currentBoard = new Board(3);
            currentBoard.BoardMatrix[0, 2] = new Field(3, 1, "X");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[0, 2].SetInRightDiagonalCombination();
            }
            currentBoard.BoardMatrix[1, 1] = new Field(2, 2, "X");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[1, 1].SetInRightDiagonalCombination();
            }
            currentBoard.BoardMatrix[2, 0] = new Field(1, 3, "X");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[2, 0].SetInRightDiagonalCombination();
            }
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

        [TestCase(1, 3, ExpectedResult = 0)]

        public int VerticalTest_SecondCombinationInARow_WithLocked(int x, int y)
        {
            var currentBoard = new Board(5);
            currentBoard.BoardMatrix[0, 0] = new Field(1, 1, "X");
            currentBoard.BoardMatrix[0, 0].SetInVerticalCombination();
            currentBoard.BoardMatrix[1, 0] = new Field(1, 2, "X");
            currentBoard.BoardMatrix[1, 0].SetInVerticalCombination();
            currentBoard.BoardMatrix[2, 0] = new Field(1, 3, "X");
            currentBoard.BoardMatrix[2, 0].SetInVerticalCombination();
            currentBoard.BoardMatrix[4, 0] = new Field(1, 5, "X");
            currentBoard.BoardMatrix[4, 0].SetInVerticalCombination();
            currentBoard.BoardMatrix[3, 0] = new Field(1, 4, "X");
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

        [TestCase(3, 3, ExpectedResult = 2)]

        public int VerticalTestAndDiagonalTest_SecondCombinationInARowInBothDirections(int x, int y)
        {
            var currentBoard = new Board(5);
            currentBoard.BoardMatrix[0, 2] = new Field(3, 1, "X");
            currentBoard.BoardMatrix[1, 2] = new Field(3, 2, "X");
            currentBoard.BoardMatrix[3, 2] = new Field(3, 4, "X");
            currentBoard.BoardMatrix[4, 2] = new Field(3, 5, "X");
            currentBoard.BoardMatrix[4, 0] = new Field(1, 5, "X");
            currentBoard.BoardMatrix[3, 1] = new Field(2, 4, "X");
            currentBoard.BoardMatrix[1, 3] = new Field(4, 2, "X");
            currentBoard.BoardMatrix[0, 4] = new Field(5, 1, "X");
            currentBoard.BoardMatrix[2, 2] = new Field(3, 3, "X");
            Combinator combinator = new(currentBoard);
            int result = combinator.GetCountOfNewCombinations(currentBoard, x, y, false);
            return result;
        }

    }
}