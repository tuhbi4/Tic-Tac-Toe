using NUnit.Framework;
using System;
using System.IO;

namespace TicTacToe.Tests
{
    [TestFixture()]
    public class InputTests
    {
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("2", ExpectedResult = 2)]
        public int RequestGameModeTest_CorrectInputReturnsCorrectChoise(string str)
        {
            var stringReader = new StringReader(str);
            Console.SetIn(stringReader);
            int result = UIInput.RequestGameMode();
            return result;
        }
    }
}