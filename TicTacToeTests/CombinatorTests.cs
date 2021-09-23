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
        [Test()]
        public void CountOfNewCombinationsAppearedTest()
        {
            var currentBoard = new Board();
            currentBoard.BoardMatrix[2, 2] = new Field("  X  ");
            currentBoard.BoardMatrix[2, 3] = new Field("  X  ");
            currentBoard.BoardMatrix[2, 4] = new Field("  X  ");
            Assert.AreEqual(1, Combinator.CountOfNewCombinationsAppeared(1, 1, currentBoard));
            Assert.AreEqual(1, Combinator.CountOfNewCombinationsAppeared(1, 2, currentBoard));
            Assert.AreEqual(1, Combinator.CountOfNewCombinationsAppeared(1, 3, currentBoard));
        }
    }
}