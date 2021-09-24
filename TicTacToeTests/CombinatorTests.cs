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
    
    public class CombinatorTests // currentBoard.BoardMatrix[rowIndex, colIndex] = new Field(colIndex - 1, rowIndex - 1, Filler)
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
            var currentBoard = new Board(3, true);
            currentBoard.BoardMatrix[2, 2] = new Field(1, 1, "  X  ");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[2, 2].SetInHorizontalCombination();
            }
            currentBoard.BoardMatrix[2, 3] = new Field(2, 1, "  X  ");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[2, 3].SetInHorizontalCombination();
            }
            currentBoard.BoardMatrix[2, 4] = new Field(3, 1, "  X  ");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[2, 4].SetInHorizontalCombination();
            }
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
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
            var currentBoard = new Board(3, true);
            currentBoard.BoardMatrix[2, 2] = new Field(1, 1, "  X  ");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[2, 2].SetInVerticalCombination();
            }
            currentBoard.BoardMatrix[3, 2] = new Field(1, 2, "  X  ");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[3, 2].SetInVerticalCombination();
            }
            currentBoard.BoardMatrix[4, 2] = new Field(1, 3, "  X  ");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[4, 2].SetInVerticalCombination();
            }
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
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
            var currentBoard = new Board(3, true);
            currentBoard.BoardMatrix[2, 2] = new Field(1, 1, "  X  ");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[2, 2].SetInDiagonalCombination();
            }
            currentBoard.BoardMatrix[3, 3] = new Field(2, 2, "  X  ");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[3, 3].SetInDiagonalCombination();
            }
            currentBoard.BoardMatrix[4, 4] = new Field(3, 3, "  X  ");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[4, 4].SetInDiagonalCombination();
            }
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
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
            var currentBoard = new Board(3, true);
            currentBoard.BoardMatrix[2, 4] = new Field(3, 1, "  X  ");
            if (firstLocked)
            {
                currentBoard.BoardMatrix[2, 4].SetInDiagonalCombination();
            }
            currentBoard.BoardMatrix[3, 3] = new Field(2, 2, "  X  ");
            if (secondLocked)
            {
                currentBoard.BoardMatrix[3, 3].SetInDiagonalCombination();
            }
            currentBoard.BoardMatrix[4, 2] = new Field(1, 3, "  X  ");
            if (thirdLocked)
            {
                currentBoard.BoardMatrix[4, 2].SetInDiagonalCombination();
            }
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
            return result;
        }

        [TestCase(1, 3, ExpectedResult = 0)]

        public int VerticalTest_SecondCombinationInARow_WithLocked(int x, int y)
        {
            var currentBoard = new Board(5, true);
            currentBoard.BoardMatrix[2, 2] = new Field(1, 1, "  X  ");
            currentBoard.BoardMatrix[2, 2].SetInVerticalCombination();
            currentBoard.BoardMatrix[3, 2] = new Field(1, 2, "  X  ");
            currentBoard.BoardMatrix[3, 2].SetInVerticalCombination();
            currentBoard.BoardMatrix[4, 2] = new Field(1, 3, "  X  ");
            currentBoard.BoardMatrix[4, 2].SetInVerticalCombination();
            currentBoard.BoardMatrix[6, 2] = new Field(1, 5, "  X  ");
            currentBoard.BoardMatrix[6, 2].SetInVerticalCombination();
            currentBoard.BoardMatrix[5, 2] = new Field(1, 4, "  X  ");
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
            return result;
        }

        [TestCase(3, 3, ExpectedResult = 2)]

        public int VerticalTestAndDiagonalTest_SecondCombinationInARowInBothDirections(int x, int y)
        {
            var currentBoard = new Board(5, true);
            currentBoard.BoardMatrix[2, 4] = new Field(3, 1, "  X  ");
            currentBoard.BoardMatrix[3, 4] = new Field(3, 2, "  X  ");
            currentBoard.BoardMatrix[5, 4] = new Field(3, 4, "  X  ");
            currentBoard.BoardMatrix[6, 4] = new Field(3, 5, "  X  ");
            currentBoard.BoardMatrix[6, 2] = new Field(1, 5, "  X  ");
            currentBoard.BoardMatrix[5, 3] = new Field(2, 4, "  X  ");
            currentBoard.BoardMatrix[3, 5] = new Field(4, 2, "  X  ");
            currentBoard.BoardMatrix[2, 6] = new Field(5, 1, "  X  ");
            currentBoard.BoardMatrix[4, 4] = new Field(3, 3, "  X  ");
            int result = Combinator.CountOfNewCombinationsAppeared(x, y, currentBoard);
            return result;
        }

    }
}